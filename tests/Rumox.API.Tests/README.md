
## Frameworks
- xUnit
- Bogus
- FluentAssertions
- Moq

## Como usar
Para executar os testes de integra��o da API deve ser inicializado a infraestutura como banco de dados pelo docker-compose de test.
- *Op��o 1*: executar diretamente o arquivo build-test.sh atraves de um terminal sh
- *Op��o 2*: pode ser executado o seguinte comando para levantar o docker-compose de teste, na pasta raiz da solu��o:
```
docker-compose -f docker-compose.yml -f docker-compose.test.yml up -d
```

## Decis�es t�cnicas
- O(s) banco de dado(s) s�o resetados em todo nova inicializa��o de teste, de acordo com seu contexto
- O teste de integra��o por agora executa apenas testes que retornar�o sucesso na execu��o
- Testes para criar novos registros executam uma repeti��o de 10x, realizando assim 10 vezes o mesmo teste para que se crie 10 registros
- Os testes mant�m em geral uma ordem, pois algumas entidades dependem de outras, exemplo: para registrar um produto precisa-se previamente de categorias registradas, sendo assim, � recomendado executar todos os testes de integra��o em conjunto
- Ao tentar obter um token de usu�rio sempre ir� cadastrar o usu�rio caso o mesmo n�o esteja ainda cadastrado
- O teste de login ir� apenas validar se o login foi realizado com sucesso ao tentar obter um UsuarioLogado ou UsuarioToken da fixture de Integra��o

### D�bitos t�cnicos
- Refatorar funcionalidade que realiza a limpeza dos dados antes de inicializar os testes