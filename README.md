# Sql Server
```
docker run --env "ACCEPT_EULA=Y" --env "MSSQL_SA_PASSWORD=P@ssw0rd" -p 1433:1433 --name mssql-local --hostname mssql-local --detach mcr.microsoft.com/mssql/server:2022-latest
docker start mssql-local
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

