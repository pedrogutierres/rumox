
## Bibliotecas
- AutoMapper
- Swagger

### Url
Por padrão deverá ser utilizado a url sem HTTPS pois a segurança deveria estar na fronteira da aplicação
Url: http://localhost:5000
Url Swagger: http://localhost:5000/swagger
Url HealthCheck UI: http://localhost:5000/api/healthchecks-ui

## Decisões técnicas
- ViewModels para registrar nova entidade terá seu ID criado internamente
- A ModelState será validada pelo próprio middleware do ASP.NET Core porém teve seu retorno modificado para manter o padrão do ProblemDetails da aplicação *(localizado no arquivo ApiConfiguration.cs)*
- Os métodos POST, PUT, PATCH e DELETE em geral retornam status 200 (Ok) com ID da transação quando houver sucesso na ação, e status 400 (BadRequest) com um ProblemDetails (RFC 7807) contendo uma lista de erros (errors) quando houver um problema na ação
- O método PUT será utilizado para atualizar entidades mesmo que parcialmente, e também não irá registrar uma nova entidade caso a mesma ainda não exista
- O método PATCH será utilizado quando a atualização da entidade for partes menores ou em um único ponto específico, exemplo: ativar ou inativar
- O método GET em geral irá retornar status 200 (Ok) quando encontrar o recurso ou status 404 (NotFound) quando não encontrar, se for uma lista mesmo que vazia irá retornar status 200 (Ok)
- Clientes e Usuários refletem a mesma entidade de negócio, visualizada no contexto CRM como clientes
- Os token de usuários são gerados baseados no JWT

### Débitos técnicos
- Implementar EventStore corretamente (hoje está apenas mockado)
- Refatorar local da secret key da autenticação JWT

#### Outros débitos
- Definir melhor a linguagem ubiqua de clientes/usuários
