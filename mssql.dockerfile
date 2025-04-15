FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

# COPY ./log.sql /var/opt/mssql/data/
# EXPOSE 1433

# Start the SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
