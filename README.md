# SupplyChainTransparency API

Este projeto implementa uma API RESTful em .NET Core 8 para gerenciar dados relacionados à Transparência na Cadeia de Suprimentos, com foco em aspectos ESG (Ambiental, Social e Governança). A API inclui autenticação JWT, validação de dados, tratamento de exceções e otimização de consultas com paginação.

## Configuração do Banco de Dados

O projeto foi configurado para ser flexível em relação ao banco de dados, suportando **SQLite** (para portabilidade, especialmente em Docker) e **SQL Server LocalDB** (para desenvolvimento em ambiente Windows).

### 1. SQLite (Recomendado para Docker e Portabilidade)

Esta é a configuração padrão do projeto no código-fonte fornecido. O SQLite é um banco de dados leve e baseado em arquivo, ideal para desenvolvimento e execução em containers Docker, pois não requer um servidor de banco de dados separado.

**String de Conexão (appsettings.json):**

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=SupplyChainTransparency.db"
}
```

**Configuração no Program.cs:**

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
```

**Arquivos de Migração:**

Os arquivos de migração para SQLite estão localizados na pasta `Migrations` do projeto.

### 2. SQL Server LocalDB (Para Desenvolvimento em Windows com Visual Studio)

Se você estiver desenvolvendo em um ambiente Windows com Visual Studio, pode optar por usar o SQL Server LocalDB. Esta opção oferece uma experiência de desenvolvimento integrada com o Visual Studio.

**Passo a Passo para Configurar SQL Server LocalDB:**

1.  **Adicionar Pacote NuGet:**
    Abra o terminal na raiz do projeto e execute:
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.5
    ```

2.  **Configurar String de Conexão (appsettings.json):**
    Altere a seção `ConnectionStrings` no `appsettings.json` para:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SupplyChainTransparencyDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3.  **Configurar Program.cs:**
    Altere a configuração do `DbContext` no `Program.cs` para usar `UseSqlServer`:
    ```csharp
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    ```

4.  **Remover Migrações Antigas e Adicionar Novas Migrações para SQL Server:**
    *   Exclua a pasta `Migrations` existente na raiz do projeto.
    *   No terminal, na raiz do projeto, execute:
        ```bash
        dotnet ef migrations add InitialCreateSqlServer --project SupplyChainTransparency.csproj --startup-project SupplyChainTransparency.csproj
        ```

5.  **Aplicar Migrações ao Banco de Dados SQL Server LocalDB:**
    No terminal, na raiz do projeto, execute:
    ```bash
    dotnet ef database update --project SupplyChainTransparency.csproj --startup-project SupplyChainTransparency.csproj
    ```

## Como Executar o Projeto

### 1. Execução Local (com SQLite)

1.  **Descompacte** o arquivo `SupplyChainTransparency_SourceCode_Updated.zip`.
2.  Abra o terminal ou prompt de comando na pasta `SupplyChainTransparency`.
3.  **Restaure as dependências** (se necessário):
    ```bash
    dotnet restore
    ```
4.  **Construa o projeto**:
    ```bash
    dotnet build
    ```
5.  **Aplique as migrações** (isso criará o arquivo `SupplyChainTransparency.db` se ele não existir):
    ```bash
    dotnet ef database update --project SupplyChainTransparency.csproj --startup-project SupplyChainTransparency.csproj
    ```
6.  **Execute a aplicação**:
    ```bash
    dotnet run
    ```
    A API estará disponível em `https://localhost:7000` (ou outra porta configurada).

### 2. Execução via Docker (com SQLite)

O `Dockerfile` fornecido configura o projeto para usar SQLite, tornando-o portátil para qualquer sistema operacional com Docker instalado.

1.  **Descompacte** o arquivo `SupplyChainTransparency_SourceCode_Updated.zip`.
2.  Abra o terminal ou prompt de comando na pasta `SupplyChainTransparency` (onde o `Dockerfile` está localizado).
3.  **Construa a imagem Docker**:
    ```bash
    docker build -t supplychaintransparency .
    ```
4.  **Execute o contêiner Docker**:
    ```bash
    docker run -p 5000:80 supplychaintransparency
    ```
    A API estará disponível em `http://localhost:5000`.

## Credenciais de Segurança

### Credenciais do Banco de Dados

*   **SQL Server LocalDB:** Utiliza **Autenticação do Windows (`Trusted_Connection=True`)**, o que significa que não há nome de usuário ou senha explícitos no código para o banco de dados. Ele usa as credenciais do usuário logado no sistema operacional.
*   **SQLite:** Não requer credenciais, pois é um banco de dados baseado em arquivo.
*   **Oracle FIAP:** As credenciais (`rm558521` / `130399`) são específicas para o Oracle e não são usadas nas configurações padrão de SQLite ou SQL Server LocalDB. Se você optar por usar o Oracle, precisará configurar a string de conexão conforme o tutorial anterior.

### Credenciais da API (JWT)

As credenciais para a autenticação JWT da API estão no `appsettings.json`:

```json
"Jwt": {
  "Key": "ThisIsAVeryStrongSecretKeyForJwtAuthentication",
  "Issuer": "SupplyChainTransparency",
  "Audience": "SupplyChainTransparencyUsers"
}
```

Para fins de demonstração, a `Key` é uma string simples. Em um ambiente de produção, esta chave deve ser protegida e gerenciada de forma segura (ex: variáveis de ambiente, Azure Key Vault).

## Como Testar a API (com Postman/Insomnia)

1.  **Importe a Coleção:**
    *   Descompacte o arquivo `SupplyChainTransparency_Postman_Collection.zip`.
    *   Importe o arquivo `SupplyChainTransparency_Postman_Collection.json` para o seu cliente Postman ou Insomnia.

2.  **Obtenha um Token JWT:**
    *   Na coleção importada, localize a requisição `Login` (em `Auth`).
    *   Envie a requisição com `username`: `testuser` e `password`: `testpassword`.
    *   A resposta conterá um token JWT. Copie este token.

3.  **Use o Token nas Requisições Protegidas:**
    *   A coleção já está configurada para usar uma variável de ambiente (`jwtToken`). Você precisará definir essa variável com o token que você copiou.
    *   Todas as outras requisições (ex: `Get Suppliers`, `Post Supplier`) estão configuradas para enviar este token no cabeçalho `Authorization` como `Bearer {{jwtToken}}`.

## Conteúdo da Entrega

Você receberá os seguintes arquivos ZIP:

*   `SupplyChainTransparency_SourceCode_Updated.zip`: Contém o código-fonte completo do projeto, incluindo o `Dockerfile` e os arquivos de migração para SQLite.
*   `SupplyChainTransparency_Postman_Collection.zip`: Contém o arquivo JSON da coleção Postman/Insomnia para testar a API.



## Pipeline CI/CD com GitHub Actions

Este projeto inclui um fluxo de CI/CD usando GitHub Actions, configurado no arquivo `.github/workflows/main.yml`. O pipeline realiza etapas de build, publicação e criação da imagem Docker, garantindo entregas automatizadas e seguras.

## docker-compose.yml

Arquivo simples para orquestração local, permitindo subir o container e persistir o banco SQLite:



