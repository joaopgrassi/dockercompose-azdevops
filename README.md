# Using `docker-compose` with your ASP.NET + EF Core integration tests

This repo contains the ASP.NET Core app that I use to demonstrate during a series of blog posts I've been writing about integration tests. You can check the series/posts here: [Integration tests in ASP.NET Core](https://blog.joaograssi.com/integration-tests-in-asp-net-core/).

[![Azure DevOps builds (branch)](https://img.shields.io/azure-devops/build/joaopgrassi/c250f6e5-f7df-4042-a3fc-f5f7e4d18a47/3/main?label=az-pipelines)](https://dev.azure.com/joaopgrassi/BlogApp/_build?definitionId=3)


## Requirements

- Docker
- .NET Core SDK 3.1.x
- Some cool terminal


## Running the tests locally

1. Start the Docker container for SQL Server: `docker-compose up`

2. Run `dotnet test`
