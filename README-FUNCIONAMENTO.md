# Ateliê da Transformação — versão funcional

## Arquitetura
- Desktop: Windows Forms .NET 8
- API: ASP.NET Core .NET 10
- Application: DTOs, interfaces e regras de negócio
- Domain: entidades e contratos
- Infrastructure: EF Core + SQL Server + Identity

## Login
Usuário administrador inicial:
- E-mail: `admin@atelie.com`
- Senha: `Admin@Atelie123`

Troque a senha antes de usar em produção.

## Banco
A API usa LocalDB:
`(localdb)\\MSSQLLocalDB`

Ao iniciar a API, as migrations são aplicadas automaticamente e as categorias iniciais são criadas.

## Executar
1. Abra a solução no Visual Studio.
2. Defina `AtelieDaTransformacao.API` como projeto de inicialização.
3. Execute o perfil `http` da API (`http://localhost:5112`).
4. Confirme que o Swagger abre.
5. Execute `AtelieDaTransformacao.Desktop`.

O Desktop já aponta para `http://localhost:5112/api/`.

## Funcionalidades
- Login JWT.
- Seed de usuário Admin.
- CRUD de produtos.
- Busca por nome/descrição.
- Filtro por categoria disponível na API.
- Controle de estoque.
- CRUD de categorias na API.
- Proteção de criação, edição e exclusão com role `Admin`.
- Confirmação antes da exclusão.
- Tratamento de erros 400/401/404/409/500.
- Migração automática do banco no startup.
