# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos da solução e dos projetos
COPY Cosmos.sln ./
COPY Cosmos.API/ Cosmos.API/
COPY Cosmos.Application/ Cosmos.Application/
COPY Cosmos.Domain/ Cosmos.Domain/
COPY Cosmos.Infrastructure/ Cosmos.Infrastructure/
COPY Cosmos.IOC/ Cosmos.IOC/

# Restaura as dependências
RUN dotnet restore

# Compila a aplicação
RUN dotnet publish Cosmos.API/Cosmos.API.csproj -c Release -o /app/published

# Etapa 2: Runtime otimizado
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos compilados da etapa de build
COPY --from=build /app/published ./

# Copia o arquivo .env para o diretório da aplicação
COPY .env .env

# Define variáveis de ambiente para garantir que o DotNetEnv carregue corretamente
ENV DOTNET_ENV_PATH="/app/.env"

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Cosmos.API.dll"]
