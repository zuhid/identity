# Prerequisites
- docker
- .net 9
- angular 

# Setup local environment
```sh
dotnet tool restore # restore tools

cd Identity && dotnet run
cd Web && npm start

clear && dotnet run
clear && npm start

http://localhost:5215
http://localhost:4200
http://localhost:8025
```


# Generate RSA public/private key
```sh
openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048 # Generate a Private Key
openssl rsa -pubout -in private_key.pem -out public_key.pem # Extract the Public Key
```

# Generate a new Angular application

- `--directory Web` Create the project in the "Web" directory
- `--prefix zc`Set the prefix for component selectors to "zc"
- `--server-routing=false` Without server-side routing for the initial setup
- `--ssr=false` Without server side rendering
- `--strict` Enable strict type checking in the application
- `--style=scss` Use SCSS as the stylesheet format
- `identity` Name of the application

```
ng new \
  --directory Web \
  --prefix zc \
  --server-routing=false \
  --skip-git \
  --skip-install \
  --ssr=false \
  --standalone=fale \
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

