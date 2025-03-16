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
  dotnet ef database update 0
  rm -rf Migrations
  dotnet ef migrations add Initial
  dotnet build
  dotnet ef database update
  # dotnet ef migrations script --idempotent --output Migrations/db_script.sql
); }

################################################## main ##################################################

# dotnet tool restore
# build-server
build-database "Identity"
