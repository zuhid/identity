# Install mssql as part of docker

```
docker run \
  --name mssql-local \
  --hostname mssql-local \
  --publish 1433:1433 \
  --env "ACCEPT_EULA=Y" \
  --env "SA_PASSWORD=P@ssw0rd" \
  --detach \
  mcr.microsoft.com/mssql/server:2022-latest
```

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

# Run tests

https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows

`dotnet tool install -g dotnet-reportgenerator-globaltool`

```
cd Identity.Tests
rm -rf TestResults
dotnet test --collect:"XPlat Code Coverage"
dotnet reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" -targetdir:"TestResults/CoverageReport" -reporttypes:Html


rm -rf Base.Tests/TestResults
rm -rf Identity.Tests/TestResults
dotnet test --collect:"XPlat Code Coverage"
dotnet reportgenerator -reports:"Base.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:"Base.Tests/TestResults/CoverageReport" -reporttypes:Html
dotnet reportgenerator -reports:"Identity.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:"Identity.Tests/TestResults/CoverageReport" -reporttypes:Html
-reporttypes:Html
```
