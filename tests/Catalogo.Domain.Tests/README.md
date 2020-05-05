
## Frameworks
- xUnit
- Bogus
- FluentAssertions
- Moq

## Decisões técnicas
- Os testes de serviços são mockados através do Moq
- As validações de entitades não são ainda testadas pelo projeto, mas tem seu teste garantido pelos testes do próprio framework (FluentAssetion)
- Repositórios de dados (Infra) não são testados pois o mesmo ocorre no teste de integração