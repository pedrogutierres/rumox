docker network create elk
docker-compose -f docker-compose.log.yml down -v
docker-compose -f docker-compose.log.yml up --build -d