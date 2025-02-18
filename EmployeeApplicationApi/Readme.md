#downloading kafka and docker compose
curl --silent --output docker-compose.yml https://raw.githubusercontent.com/confluentinc/cp-all-in-one/6.1.1-post/cp-all-in-one/docker-compose.yml



#starting and downloading all the containers
docker compose up -d