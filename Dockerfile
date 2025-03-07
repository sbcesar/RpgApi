# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /RpgApi

# Copiar todo
COPY . ./
# Restaurar dependencias
RUN dotnet restore
# Compilar y publicar el proyecto
RUN dotnet publish -c Release -o /RpgApi/out

# Build runtime image (usar la imagen .NET 8.0)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /RpgApi
COPY --from=build /RpgApi/out .
ENTRYPOINT ["dotnet", "RpgApi.dll"]