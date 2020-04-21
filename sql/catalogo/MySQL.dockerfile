FROM mysql:5.7

COPY ["dump/dump.sql", "/docker-entrypoint-initdb.d"]