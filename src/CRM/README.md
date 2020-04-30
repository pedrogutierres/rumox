
## Frameworks
- Entity Framework Core
- Pomelo MySQL

## Banco de dados
- MongoDB

## Decisões técnicas
- O projeto CrossCutting Identity irá realizar ações externas como enviar e-mail de recuperação de senha para o cliente
- Eventos são criados com apenas aquilo que foi realizado na entidade (novo registro, uma alteração parcial)
- Eventos de deleção são registrados com todos os dados da entidade