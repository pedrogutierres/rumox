
Biblioteca de suporte para utilizar o logstack
A biblioteca visa facilidade a implementação de ILogger para ASP.NET Core e também de Middleware de log para as comunicações com a API

## Bibliotecas
- RabbitMQ

## Decisões técnicas
- A configuração de conexão com o RabbitMQ deve ser feita pelo appsettings da api em "Logging:EnterpriseLog:RabbitMQ"
- O EnterpriseLoggerProvider é um provider para utilizar o serviço de log do ASP.NET Core (ILogger<T>)
- O EnterpriseLogMiddleware é um middleware para implementar na API, definindo aqui algumas decisões como: logar todos os dados de contido no header do request, tamanho do request e response, ip do client, e também irá logar o corpo do request caso aconteça algum erro na aplicação.
