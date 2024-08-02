Neste projeto implementei um backend de blog em ASP.NET Core com suporte para WebSockets para notificações em tempo real. Utilizei o Entity Framework Core para gerenciamento de dados e o middleware para WebSockets para comunicação em tempo real.

Estrutura Básica do Projeto
O projeto é dividido em camadas, separadas em pastas (descritas em parênteses):

    - Models: Definições de classes de domínio (Post e User).(Models e Entities)
    - Data: Configuração do Entity Framework (DbContext).(Data e Migrations)
    - Controllers: Controladores para gerenciar autenticação e postagens.(Controllers)
    - Hubs: Classe para gerenciar a comunicação WebSocket.(WebSocket)
    - Middleware: Middleware para gerenciar conexões WebSocket.(Program.cs)

  Para rodar o projeto usando banco de dados local, basta fazer o procedimento abaixo:
  1- remover o conteudo dentro da pasta Migration
  2- No menu superior do VisualStudio, clicar em Tools>Nuget Package Manager> Package Manage Console
  3 - No Package Manage console, digitar :
    Add-Migration InitialCreate
    Update-Database

    
