# Microcop User Management Service

## A fully Functionality of Microcop User Management REST API

This service was designed to facilitate CRUD operations for user management, exemplifying the development of a REST API in C#/.NET Core.

### The project is structured based on Clean Architecture, comprising four primary layers:

1 Domain - including the entities, value objects, and domain services.
2 Application - including application services, DTOs(data transfer objects) and mapper. It should reference the Domain layer.
3 Infrastructure - including the implementation of data access, logging, email, and other communication mechanisms. It should reference the Application layer.
4 Presentation (API) - The main project contains the presentation layer and implements the ASP.NET Core web API. It should reference the Application and Infrastructure layers.

#### This project demonstrates how to perform the following actions:

- Create a user
  - Validate user's password, email and username
- Get a user details by id
- Update a user details
- Delete a user
- Authentication user using ApiKey authorization
- File Logging for each request with specific format data

#### More details in tech stack, libraries and techniques that are using in development:

- Framework - EntityFramework Core
- Database - PostgreSQL
- Logging - Serilog
- Repository - Generic Repository
- Exceptions - Global Middleware
- Mapper - AutoMapper

## How to test this project in your local environment:

1. Clone this project from repository
2. Add your connection string for postgreSQL database in `Presentation/appsettings.json`
3. Run a command to apply migrations in database `dotnet ef  -p ./Infrastructure/ -s ./Presentation/ database update`
4. Run the project in Visual Studio or another editor

#### Short video from my testing cases
