# Fintech Core Banking API

A modular ASP.NET Core Web API simulating a retail banking backend, built for scalability and experimentation in financial applications. This API supports clients, instruments, portfolios, trades, and investments—powered by a PostgreSQL relational schema.

## Features
- Modular controller design using EF Core and async LINQ queries
- Swagger UI 
- Filter, Sort and Pagination features added
- PostgreSQL database integration
- JWT authentication setup is in progress

## API Endpoints 
- Use /swagger to test

## PostgreSQL Setup
- Create database named postgres
- Run the Create and Insert queries from `schema.sql` and `seed.sql` files to set up tables and data 
- Configure connection string in appsettings.json:

## JWT Authentication (in progress)
The login endpoint /api/auth/login is under development. Token validation middleware and [Authorize] protection will be re-enabled once finalized. For now, API testing is fully open in Swagger.

## Author 
Bhagya Sri Ramkumar