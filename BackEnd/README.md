

1 - para criar a migration

abra o terminal e execute o comando:

a - dotnet ef migrations add InitialCreate -p GestaoCatalogo.Infrastructure -s GestaoCatalogo.Api

2 - Atualizar Banco
a - dotnet ef database update -p GestaoCatalogo.Infrastructure -s GestaoCatalogo.Api