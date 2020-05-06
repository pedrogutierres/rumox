
## Bibliotecas
- xUnit
- Bogus
- FluentAssertions
- Moq

## Como usar
Para executar os testes de integração da API deve ser inicializado a infraestutura como banco de dados pelo docker-compose de test.
- *Opção 1*: executar diretamente o arquivo build-test.sh atraves de um terminal sh
- *Opção 2*: pode ser executado o seguinte comando para levantar o docker-compose de teste, na pasta raiz da solução:
```
docker-compose -f docker-compose.yml -f docker-compose.test.yml up -d
```

## Decisões técnicas
- O(s) banco de dado(s) são resetados em todo nova inicialização de teste, de acordo com seu contexto
- O teste de integração por agora executa apenas testes que retornarão sucesso na execução
- Testes para criar novos registros executam uma repetição de 10x, realizando assim 10 vezes o mesmo teste para que se crie 10 registros
- Os testes mantém em geral uma ordem, pois algumas entidades dependem de outras, exemplo: para registrar um produto precisa-se previamente de categorias registradas, sendo assim, é recomendado executar todos os testes de integração em conjunto
- Ao tentar obter um token de usuário sempre irá cadastrar o usuário caso o mesmo não esteja ainda cadastrado
- O teste de login irá apenas validar se o login foi realizado com sucesso ao tentar obter um UsuarioLogado ou UsuarioToken da fixture de Integração

### Débitos técnicos
- Refatorar funcionalidade que realiza a limpeza dos dados antes de inicializar os testes