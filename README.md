# Accounting API (Backend Service)

![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg) ![Status](https://img.shields.io/badge/status-Active-green.svg)

**Accounting API** is a scalable Pre-Accounting Backend service developed using modern software architecture principles (SOLID, Clean Architecture). This project provides RESTful endpoints capable of being consumed by any Frontend (React, Angular, Mobile) application.

##  Architecture & Technologies

The project follows the **Onion Architecture (N-Layer)** structure and consists of 4 main layers:

* **AccountingApp.Core:** Entities, DTOs, Interfaces. (Dependency-free central layer)
* **AccountingApp.Data:** EF Core 8, Repository Pattern, Unit of Work, SQL Server configurations.
* **AccountingApp.Services:** Business Logic, Validations (FluentValidation), Mapping (AutoMapper).
* **AccountingApp.API:** REST Controllers exposed to the client.

## Features

* **Entity Framework Core 8 Code-First** approach.
* **Repository & Unit of Work Design Pattern** implementation.
* **FluentValidation** for automatic model validation.
* **AutoMapper** for Entity-DTO mapping.
* **Global Exception Handling** (Centralized error management via Middleware).
* **Swagger/OpenAPI** documentation.
