
## Bibliotecas
- Polly

## Decisões técnicas
- A biblioteca polly está sendo utilizada para controlar retry e circuit break nas consultas ao serviço de cache, atualmente definindo que o serviço de cache poderá estar fora do ar e isso não irá expor excessões para a aplicação.

### Débitos técnicos
- Repensar sobre como está utilizando o polly