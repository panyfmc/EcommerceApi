 # 🛒 EcommerceApi
 
 Projeto backend desenvolvido em .NET 8 (C#) e Entity Framework Core para gerenciamento de pedidos e produtos de um e-commerce.
 A aplicação segue boas práticas de arquitetura, utilizando DTOs (Data Transfer Objects) e Docker para orquestração completa do ambiente.

 ------------------------------------------------------------

 ## 🚀 Tecnologias Usadas

 - C# / .NET 8
 - ASP.NET Core Web API
 - Entity Framework Core (SQL Server)
 - Docker & Docker Compose
 - Swagger (OpenAPI)

 ------------------------------------------------------------

 ## ⚙️ Como Rodar o Projeto com Docker (Recomendado)

 A forma mais simples de executar o projeto é utilizando Docker.

 1. Clonar o repositório:
```bash
 git clone https:github.com/panyfmc/EcommerceApi.git
 cd EcommerceApi
```
 ------------------------------------------------------------

 2. Subir os containers:
```bash
 docker-compose down -v && docker-compose up --build
```
 - -v remove volumes antigos do banco
 - --build recompila a aplicação

 ------------------------------------------------------------

 3. Acessar Swagger:

 <http:localhost:8080/index.html>

 ------------------------------------------------------------

 ## 🛠️ Como Rodar Localmente (Sem Docker)

 1. Configurar Connection String (appsettings.json):
```bash
 {
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
 }
```
 ------------------------------------------------------------

 2. Restaurar dependências e aplicar migrations:
```bash
 dotnet restore
 dotnet ef database update
```
 ------------------------------------------------------------

 3. Executar aplicação:
```bash
 dotnet run
```
 Swagger local:
 <http:localhost:5111/swagger/index.html>

 ------------------------------------------------------------

 ## 📦 Endpoints Principais

 ----------------------------
 PEDIDOS (/api/pedidos)
 ----------------------------

 - GET    /api/pedidos              -> Listar pedidos
 - GET    /api/pedidos/{id}         -> Buscar pedido por ID
 - POST   /api/pedidos              -> Criar pedido
 - PUT    /api/pedidos/{id}         -> Atualizar pedido
 - PATCH  /api/pedidos/{id}/status  -> Atualizar status
 - DELETE /api/pedidos/{id}         -> Deletar pedido

 ----------------------------
 PRODUTOS (/api/produtos)
 ----------------------------

 - GET    /api/produtos             -> Listar produtos
 - GET    /api/produtos/{nome}      -> Buscar por nome
 - POST   /api/produtos             -> Criar produto
 - PUT    /api/produtos/{nome}      -> Atualizar produto
 - DELETE /api/produtos/{nome}     -> Deletar produto

 ------------------------------------------------------------

 ## 📌 Arquitetura e Decisões de Projeto

 - DTOs: isolamento das entidades do banco de dados
 - UUID (Guid): chaves únicas globais
 - Migrations automáticas no startup (Docker-ready)
 - Fluent API para mapeamento de tabelas
 - Arquitetura pronta para produção

 ------------------------------------------------------------

 ## 💡 Objetivo do Projeto

 - Boas práticas de backend
 - Arquitetura limpa
 - APIs REST bem estruturadas
 - Uso profissional de DTOs
 - Ambiente Docker replicável
