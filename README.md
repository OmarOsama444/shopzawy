# 🛒 ShopZawy - Modern E-Commerce Platform

This project is a robust, modular e-commerce platform that follows **Clean Architecture** and **Modular Monolith** principles. Built with **ASP.NET Core**, the system is designed for performance, scalability, and maintainability. It integrates with **PostgreSQL** as the database, and utilizes **Docker** for containerization, making deployment and scalability easier.

## 🧱 Tech Stack

-   **ASP.NET Core** – For building the web API and application logic
-   **PostgreSQL** – For data storage, with partitioning by language code for translations
-   **Docker** – Containerized environments for easy deployment
-   **MassTransit** – For communication between modules, with future integration with RabbitMQ
-   **MediateR (CQRS)** – To separate commands and queries, enabling clear communication patterns
-   **Result Pattern** – To improve performance and avoid throwing exceptions unnecessarily
-   **Permission-based Authentication** – To manage user roles and access permissions securely

## 📦 Core Features

1. **Modular Monolith Architecture**:
   The system is structured as a modular monolith, meaning different modules (such as Users, Vendors, Products, etc.) are encapsulated and independent while still part of a single deployment unit.

2. **Localization and Translation Support**:

    - **Product Translations**: Supports multilingual product descriptions
    - **Product Item Translations**: Multilingual support for product items
    - **Category Structure**: Hierarchical categories with language-specific translations
    - **Partitioning by Language**: Optimized data storage for multilingual content

3. **Database Performance Optimization**:

    - Inverted indexes for partitioned language-specific tables
    - Optimized search and retrieval operations for translated content

4. **CQRS (Command Query Responsibility Segregation)**:

    - Clean separation of commands and queries using MediateR
    - Improved maintainability and scalability

5. **MassTransit Integration**:

    - Inter-module communication
    - Event-driven architecture support
    - Future RabbitMQ integration ready

6. **Permission-Based Authentication**:
    - Granular access control
    - Role-based permissions
    - Secure user management

## 📂 Project Structure

```
src/
├── Api/                    # Main API project
└── Modules/               # Feature modules
    ├── Users/            # User management module
    ├── Notification/     # Notification system
    └── Orders/          # Order processing module
        ├── Application/  # Business logic
        ├── Domain/      # Domain models
        ├── Infrastructure/ # Data access & external services
        └── Presentation/  # API endpoints
```

## 🚀 Getting Started

### Prerequisites

-   .NET 8.0 SDK
-   Docker and Docker Compose
-   PostgreSQL (if running locally)

### Running with Docker

1. Clone the repository:

    ```bash
    git clone https://github.com/your-org/shopzawy.git
    cd shopzawy
    ```

2. Start the containers:

    ```bash
    docker-compose up -d
    ```

3. Access the API:
    - Swagger UI: `https://localhost:5001/swagger`
    - API Base URL: `https://localhost:5001/api`

### Running Locally

1. Navigate to the project directory:

    ```bash
    cd src/Api
    ```

2. Run the application:
    ```bash
    dotnet run
    ```

## 🧪 Testing

-   Unit tests are located in the `Tests` directory
-   Integration tests are available for API endpoints
-   Run tests using:
    ```bash
    dotnet test
    ```

## 📝 API Documentation

-   Swagger UI is available at `/swagger` when running the application
-   API endpoints are documented with XML comments
-   Postman collection available in the `docs` folder

## 🔧 Configuration

-   Environment variables are managed through `appsettings.json`
-   Docker-specific settings in `docker-compose.yml`
-   Database connection strings in `appsettings.Development.json`

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

-   Your Name - Initial work

## 🙏 Acknowledgments

-   Thanks to all contributors
-   Inspired by clean architecture principles
-   Built with modern .NET technologies
