docker compose --profile all --profile testing build
docker compose --profile all --profile testing up --abort-on-container-exit e2etesting
docker compose --profile all --profile testing down