# RUMOX
Aplicação para fins de estudos utilizando diversas tecnologias, simulando um sistemas de vendas com catálogo de produtos, clientes, logística, marketing e pagamentos.

# O projeto

O projeto é desenvolvido em C# utilizando o framework .NET Core 3.1 no seu back-end, e possívelmente será utilizado Angular para o front-end.

## Momento atual

No momento atual está implementado alguns padrões, técnicas e modelagem seguindo:

- DDD
- Clean Code
- Design Patterns
- Domain Notifications

Também utiliza algumas bibliotecas como:

- Swagger *(para documentação da API)*
- JWT *(para autenticação de usuários)*
- AutoMapper *(para mapeamento de entitades à viewmodel)*
- MongoDB Driver
- Entity Framework Core *(para os bancos de dados relacionais)*
- Pomelo MySQL *(para integração do entity framework core com o MySQL)*
- Polly *(atualmente apenas nos serviços de cache)*
- MediatR *(para as domain notifications)*
- FluentValidation *(para validação de entidades)*

Os bancos de dados utilizados:

- MongoDB
- MySQL
- Redis *(para serviços de cache)*

Ferramentas e bibliotecas utilizadas nos testes:

- xUnit
- Bogus
- FluentAssertions
- Moq
- AutoMoq

**Também há diversas decisões técnicas e alguns débitos técnicos destacados dentro das seções de cada projeto, segue:**
1. [API](https://github.com/pedrogutierres/rumox/blob/master/src/API/README.md) - [Doc Tests](https://github.com/pedrogutierres/rumox/blob/master/tests/Rumox.API.Tests/README.md)
1. [Catalogo](https://github.com/pedrogutierres/rumox/blob/master/src/Catalogo/README.md) - [Doc Tests](https://github.com/pedrogutierres/rumox/blob/master/tests/Catalogo.Domain.Tests/README.md)
1. [CRM](https://github.com/pedrogutierres/rumox/blob/master/src/CRM/README.md) - [Doc Tests](https://github.com/pedrogutierres/rumox/blob/master/tests/CRM.Domain.Tests/README.md)

## Para executar

Atualmente o docker está implementado apenas para levantar o ambiente de testes para a execução dos testes de integração, [segue como fazer](https://github.com/pedrogutierres/rumox/tree/master/tests/Rumox.API.Tests)

~~Ainda não está implementado o deploy do aplicativo através do docker, em breve...~~

## Futuro

Segue alguns frameworks, bibliotecas, padrões, tecnologias e outros serviços, que possívelmente serão adotados:

- CQRS
- ELK (Logs)
- RabbitMQ
- Kafka
- Dapper
- RavenDB
- SQL Server
- Firebase
- Neo4J
- MS Orleans
- Chatbot
- Testes de carga com JMeter (e análise APDEX)
