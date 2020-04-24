docker-compose -f docker-compose.yml -f docker-compose.test.yml down
docker-compose -f docker-compose.yml -f docker-compose.test.yml up -d

# rebuild
# docker-compose -f docker-compose.yml -f docker-compose.test.yml up -d

# down
# docker-compose -f docker-compose.yml -f docker-compose.test.yml down

# down with volume
# docker-compose -f docker-compose.yml -f docker-compose.test.yml down -v