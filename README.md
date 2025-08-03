# AnimeApp 🎬

Projeto em ASP.NET 9 + MySQL para gerenciamento de animes. Esta aplicação expõe uma Web API RESTful com suporte a Docker e Swagger.

## 🧬 Tecnologias

- ASP.NET Core 9
- Entity Framework Core (MySQL)
- MediatR
- AutoMapper
- Serilog
- Docker + Docker Compose

---

## 🚀 Como rodar o projeto

### 1. Faça o fork e clone o repositório

```bash
git clone https://github.com/seu-usuario/AnimeApp.git
cd AnimeApp
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
- Subir um container com MySQL na porta 3001
- Criar o banco AnimeDb com usuário root e senha root

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

## 🔒 Connection String (Docker vs Local)
- Em produção ou Docker Compose: já configurado via docker-compose.yml:
ConnectionStrings__DefaultConnection=Server=mysql;Port=3306;Database=AnimeDb;User=root;Password=root;
- Para desenvolvimento local (sem Docker Compose) atualize o appsettings.Development.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3001;Database=AnimeDb;User=root;Password=root;"
  }
}
```

## 🧪 Testes
Os testes unitários estão localizados no projeto de testes ```AnimeApp.Test``` 
Execute via terminal:
```bash
dotnet test
```
