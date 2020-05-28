
## Bibliotecas
- Entity Framework Core
- Pomelo MySQL

## Banco de dados
- MySQL

## Decisões técnicas
- Não será validado no produto se a categoria existe, tal responsabilidade foi deixado para o relacionamento foreign key do banco de dados
- Eventos das entidades são criados com apenas aquilo que foi realizado na entidade (novo registro, uma alteração parcial)
- Eventos de exclusão das entidades são registrados com todos os dados da entidade