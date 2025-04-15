# docker exec -it api /bin/bash

# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY ./Base Base
COPY ./Identity Identity

# Build the application
# RUN dotnet build Identity
RUN dotnet publish Identity -c Release -o out

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime

# Set the working directory inside the container for the runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out .

# Expose the application's default port (adjust if needed)
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "Zuhid.Identity.dll"]
