# 🛒 EcommerceApi

Projeto backend desenvolvido em **.NET 8 (C#)** e **Entity Framework Core** para o gerenciamento de pedidos e produtos de um e-commerce, utilizando o **SQL Server** como banco de dados local.

---

## 🚀 Tecnologias usadas

- **C# / .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core (SQL Server 8.0.8)**
- **Swashbuckle / OpenAPI (Swagger)**

---

## ⚙️ Como rodar o projeto localmente


### 1. Clonar o repositório

```bash

git clone https://github.com/panyfmc/EcommerceApi.git
cd EcommerceApi

``` 


### 2. Configurar a String de Conexão (Connection String)

No seu arquivo appsettings.json, certifique-se de que a string de conexão aponta para a sua instância local do SQL Server. Exemplo padrão:

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

```


### 3. Restaurar as dependências (Packages)

```bash

dotnet restore

```


### 4. Rodar as migrações para criar o Banco de Dados

Caso utilize a interface de linha de comando (dotnet ef):

```bash
dotnet ef database update
```


### 5. Rodar servidor

```bash
dotnet run
```

O servidor iniciará localmente e a documentação interativa do Swagger poderá ser acessada em:
http://localhost:5111/swagger/index.html


## 📦 Endpoints principais

### Pedidos
- **GET /api/pedidos** → Listar todos os pedidos
- **POST /api/pedidos** → Criar um novo pedido
- **GET /api/pedidos/{id}** → Buscar detalhes de um pedido por ID
- **PUT /api/pedidos/{id}** → Atualizar todos os dados de um pedido por ID
- **DELETE /api/pedidos/{id}** → Deletar um pedido por ID
- **PATCH /api/pedidos/{id}/status** → Atualizar parcialmente o status de um pedido


### Produtos
- **GET /api/produtos** → Listar todos os produtos
- **POST /api/produtos** → Criar um novo produto
- **GET /api/produtos/{nome}** → Buscar detalhes de um produto pelo Nome
- **GET /api/produtos/{nome}** → Deletar um produto pelo Nome
- **GET /api/produtos/{nome}** → Atualizar parcialmente o valor de um produto pelo Nome


## 📌 Observações importantes

- Banco de Dados: O projeto está configurado para utilizar o SQL Server no localhost. Certifique-se de que o serviço do seu SQL Server esteja ativo antes de aplicar as migrations ou rodar a API.

- ORM: O mapeamento de dados e o controle transacional são gerenciados nativamente pelo Entity Framework Core.

- Documentação: Toda a estrutura de rotas e payloads esperados podem ser visualizados diretamente na interface gráfica gerada pelo Swagger.
