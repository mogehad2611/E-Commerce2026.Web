# E-Commerce2026 API

A scalable ASP.NET Core Web API E-Commerce project built using Clean Architecture principles and enterprise-level backend development practices.

---

## Features

- Clean Architecture implementation
- Repository Pattern
- Unit Of Work Pattern
- Specification Pattern
- Generic Repository
- Entity Framework Core with SQL Server
- Pagination, Filtering & Sorting
- AutoMapper DTO Mapping
- Custom Exception Handling Middleware
- Validation & API Error Responses
- Swagger API Documentation
- Redis In-Memory Basket Module
- Authentication & Authorization
- ASP.NET Core Identity
- JWT Token Generation & Validation
- User Registration & Login
- Role & User Seeding
- Address Management
- Order Management Module
- Delivery Methods
- Stripe Payment Integration
- RESTful API Design
- Angular Frontend Integration Support
- Postman API Testing

---

## Technologies Used

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- AutoMapper
- Redis
- Stripe
- Swagger / OpenAPI
- ASP.NET Core Identity
- JWT Authentication

---

## Architecture

The project follows Clean Architecture principles with layered separation:

```text
E-Commerce2026
│
├── DomainLayer
├── ServiceAbstraction
├── Service
├── Persistence
├── Presentation
└── Shared
```

### Layers Overview

| Layer | Responsibility |
|---|---|
| DomainLayer | Entities, Interfaces, Contracts |
| ServiceAbstraction | Service Interfaces |
| Service | Business Logic |
| Persistence | Database Access & Repositories |
| Presentation | Controllers & APIs |
| Shared | Shared DTOs & Utilities |

---

## Design Patterns Used

- Repository Pattern
- Unit Of Work Pattern
- Specification Pattern
- Dependency Injection
- Factory Delegate Pattern

---

## Database

- SQL Server Database
- Entity Framework Core Migrations
- Data Seeding Support

---

## API Features

### Product Module

- Get All Products
- Get Product By Id
- Product Filtering
- Product Sorting
- Product Pagination
- Brand & Type Filtering

### Basket Module

- Redis Basket Storage
- Basket CRUD Operations

### Authentication Module

- Register
- Login
- JWT Authentication
- Authorization

### Order Module

- Create Orders
- Get Orders
- Delivery Methods
- Payment Processing

---

## Payment Integration

Stripe payment gateway integration is implemented for handling payment workflows.

---

## API Testing

API endpoints are tested using:

- Swagger UI
- Postman

---

## Swagger Endpoint

```text
https://localhost:{port}/swagger
```

---

## Project Status

The project is under active development and is being built incrementally while following backend best practices and scalable architecture concepts.

---

## Author

Muhammad Gehad