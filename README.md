# 🛒 Modular E-Commerce Backend (Orders Module)

This project is a modular e-commerce backend built with ASP.NET Core, MediatR, and Minimal APIs. It focuses on clean architecture, modular separation, and scalability — specifically handling Vendors, Categories, Specifications, Brands, Products, and Banners in the Orders domain.

---

## 🧱 Tech Stack

- **ASP.NET Core 8+**
- **MediatR** – for in-process messaging
- **Minimal APIs** – for lightweight and fast endpoint mapping
- **Modular Monolith Architecture** – decoupled features within the same solution
- **Fluent Results** – for consistent success/failure handling
- **Swagger/OpenAPI** – API documentation (if enabled)

---

## 📦 Modules and Endpoints

### ✅ Vendors (`/api/vendors`)
- **POST /** – Create a vendor
- **GET /** – Paginate vendors (with optional name filter)
- **PUT /{id}** – Update a vendor

### 🧾 Specifications (`/api/specs`)
- **POST /{id}** – Add specification options to a category
- **GET /{id}** – Get spec options for a category
- **POST /** – Create a new spec
- **GET /** – Paginate specs with filtering

### 🛍️ Products (`/api/products`)
- **POST /** – Create a product
- **POST /{Id}/items** – Add product items (SKUs)
- **PUT /items/{Id}** – Update a product item
- **DELETE /items/{Id}** – Delete a product item

### 🗂️ Categories (`/api/categories`)
- **POST /** – Create category
- **GET /main** – Get main categories (localized)
- **GET /** – Paginate categories
- **GET /{name}** – Get category by name
- **PUT /{name}** – Update category
- **POST /{name}/specs/** – Assign specs to category

### 🏷️ Brands (`/api/brands`)
- **POST /** – Create brand
- **GET /** – Paginate brands
- **PUT /{name}** – Update brand

### 🎏 Banners (`/api/banners`)
- **POST /** – Create banner
- **GET /active** – Get active banners
- **GET /** – Paginate banners
- **DELETE /{id}** – Delete banner

---

## 🧪 Running Locally

1. **Clone the repo**

    ```bash
    git clone https://github.com/your-org/ecommerce-backend.git
    cd ecommerce-backend
    ```

2. **Run the project**

    ```bash
    dotnet run --project src/Modules.Orders.Presentation
    ```

3. **Access the API**

    - **Swagger UI**: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) (if Swagger is configured)
    - Use **Postman/Insomnia** for testing endpoints.

---

## 🧱 Folder Structure (Simplified)

```bash
Modules/
└── Orders/
    ├── Application/
    │   └── UseCases/...
    ├── Domain/
    ├── Infrastructure/
    └── Presentation/
        └── Endpoints/
```
