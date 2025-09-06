#!/bin/bash

################################################## variables ##################################################
# postgres_image="postgres:17"
postgres_image="postgres:17"
postgres_container="postgres_container"
postgres_user="postgres"
postgres_password="P@ssw0rd"
postgres_port="5432"

################################################## functions ##################################################

set_secrets(){
  dotnet user-secrets --project Identity clear
  dotnet user-secrets --project Identity set postgres_server "Server=localhost;Port=$postgres_port;User Id=postgres;Password=P@ssw0rd"
  dotnet user-secrets --project Identity list
}

# This script updates all outdated NuGet packages in .NET projects within the current directory.
update_dotnet_packages() {
  find . -name "*.csproj" | sort | while read csproj; do
    echo "Processing project: $csproj"
    dotnet list "$csproj" package --outdated | grep -E '>' | awk '{print $2, $4}' |
    while read name version; do
      echo "Updating package: $name to project: $csproj"
      dotnet add "$csproj" package "$name"
    done 
  done
  # dotnet list package --outdated
  dotnet tool update --all # Update all tools
}

build_server(){
  # remove the image
  docker container rm "$postgres_container" --force
  # Run the PostgreSQL container
  docker run --name $postgres_container --publish $postgres_port:5432 --detach \
    --env "POSTGRES_USER=$postgres_user" \
    --env "POSTGRES_PASSWORD=$postgres_password" \
    $postgres_image

  # remove the image
  docker container rm mailhog_container --force
  # Run the PostgreSQL container
  docker run --name mailhog_container --publish 1025:1025 --publish 8025:8025 --detach mailhog/mailhog
}

build_database(){
  dbname=${3,,} # get the database in lower case
  docker start $postgres_container # start the container

  ################################################## recreate migration
  rm -rf $1/Migrations # remove migration folder
  dotnet ef migrations add initial --project $1 --startup-project $2 --context "$3Context" # create migration
  dotnet ef migrations script --project $1 --startup-project $2 --context "$3Context" --output $1/Migrations/script.sql # create script for review

  ################################################## build log database
  docker exec $postgres_container bash -c "dropdb --username=$postgres_user --if-exists --force log" # drop database
  docker exec $postgres_container bash -c "createdb --username=$postgres_user log" # create database
  docker exec -it $postgres_container psql -U $postgres_user -d log -c '
  create table log (
      id uuid not null,
      updated timestamp with time zone not null,
      log_level text not null,
      category text not null,
      event_id text not null,
      event_name text not null,
      state text not null,
      exception text not null,
      constraint pk_log primary key (id)
  );'

  ################################################## build identity database
  docker exec $postgres_container bash -c "dropdb --username=$postgres_user --if-exists --force $dbname" # drop database
  docker exec $postgres_container bash -c "createdb --username=$postgres_user $dbname" # create database
  ##### create __EFMigrationsHistory table
  docker exec -it $postgres_container psql -U $postgres_user -d $dbname -c '
  create table "__EFMigrationsHistory" (
    "MigrationId" character varying(150) not null,
    "ProductVersion" character varying(32) not null,
    constraint "PK___EFMigrationsHistory" primary key ("MigrationId")
  );'
  dotnet ef database update --project $1 --startup-project $2 --context "$3Context" # apply migrations
}

################################################## execute ##################################################
clear
time {
  # set_secrets
  # update_dotnet_packages
  build_server
  build_database Identity Identity Identity
  # build_database BaseApi Identity Log
}
