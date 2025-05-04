# Orders Module API

This project is a modular ASP.NET Core Web API responsible for handling operations related to products, categories, brands, specifications, vendors, and banners. It uses MediatR for CQRS pattern implementation and clean separation between concerns.

## Table of Contents

* [Overview](#overview)
* [Tech Stack](#tech-stack)
* [API Endpoints](#api-endpoints)

  * [Vendors](#vendors)
  * [Specifications](#specifications)
  * [Products](#products)
  * [Categories](#categories)
  * [Brands](#brands)
  * [Banners](#banners)
* [Contributing](#contributing)
* [License](#license)

---

## Overview

This API is part of a larger e-commerce solution. It supports:

* Creating and updating vendors
* Managing specifications and their options
* Creating and managing products and their items
* Category and brand CRUD operations
* Managing promotional banners

The modular approach allows you to maintain and scale features independently.

## Tech Stack

* **Framework:** ASP.NET Core Web API
* **Pattern:** CQRS with MediatR
* **Validation & Error Handling:** Custom extension `ExceptionToResult`
* **Routing:** Minimal API with `MapGroup`

## API Endpoints

### Vendors

| Method | Route             | Description              |
| ------ | ----------------- | ------------------------ |
| POST   | /api/vendors      | Create a new vendor      |
| GET    | /api/vendors      | Paginate vendor list     |
| PUT    | /api/vendors/{id} | Update a specific vendor |

### Specifications

| Method | Route           | Description                   |
| ------ | --------------- | ----------------------------- |
| POST   | /api/specs/{id} | Add spec options to category  |
| GET    | /api/specs/{id} | Get spec options for category |
| POST   | /api/specs      | Create new specification      |
| GET    | /api/specs      | Paginate specifications       |

### Products

| Method | Route                    | Description           |
| ------ | ------------------------ | --------------------- |
| POST   | /api/products            | Create new product    |
| POST   | /api/products/{id}/items | Add product items     |
| PUT    | /api/products/items/{id} | Update a product item |
| DELETE | /api/products/items/{id} | Delete a product item |

### Categories

| Method | Route                        | Description                |
| ------ | ---------------------------- | -------------------------- |
| POST   | /api/categories              | Create new category        |
| GET    | /api/categories/main         | Get main categories        |
| GET    | /api/categories              | Paginate categories        |
| GET    | /api/categories/{name}       | Get category by name       |
| PUT    | /api/categories/{name}       | Update category by name    |
| POST   | /api/categories/{name}/specs | Assign specs to a category |

### Brands

| Method | Route              | Description          |
| ------ | ------------------ | -------------------- |
| POST   | /api/brands        | Create new brand     |
| GET    | /api/brands        | Paginate brands      |
| PUT    | /api/brands/{name} | Update brand details |

### Banners

| Method | Route               | Description        |
| ------ | ------------------- | ------------------ |
| POST   | /api/banners        | Create banner      |
| GET    | /api/banners/active | Get active banners |
| GET    | /api/banners        | Paginate banners   |
| DELETE | /api/banners/{id}   | Delete a banner    |

