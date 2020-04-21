docker-compose -f docker-compose.yml -f docker-compose.dev.yml down
docker-compose -f docker-compose.yml -f docker-compose.dev.yml up -d

# rebuild
# docker-compose -f docker-compose.yml -f docker-compose.infra.yml up -d

# down
# docker-compose -f docker-compose.yml -f docker-compose.infra.yml down

# down with volume
# docker-compose -f docker-compose.yml -f docker-compose.infra.yml down -v