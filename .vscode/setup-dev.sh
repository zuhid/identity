################################################## variables ##################################################
SQL_USERNAME="sa"
SQL_PASSWORD="P@ssw0rd"

################################################## functions ##################################################
build-server() {
  echo "Building SQL Server container..."
  # remove old container
  docker container stop mssql-dev
  docker container rm mssql-dev

  # create new container
  docker run \
    --name mssql-dev \
    --hostname mssql-dev \
    --publish 1433:1433 \
    --env "ACCEPT_EULA=Y" \
    --env "SA_PASSWORD=$SQL_PASSWORD" \
    --detach \
    mcr.microsoft.com/mssql/server:2022-latest
}

build-database-log() {
  # docker exec -it mssql-dev bash
  echo "Building Log database..."
  docker exec mssql-dev /opt/mssql-tools18/bin/sqlcmd -S localhost -U $SQL_USERNAME -P $SQL_PASSWORD -C -d master -Q "
    if exists (select 1 from sys.databases where name = 'Log')
      drop database Log;
    go
    create database Log;
    go
    use Log;
    go
    create table dbo.Log (
        Id uniqueidentifier NOT NULL
      , Updated datetime2 null
      , LogLevel nvarchar(100) null
      , Category nvarchar(100) null
      , EventId nvarchar(100) null
      , EventName nvarchar(100) null
      , State nvarchar(max) null
      , Exception nvarchar(max) null,
      CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    )"
}

# $1 is the path to the project
# $2 is the name of the context
build-database() {
  echo "Building $2 database..."
  cd "$1"
  # dotnet ef database update 0 --context $2c
  rm -rf Migrations
  dotnet ef migrations add Initial --context $2
  dotnet build
  dotnet ef database update --context $2
  # dotnet ef migrations script --context $2 --idempotent --output Migrations/db_script.sql
}

################################################## main ##################################################

clear
build-server
dotnet tool restore
sleep 10 # wait for SQL Server to start
build-database-log
build-database "Identity" "IdentityContext"
