
# 🛒 Modular E-Commerce Backend with Clean Architecture

This project is a robust, modular e-commerce backend that follows **Clean Architecture** and **Modular Monolith** principles. Built with **ASP.NET Core**, the system is designed for performance, scalability, and maintainability. It integrates with **PostgreSQL** as the database, and utilizes **Docker** for containerization, making deployment and scalability easier.

## 🧱 Tech Stack
- **ASP.NET Core** – For building the web API and application logic.
- **PostgreSQL** – For data storage, with partitioning by language code for translations.
- **Docker** – Containerized environments for easy deployment.
- **MassTransit** – For communication between modules, with future integration with RabbitMQ.
- **MediateR (CQRS)** – To separate commands and queries, enabling clear communication patterns.
- **Result Pattern** – To improve performance and avoid throwing exceptions unnecessarily by using a consistent result handling pattern.
- **Permission-based Authentication** – To manage user roles and access permissions securely.

## 📦 Core Features
1. **Modular Monolith Architecture**: 
   The system is structured as a modular monolith, meaning different modules (such as Users, Vendors, Products, etc.) are encapsulated and independent while still part of a single deployment unit.

2. **Localization and Translation Support**: 
   - **Product Translations**: Supports multilingual product descriptions through the `product_translation` table.
   - **Product Item Translations**: Similarly, product items can have translations in the `product_item_translation` table.
   - **Category Structure**: Categories are organized in a tree-like structure and support language-specific translations.
   - **Partitioning by Language**: Data for each language is partitioned, making it more efficient to query and handle translated data.

3. **Database Performance Optimization**: 
   - Uses **inverted indexes** that are generalized for partitioned language-specific tables, which optimizes search and retrieval operations for translated content.

4. **CQRS (Command Query Responsibility Segregation)**: 
   - Commands and queries are separated using **MediateR**, streamlining operations and making the codebase easier to maintain and scale.

5. **MassTransit** for Cross-Module Communication:
   - Modules (such as Users and Orders) communicate using **MassTransit**, which can be extended to work with **RabbitMQ** for event-driven messaging in the future.

6. **Permission-Based Authentication**: 
   - User authentication and authorization are managed with **permission-based** access control, ensuring that each user has appropriate access to resources.

## 📂 Folder Structure (Simplified)
```bash
src/
├── Api/
└── Modules/
    └── Users/...
    └── Notification/...
    └── Orders/
        ├── Application/
        │   └── UseCases/...
        ├── Domain/
        ├── Infrastructure/
        └── Presentation/
            └── Endpoints/
```

## 🧩 Design Principles
- **Clean Architecture**: Each feature is neatly separated into different layers (Application, Domain, Infrastructure, Presentation) to ensure maintainability and testability.
- **Modular Monolith**: Even though the system is a monolith, it’s structured in a modular way so that each module can be developed and tested independently.
- **Result Pattern**: Instead of throwing exceptions, the application uses a consistent result pattern to improve performance and handle errors gracefully.

## 🚀 Running Locally
To run the project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/your-org/ecommerce-backend.git
   cd ecommerce-backend
   ```

2. Run the project:
   ```bash
   dotnet run --project src/Modules.Orders.Presentation
   ```

3. Access the API:
   - Swagger UI: `https://localhost:5001/swagger/index.html` (if Swagger is configured)
   - Use **Postman** or **Insomnia** to test API endpoints.

## 📌 Future Enhancements
- **API Versioning**: To ensure backward compatibility.
- **Unit & Integration Tests**: To ensure the reliability of the system.
- **Caching**: Implement caching for read-heavy endpoints to improve performance.
- **Docker Support**: For easier and more portable deployments.

## 📄 License
MIT License. See LICENSE file.
