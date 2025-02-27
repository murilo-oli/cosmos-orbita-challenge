# Comentários sobre o Projeto

## Arquitetura

Decidi construir o backend seguindo o padrão de projeto Repositpry e as estrutura de camadas do padrão DDD, pensando em desacoplamento e compartilhamento das funcionalidades. A estrutura seguida foi a seguinte:

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

Já no client web foi construído com o framework Nuxt e o system design requisitado foi o Vuetify. Utilizei Pinia para controle de estados globais, Zod para validação de tipos e interfaces e a biblioteca oFetch para facilitar as requisições da API. Através da lib oFetch, construí uma Factory que me permitiu abstrair as requisições em um padrão bem semlhante ao Repository, utilizado no backend, facilitando o desenvolvimento.

## Bibliotecas de Terceiros

**Husky:** responsável por criar automações GIT, principalmente para validar o build antes de enviar um push.

**Pinia:** responsável por gerenciar os estados globais da aplicação;

**Zod:** responsável por validar os schemas e interfaces dos formulários enviados para a API

**oFetch:** responsável por executar as requisições para a API

**vue-the-mask**: responsável por aplicar mascaras ao input do CPF do usuário

**DotNetEnv:** utilizada para a leitura do arquivo .ENV, responsável por ler as variáveis de ambiente compartilhada entre os projetos da solução;

**xUnit:** utilizada para criar testes unitários;

## Implementações Futuras

Caso houvesse mais tempo, além de melhorar a interface e simplificar os componentes da web, eu implementaria as seguintes funcionalidades:

- Finalização do CRUD de Usuários (o de estudantes está 100% finalizado);
- Aplicação de Testes Unitários nas demais camadas do sistema;
- Internacionalização do sistema;
- Implementação da funcionalidade de seleção de avatares para os usuários;
- FInalização da dockerização;
- Deploy em uma VPS.

## Requisitos Obrigatórios

Ao meu ver, todos os requisitos obrigatórios foram entregues.
