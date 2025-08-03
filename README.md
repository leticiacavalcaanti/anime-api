# AnimeApp 🎬

Projeto em ASP.NET 9 + SQL Server para gerenciamento de animes. Esta aplicação expõe uma Web API RESTful com suporte a Docker e Swagger.

## 🧬 Tecnologias

- ASP.NET Core 9
- Entity Framework Core
- SQL Server (via Docker)
- MediatR
- AutoMapper
- Serilog
- Docker + Docker Compose

---

## 🚀 Como rodar o projeto

### 1. Faça o fork e clone o repositório

```bash
git clone https://github.com/leticiacavalcaanti/anime-api
cd anime-api
```

### 2. Configure o ambiente
O projeto já está preparado com um docker-compose.yml e um Dockerfile prontos para uso.

### 3. Suba os containers com Docker Compose
```bash
docker-compose up --build -d
```
Esse comando irá:
- Construir a imagem da API usando o .NET SDK 9.0
- Subir um container com a API na porta 5000
- Subir um container com SQL Server na porta 1433
- Criar o banco AnimeDb com o usuário sa e a senha Your_password123
- Aguardar o banco estar pronto antes de iniciar a API (via healthcheck)

### 4. Acesse a documentação da API
Depois que os containers estiverem no ar, acesse: http://localhost:5000/swagger
Você verá a documentação interativa (Swagger UI) com todas as rotas disponíveis.

## 🛠️ Estrutura do Projeto

```
AnimeApp/
├── AnimeApp.API/              # Projeto principal da Web API
│   └── Dockerfile             # Dockerfile da API
├── AnimeApp.Application/      # Camada de aplicação (DTOs, comandos)
├── AnimeApp.Domain/           # Entidades e exceções do domínio
├── AnimeApp.Infra/            # Repositórios e contexto EF Core
├── AnimeApp.Tests              # Testes da Aplicação
├── docker-compose.yml         # Orquestração dos serviços Docker

```

## 🔒 Connection String
- A string de conexão está definida no docker-compose.yml com:
```
ConnectionStrings__DefaultConnection: Server=sqlserver;Database=AnimeDb;User Id=sa;Password=Your_password123;
```

## 🧪 Testes
Os testes unitários estão localizados no projeto de testes ```AnimeApp.Test``` 
Execute via terminal:
```bash
dotnet test
```
