# Using `docker-compose` with your ASP.NET + EF Core integration tests

This repo contains the ASP.NET Core app that I use to demonstrate during a series of blog posts I've been writing about integration tests. You can check the series/posts here: [Integration tests in ASP.NET Core](https://blog.joaograssi.com/integration-tests-in-asp-net-core/)


## Requirements

- Docker
- .NET Core SDK 3.1.x
- Some cool terminal


## Running the tests locally

1. Start the Docker container for SQL Server: `docker-compose up`

2. Run `dotnet test`
