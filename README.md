Ateliê da Transformação
Ateliê da Transformação é uma vitrine digital integrada a um painel administrativo, desenvolvida com uma arquitetura em camadas limpa utilizando .NET 8. Permite que clientes naveguem por produtos artesanais, filtrem por categoria e gerem automaticamente um link do WhatsApp com mensagem pré‑preenchida contendo nome e preço do item. O back‑office oferece gerenciamento seguro de estoque, produtos e categorias.

Equipe e Responsabilidades por Camada

Camada | Responsável | Responsabilidades

Domain | Murilo |	Define os modelos de negócio centrais (Product, ProductCategory) e contratos abstratos de repositório (IProductRepository, IProductCategoryRepository). Contém apenas lógica de negócio, sem dependências externas.

Application | Renan |	Implementa serviços de caso de uso (ProductService, ProductCategoryService, WhatsAppService), mapeamentos DTO e orquestra o fluxo entre Domain e Infrastructure.

Infrastructure | Igor | Fornece implementações do EF Core, AtelieDaTransformacaoDbContext, configurações Fluent API e dados de seed. Gerencia todas as questões de persistência.

API	| Murilo | Expõe endpoints RESTful, documentados com Swagger, e utiliza os serviços da camada Application.

UI (Web) | Renan / Igor | Front‑end ASP.NET Core MVC para a vitrine pública e o painel administrativo (/Admin). Usa Bootstrap 5, jQuery para validações no cliente e consome a API.


--------------------------------


Tecnologia Utilizada

Tecnologia | Versão | Papel

.NET | 8.0 | Framework principal

ASP.NET Core MVC | 8.0 | Interface web pública e painel administrativo

ASP.NET Core Web API | 8.0 | Serviços REST

Entity Framework Core | 8.0.9 | ORM e migrações

SQL Server LocalDB	–	Banco de dados de desenvolvimento

ASP.NET Core Identity | 8.0.9 | Autenticação e gerenciamento de papéis

Swashbuckle (Swagger) | 8.x | Documentação interativa da API

Bootstrap | 5.x | UI responsiva

jQuery	–	Validações no cliente


--------------------------------


Executando o Projeto

Pré‑requisitos
  SDK .NET 8 instalado
  SQL Server LocalDB em execução (a string de conexão padrão já funciona)

Passos

1.Restaurar dependências
  dotnet restore

2.Aplicar migrações (Infrastructure → API)
  dotnet ef database update \
      --project AtelieDaTransformacao.Infrastructure \
      --startup-project AtelieDaTransformacao.API

3.Iniciar os serviços (abrir dois terminais)
  API
    dotnet run --project AtelieDaTransformacao.API
    Swagger disponível em https://localhost:{PORT}/swagger
  Web UI
    dotnet run --project AtelieDaTransformacao.UI
    Vitrine pública: https://localhost:{PORT}
    Painel administrativo: https://localhost:{PORT}/Admin


--------------------------------


Modelo de Dados (simplificado)

Produto
  Id (int, PK)
  Title (string, obrigatório)
  Description (string)
  Price (decimal)
  Image (string, URL ou caminho)
  CategoryId (int, FK → ProductCategory)
  IsFeatured (bool)
  IsAvailable (bool)
  StockQuantity (int)
  CreatedAt (DateTime)

Categoria de Produto
  Id (int, PK)
  Name (string)
  Description (string)
  Products – Navegação ICollection<Product>


--------------------------------


Segurança e Autenticação
  ASP.NET Core Identity gerencia usuários e papéis.
  Acesso ao /Admin e a todos os endpoints que modificam dados requer a função Admin.
  Na primeira execução, o SeedData cria um usuário administrador a partir das configurações em appsettings.json.

Credenciais padrão do administrador (alterar após o primeiro login):

Usuário: admin@atelie.com
Senha: Admin@Atelie123


--------------------------------


Visão Geral da API (endpoints selecionados)

Módulo | Método | Rota
Auth | POST | /api/auth/register
Auth | POST | /api/auth/login
Auth | POST | /api/auth/desktop-login
Auth | GET | /api/auth/me
Products | GET | /api/products
Products | GET | /api/products/{id}
Products | POST | /api/products
Products | PUT | /api/products/{id}
Products | DELETE | /api/products/{id}
Categories | GET | /api/categories
Categories | POST | /api/categories
Categories | PUT | /api/categories/{id}
Categories | DELETE | /api/categories/{id}
Orders | GET | /api/orders
Orders | PUT | /api/orders/{id}/status
Users | GET | /api/users
Users | POST | /api/users/create-desktop
Users | POST | /api/users/deactivate/{id}
Users | POST | /api/users/activate/{id}
Users | DELETE | /api/users/{id}


--------------------------------


Contribuindo
1. Faça um fork do repositório.
2. Crie uma branch para a feature (git checkout -b feature/nova-funcionalidade).
3. Garanta que a solução compile e que todos os testes passem.
4. Abra um Pull Request descrevendo claramente as alterações.
