
## Frameworks
- AutoMapper
- Swagger

## Decisões técnicas
- ViewModels para registrar nova entidade terá seu ID criado internamente
- A ModelState será validada pelo próprio ASP.NET Core porém teve seu retorno modificado para manter o padrão da aplicação
- Os métodos POST, PUT, PATCH e DELETE em geral retornam status 200 (Ok) com ID da transação quando houver sucesso na ação, e status 400 (BadRequest) com uma lista de erros ("errors") quando houver um problema na ação
- O método PUT será utilizado para atualizar entidades mesmo que parcialmente, e também não irá registrar uma nova entidade caso a mesma ainda não exista
- O método PATCH será utilizado quando a atualização da entidade for em um único ponto específico, exemplo: Ativar e Inativar
- O método GET em geral irá retornar status 200 (Ok) quando encontrar o recurso ou status 404 (NotFound) quando não encontrar, se for uma lista mesmo que vazia irá retornar status 200