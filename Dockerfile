# Use uma imagem base do SDK do .NET para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho
WORKDIR /app

# Copia o csproj e restaura as dependências
COPY PolarisContacts/*.csproj ./
RUN dotnet restore

# Copia o restante do código
COPY . .

# Publica a aplicação
RUN dotnet publish -c Release -o out

# Usa a imagem base do runtime do .NET para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/out .

# Define a porta que a aplicação irá escutar
EXPOSE 80

# Define o comando para executar a aplicação
ENTRYPOINT ["dotnet", "PolarisContacts.dll"]
