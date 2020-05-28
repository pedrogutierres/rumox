
## Bibliotecas
- MongoDB Driver

## Banco de dados
- MongoDB
- Redis *(integração do login de cliente)*

## Decisões técnicas
- O projeto CrossCutting Identity irá realizar ações externas como enviar e-mail de recuperação de senha para o cliente
- Eventos das entidades são criados com apenas aquilo que foi realizado na entidade (novo registro, uma alteração parcial)
- Eventos de exclusão das entidades são registrados com todos os dados da entidade

### Débitos
- Definir melhor se o contexto continuará sendo CRM