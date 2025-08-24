#!/bin/bash

################################################## variables ##################################################
postgres_image="postgres:17"
postgres_container="postgres_container"
postgres_user="postgres"
postgres_password="P@ssw0rd"

################################################## functions ##################################################

set_secrets(){
  dotnet user-secrets --project Identity set db_credential "User Id=postgres;Password=P@ssw0rd"
  # dotnet user-secrets --project Identity list
  # dotnet user-secrets --project clear
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
  docker run --name $postgres_container --publish 5432:5432 --detach \
    --env "POSTGRES_USER=$postgres_user" \
    --env "POSTGRES_PASSWORD=$postgres_password" \
    $postgres_image
}

build_database(){
  dbname=${3,,} # get the database in lower case
  docker start $postgres_container # start the container
  rm -rf $1/Migrations # remove migration folder
  dotnet ef migrations add initial --project $1 --startup-project $2 --context "$3Context" # create migration
  dotnet ef migrations script --project $1 --startup-project $2 --context "$3Context" --output $1/Migrations/script.sql # create script for review
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

build-database-log() {
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
}

################################################## execute ##################################################
clear
# set_secrets
# update_dotnet_packages
# build_server
# build_database Identity Identity Identity
# build_database BaseApi Identity Log
build-database-log
