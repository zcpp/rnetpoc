FROM docker.wsa-dev.work/library/r-netcore:1.0
WORKDIR /app
COPY ./netcoreapp3.1/ ./
CMD dotnet rnetpoc.dll appsettings.json