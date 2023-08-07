# Desafio Técnico - Backend

Projeto resultado do desafio técnico backend, uma API que fará a persistência de dados de um quadro de kanban. Esse quadro possui listas, que contém cards.

## Principais recursos e tecnologias utilizadas no backend

1. NET 6 Web Api
2. AutoMapper
3. Entity Framework Core
4. Postgres
5. Swagger
5. xUnit
6. Docker/Docker-Compose

## Rodando o Backend

O back possui migration de banco de dados porém já é aplicada na inicialização da aplicação.

para rodá-lo, faça:

```console
> cd BACK
> docker-compose up
```

## Rodando o Frontend

O frontend de exemplo está disponibilizado na pasta FRONT.

Para rodá-lo, faça:

```console
> cd FRONT
> yarn
> yarn start
```
