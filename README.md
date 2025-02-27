# 🪐 Cosmos

Projeto desenvolvido para o desafio fullstack da empresa [+A Educação.](https://maisaedu.com.br/)

## Descrição

O projeto tem como objetivo gerenciar a matrícula de estudantes de uma instituição de ensino. Os requisitos estão contídos [nesse repositório.](https://github.com/grupo-a/orbita-challenge-full-stack-web)

O sistema foi desenvolvido utilzando **.NET 8** no backend e **Nuxt 3** no front end. Mais abaixo estará a estrutura e as bibliotecas utilizadas.

## Executando o Projeto

### Requisitos

- .NET 8 (8.0.4)
- Node Js (v20.18.0)
- PostgreSQL (15^)
- PNPM (9.15.3) --opcional

### Clone o repositório

```bash
git clone git@github.com:murilo-oli/cosmos-orbita-challenge.git
```

### Iniciando o servidor

Acesse a pasta server:

```bash
cd ./server
```

Crie um arquivo **.env** e preencha as variáveis de ambiente abaixo:

```env
DEFAULT_FIRST_PASS="3C42822BB09F8BD829A0460F12E19DF3-085E00DFD40DB3AE" //mantenha essa senha, ela pode ser alterada futuramente
JWT_SECRET= crie_uma_secret_jwt

CONNECTION_STRING="Host=seu_host_postgresql;Port=sua_porta_postgresql;Database=CosmosDB;Username=seu_usuario_postgresql;Password=senha_do_usuario"
DB_HOST="seu_host_postgresql"
DB_PORT=sua_porta_postgresql
DB_NAME=CosmosDB
DB_USERNAME=seu_usuario_postgresql
DB_PASSWORD=senha_do_usuario
```

Acesse o projeto da API e execute o sistema (o sistema executa as migrations automaticamente):

```bash
cd ./Cosmos.API

dotnet watch run
```

Isso abrirá uma página em seu navegador, adicione "/swagger".

### Acesse a pasta "web" e instale as dependências (estou usando pnpm)

```bash
cd ./web

pnpm install
```

### Crie um arquivo ".env" para registrar a BASE_URL do servidor

```env
API_BASE_URL=http://localhost:porta_do_servidor/api
```

### Execute o client

```bash
pnpm dev -o
```

Se tudo correu bem, você conseguirá visualizar a tela de login. Utilize o usuário abaixo para testar o sistema:

```bash
email: admin@cosmos.com

senha: admin
```

### Pendência

Existe também um arquivo **Dockerfile** e um **docker-compose.yaml** na pasta "server" que executam o backend, porém ainda estão incompletos. Caso queira utilizar, basta executar o comando abaixo na pasta "server"

```bash
docker-compose up --build -d
```

## Arquitetura e Libs

O backend foi construido seguindo o padrão Repository, aliado a estrutura de camadas do padrão DDD, separando a solução nos projetos:

- Cosmos.Domain
  - Entidades;
  - Enums;
- Cosmos.Infrastructure:
  - Repositories;
  - Database Context;
  - Migrations;
- Cosmos.Application:
  - Services;
  - Geração e Validação de Tokens;
  - Encriptação e Validação de senhas;
  - DTOs
- Cosmos.IOC:
  - Injeção de Dependências;
  - Configurações de Autenticação da API;
- Cosmos.API:
  - Controllers e Rotas;

Já o client, foi construído utilizando **Nuxt 3** e **Vuetify**. As validações de schemas foram feitas atravéz da biblioteca **Zod**, os estados globais são gerenciados pela biblioteca **Pinia** e as requisições de API seguem o padrão Repository, através da biblioteca **oFetch**.

Além disso, o sistema conta com automações de commits utilizando **Husky.**
