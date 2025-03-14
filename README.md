# Sql Server

```
docker run --env "ACCEPT_EULA=Y" --env "MSSQL_SA_PASSWORD=P@ssw0rd" -p 1433:1433 --name mssql-local --hostname mssql-local --detach mcr.microsoft.com/mssql/server:2022-latest
docker start mssql-local
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

# Generate a new Angular application

- `--directory Web` Create the project in the "Web" directory
- `--prefix nc`Set the prefix for component selectors to "nc"
- `--server-routing=false` Without server-side routing for the initial setup
- `--ssr=false` Without server side rendering
- `--strict` Enable strict type checking in the application
- `--style=scss` Use SCSS as the stylesheet format
- `identity` Name of the application

```
ng new \
  --directory Web \
  --prefix nc \
  --server-routing=false \
  --skip-git \
  --skip-install \
  --ssr=false \
  --strict \
  --style=scss \
  identity
```
