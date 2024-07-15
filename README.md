# mailservice

## setup:
```bash 
sudo docker login docker.sprun.ir
sudo chmod +x redeploy.sh && ./redeploy.sh
```

> go to server and run

```bash
sudo docker compose -f "docker-compose.yml" pull
sudo docker compose -f "docker-compose.yml" up -d
```

## install:
```bahs
dotnet add package MailKit
dotnet add package Newtonsoft.Json
dotnet add package MassTransit
dotnet add package MassTransit.RabbitMQ
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design && dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet tool install --global dotnet-ef
```

> ensure that you've already setup the `DOCKER_USERNAME`, `DOCKER_PASSWORD`, `SERVER_HOST`, `SERVER_PASSWORD`, `SERVER_USER` secret vars.
