docker network create elk
docker-compose -f docker-compose.log.yml down
docker-compose -f docker-compose.log.yml up --build -d