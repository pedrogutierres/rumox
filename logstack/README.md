
## Enterprise Application Log com ELK

Baseado no projeto do Luiz Carlos Faria que pode ser acessado em: https://github.com/docker-gallery/EnterpriseApplicationLog

## Para executar

Para levantar os serviços de log:

```
git clone https://github.com/pedrogutierres/rumox.git
cd rumox/logstack
docker network create elk
docker-compose -f docker-compose.log.yml down -v
docker-compose -f docker-compose.log.yml up --build -d
```

ou execute o arquivo *up-logstack.sh*
