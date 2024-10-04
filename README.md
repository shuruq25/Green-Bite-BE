# CareQuest Backend Project

## Project Overview

**CareQuest** is a backend solution for an e-commerce platform specializing in health and wellness products, pharmacy items, and related services. Built using .NET 8, this project includes core functionalities like user authentication, product management, cart management, orders, payments, and more.



## Features
**User Managment**
  - Register new user
  - User authentication with JWT token
  - Role-based access control (Admin, Customer).
  
**Product Management**:
  - Create new product 
  - Delete product 
  - Update product
  - Search Products: Provides functionality to search for products based on various filters, such as name, category, and price range. This includes the ability to search products within a maximum and minimum price range.
  - Role-based access control (Admin, Customer)
  
**Order Management**:

**Cart Management**:
  - Add Item to Cart
  - Update Cart Item
  - Remove Cart
  - View Cart
  - Role-based access control (Admin, Customer)

## Technologies Used

- **.Net 8**: Web API Framework
- **Entity Framework Core**: ORM for database interactions
- **PostgreSQl**: Relational database for storing data
- **JWT**: For user authentication and authorization
- **AutoMapper**: For object mapping
- **Swagger**: API documentation

## Prerequisites

- .NET 8 SDK
- SQL Server
- VSCode 
- Postman or similar API testing tools

## Getting Started

### 1. Clone the repository:
```bash
git clone `https://github.com/shuruq25/sda-3-online-Backend_Teamwork.git` 


```

### 2. Setup database

- Make sure PostgreSQL Server is running
- Create `appsettings.json` file
- Update the connection string in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Local": "Server=localhost;Database=ECommerceDb;User Id=your_username;Password=your_password;"
  }
}
```

- Run migrations to create database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- Run the application

```bash
dotnet watch
```

The API will be available at:  'http://localhost:5125'

### Swagger

- Navigate to `http://localhost:5125/swagger/index.html` to explore the API endpoints.


## Project structure

```bash
|-- Controllers: #API controllers with request and response
|-- Database # DbContext and Database Configurations
|-- DTOs # Data Transfer Objects
|-- Entities # Database Entities (User, Product, Category, Order, Address , Cart , Cart Details , Coupon , Payment , Review)
|-- Middleware # Logging request, response and Error Handler
|-- Repositories # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Utils # Common logics
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
```

## API Endpoints

### User

- **POST** `/api/users/register` – Register a new user.
- **POST** `/api/users/login` – Login and get JWT token.
  
### Product 

- **POST** `/api/v1/product` - Creating a product only the admin.
- **GET** `/api/v1/product` - Viewing all products for both user and admin.
- **GET** `/api/v1/product/Id` - Viewing product to user/admin. 
- **GET** `/api/v1/product/search` - Search product based on the name. 
- **PUT** `/api/v1/product/adminId` - Updating product only the admin.
- **DELETE** `/api/vi/Product/adminId` - Deleting product only for admin.
  
### Cart

- **POST** `/api/v1/cart` - Creating a only one cart for admin and user.
- **GET** `/api/v1/cart/Id` -  retrieves the cart for a specific user and admin by their Id.
- **PUT** `/api/v1/cart/Id` - Updating cart item for both user and admin.
- **DELETE** `/api/vi/Product/Id` - Deleting cart item for both user and admin.
- 
## Deployment

The application is deployed and can be accessed at: [https://your-deploy-link.com](https://your-deploy-link.com)

## Team Members

- **Lead** : Shuruq Almuhalbidi (@shuruq25)
- Abdullah Alkhwahir()
- Hadeel Alghashmari ()
- Raghad Alharbi()
- Reema Algureshie()

## License

This project is licensed under the MIT License.