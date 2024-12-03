# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PolarisContacts/PolarisContacts.csproj", "PolarisContacts/"]
COPY ["PolarisContacts.Application/PolarisContacts.Application.csproj", "PolarisContacts.Application/"]
COPY ["PolarisContacts.CrossCutting.Helpers/PolarisContacts.CrossCutting.Helpers.csproj", "PolarisContacts.CrossCutting.Helpers/"]
COPY ["PolarisContacts.Domain/PolarisContacts.Domain.csproj", "PolarisContacts.Domain/"]
COPY ["PolarisContacts.CrossCutting.DependencyInjection/PolarisContacts.CrossCutting.DependencyInjection.csproj", "PolarisContacts.CrossCutting.DependencyInjection/"]
COPY ["PolarisContacts.Infrastructure/PolarisContacts.Infrastructure.csproj", "PolarisContacts.Infrastructure/"]
RUN dotnet restore "./PolarisContacts/PolarisContacts.csproj"
COPY . .
WORKDIR "/src/PolarisContacts"
RUN dotnet build "./PolarisContacts.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PolarisContacts.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PolarisContacts.dll"]