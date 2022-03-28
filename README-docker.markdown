## Docker Specific Commands

To list all images, use

```
docker image ls
```

To list all containers, use

```
docker container ls
```

To build a docker file with a tag, use

```
docker build -f DOCKER_FILE_LOCATION -t TAG_NAME .
```

To kill a container, use

```
docker container kill CONTAINER_NAME
```

To run a docker image as an interactive terminal, use

```
docker run -it DOCKER_IMAGE_NAME sh
```

#

## Docker Compose Specific Commands

To [re]build all images to running containers within a docker compose configuration, use

```
docker-compose up --build
```

To force all images to be recreated, use

```
docker-compose up --force-recreate
```

To run containers in detached mode, use

```
docker-compose up -d
```

To stop docker compose while it is running in detached mode, use

```
docker-compose stop
```
