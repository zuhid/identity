# Upgrade packages
```sh
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
```

# Secrets
```sh
dotnet user-secrets init
dotnet user-secrets clear
dotnet user-secrets set db_credential "User Id=postgres;Password=P@ssw0rd;"
dotnet user-secrets list
```
