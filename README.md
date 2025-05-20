# 🧺 Basket Microservice

A modern and scalable **Basket Microservice** built with **ASP.NET Core 8** using an **N-Tier architecture** with elements of Clean Architecture principles. It manages shopping basket operations with **Redis for distributed caching and MySQL for persistence**. On checkout, it publishes a BasketCheckoutEvent to **RabbitMQ for asynchronous communication**. The design emphasizes separation of concerns, testability, and maintainability.

---

## 🚀 Features

- ✅ **ASP.NET Core 8 Web API** using REST principles (CRUD)
- ✅ **CQRS** with MediatR for command/query separation
- ✅ **Clean Architecture** for maintainability and testability
- ✅ **Redis** for distributed caching using **Cache-aside**, **Decorator**, and **Proxy** patterns
- ✅ **Scrutor** for applying the Decorator pattern via dependency injection
- ✅ **gRPC client** to consume Discount Microservice for price calculation
- ✅ **MassTransit with RabbitMQ** to publish `BasketCheckout` events
- ✅ **Carter** for minimal REST API endpoints
- ✅ **EF Core 8** for MySQL integration
- ✅ **Automapper** for clean mapping between entities and DTOs
- ✅ **Shared Contracts** for inter-service communication consistency

---

## 🏗️ Architecture Overview

This Basket Microservice follows an N-Tier architecture, incorporating some principles from Clean Architecture for enhanced separation of concerns and testability. The project structure is organized as follows:

1.  **API Layer** → Exposes RESTful endpoints using the Carter library for a lightweight and modular approach.
2.  **Application Layer** → Implements the core application logic using the CQRS pattern (Commands, Queries, and Handlers) facilitated by MediatR for in-process messaging.
3.  **Domain Layer** → Contains the central `Basket` entity and encapsulates core business rules and logic, independent of any specific framework or infrastructure.
4.  **Infrastructure Layer** → Handles interactions with external systems:
    - **EF Core** → Manages data persistence with the MySQL database.
    - **Redis** → Implements distributed caching for basket data to improve performance and scalability.
    - **RabbitMQ** → Facilitates asynchronous communication, particularly for publishing `BasketCheckoutEvent` upon successful checkout.
    - **Scrutor** → Simplifies the registration of decorators in the DI container, enabling clean application of the Decorator pattern for cache.
5.  **Contracts Layer** → Defines Data Transfer Objects (DTOs) used for communication between different layers and potentially other microservices. This ensures a consistent data structure across the application.
6.  **Shared Layer** → Contains common utilities and reusable components such as:
    - Behaviors → Implements MediatR pipeline behaviors for cross-cutting concerns.
    - CORS → Configures Cross-Origin Resource Sharing policies.
    - Exception Handling → Defines global exception handling mechanisms.

## 🧩 Design Patterns

- **CQRS** – For separation of write and read responsibilities
- **Decorator Pattern** – For wrapping Redis caching logic
- **Proxy Pattern** – Abstracts Redis operations behind interfaces
- **Cache-Aside Pattern** – Loads from DB on cache miss and stores in Redis
- **Unit of Work** – Manages and tracks all changes made to the database within a single business transaction. This ensures data consistency and simplifies transaction management, typically in conjunction with the Repository pattern.

## 🔁 Inter-Service Communication

- Consumes **Discount gRPC Service** to apply real-time discounts.
- Publishes `BasketCheckout` event to **RabbitMQ** via **MassTransit** for downstream services (e.g., Ordering).

---

## 📦 Migrations

### 🔧 Create a Migration

```bash

dotnet ef migrations add InitialCreate --project .\src\Basket.Infrastructure --startup-project .\src\Basket.Api --context ApplicationDbContext --output-dir Data\Migrations
```

### 🚀 Apply the Migration

```bash
dotnet ef database update  --project .\src\Basket.Infrastructure --startup-project .\src\Basket.Api --context ApplicationDbContext
```

✅ You can skip this step if you're just running the app — it will apply the latest migrations automatically.

## 📦 Running the Microservice

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) (for running Redis, MySQL, and RabbitMQ)
- [Visual Studio 2022+ / VS Code](https://code.visualstudio.com/)

### ▶️ Running via Docker Compose

```bash
docker-compose up --build

```

## 📈 Planned Improvements

- 🔐 **Authentication & Authorization**
  - Integrate JWT or API key-based security for gRPC endpoints to protect the service.
- 🔁 **Retry & Circuit Breaker Policies**
  - Implement Retry and Circuit Breaker policies with Polly
- 🗃️ **Unit & Integration Tests improvements**
  - Increase Unit Testing & Integration Testing Coverage
- 📤 **Outbox Pattern**
  - Implement the Outbox Pattern using EF Core and MassTransit to ensure reliable event publishing:
