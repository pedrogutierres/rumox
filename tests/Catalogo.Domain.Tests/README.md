
## Frameworks
- xUnit
- Bogus
- FluentAssertions
- Moq

## Decis�es t�cnicas
- O(s) banco de dado(s) s�o resetados em todo nova inicializa��o de teste, de acordo com seu contexto
- O teste de integra��o por agora executa apenas testes que retornar�o sucesso na execu��o
- Testes para criar novos registros executam uma repeti��o de 10x, realizando assim 10 vezes o mesmo teste para que se crie 10 registros
- Os testes mant�m em geral uma ordem, pois algumas entidades dependem de outras, exemplo: para registrar um produto precisa-se previamente de categorias registradas, sendo assim, � recomendado executar todos os testes de integra��o em conjunto