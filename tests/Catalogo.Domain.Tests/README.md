
## Frameworks
- xUnit
- Bogus
- FluentAssertions
- Moq

## Decisões técnicas
- O(s) banco de dado(s) são resetados em todo nova inicialização de teste, de acordo com seu contexto
- O teste de integração por agora executa apenas testes que retornarão sucesso na execução
- Testes para criar novos registros executam uma repetição de 10x, realizando assim 10 vezes o mesmo teste para que se crie 10 registros
- Os testes mantém em geral uma ordem, pois algumas entidades dependem de outras, exemplo: para registrar um produto precisa-se previamente de categorias registradas, sendo assim, é recomendado executar todos os testes de integração em conjunto