################################################## variables ##################################################
SCRIPT_DIR=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)

################################################## functions ##################################################
build-server() {
  docker container rm --force mssql-local

  docker run \
    --name mssql-local \
    --hostname mssql-local \
    --publish 1433:1433 \
    --env "ACCEPT_EULA=Y" \
    --env "SA_PASSWORD=P@ssw0rd" \
    --detach \
    mcr.microsoft.com/mssql/server:2022-latest
}

build-database() { (
  cd "$SCRIPT_DIR/$1"
  dotnet ef database update 0 --context $2
  rm -rf Migrations
  dotnet ef migrations add Initial --context $2
  dotnet build
  dotnet ef database update --context $2
  # dotnet ef migrations script --context $2 --idempotent --output Migrations/db_script.sql
); }

build-database-log() {
  # docker exec -it mssql-local  bash
  docker exec mssql-local /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -d master -Q "
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

start-api() { (
  cd "$SCRIPT_DIR/$1"
  dotnet run
); }

################################################## main ##################################################

clear
# dotnet tool restore
# build-server
# docker start mssql-local
# build-database-log
# build-database "Identity" "IdentityContext"
# start-api "Identity"
