
# User Profile and Post Management API

## Overview

This project is a **RESTful API** developed using **ASP.NET Core** to manage **User Profiles** and their associated **Posts**. It allows users to create, read, update, and delete user profiles and their posts. The system follows best practices for security, validation, and API usage. The frontend UI is built with **HTML**, **CSS**, and **JavaScript**, which interacts with the API for CRUD operations.

## Features

- Manage user profiles with attributes such as FirstName, LastName, Email, and DateOfBirth.
- Manage posts with attributes such as Title, Content, and DatePosted, associated with a specific user.
- Perform CRUD operations on both user profiles and posts.
- Basic user interface for managing profiles and posts.

## Technology Stack

- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server
- **Frontend**: HTML, CSS, JavaScript
- **ORM**: Entity Framework Core
- **API Documentation**: Postman (example requests and responses)

---

## Environment Setup

### Prerequisites

Before you begin, ensure that you have the following tools installed:

- **.NET SDK** (version 6.0 or higher) - [Install .NET SDK](https://dotnet.microsoft.com/download/dotnet)
- **SQL Server** or **SQL Server Express** - [Download SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Visual Studio** or **VS Code** (with C# extension) - [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)
- **Node.js** (for frontend) - [Install Node.js](https://nodejs.org/)

### Clone the Repository

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/your-repo-name
   ```

2. Navigate into the project directory:

   ```bash
   cd YourProjectDirectory
   ```

---

## Database Setup

### 1. Create the Database

You need to create a SQL Server database to store the user profiles and posts.

1. Open **SQL Server Management Studio** (SSMS).
2. Connect to your SQL Server instance.
3. Run the following SQL commands to create the database:

   ```sql
   CREATE DATABASE UserProfileDb;
   GO
   ```

4. After the database is created, update the `appsettings.json` file in the project to point to your local database. The connection string should look like this:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=UserProfileDb;Integrated Security=True;"
   }
   ```

### 2. Apply Migrations and Seed Data

1. Open the **Package Manager Console** in Visual Studio or run the following commands in the terminal (if using VS Code):

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. This will create the tables `UserProfiles` and `Posts` in the database.

3. If needed, seed initial data by creating a method in the `ApplicationDbContext` class or directly inserting data into the database.

---

## Running the Application

### 1. Backend (API)

To run the backend API:

1. Open the project in **Visual Studio** or **VS Code**.
2. Ensure that the correct target framework (.NET 6.0 or higher) is selected.
3. Press `F5` in Visual Studio or run the following command in the terminal:

   ```bash
   dotnet run
   ```

   This will start the API on `http://localhost:5000` by default.

### 2. Frontend (UI)

1. The frontend is a simple HTML, CSS, and JavaScript interface located in the `wwwroot` folder or separate directory (based on your project structure).
2. Open the `index.html` file in your browser.
3. The frontend will interact with the API to perform CRUD operations for user profiles and posts.

---

## API Endpoints

### User Profile Endpoints

#### GET: /api/UserProfile

Retrieve all user profiles.

**Response Example:**

```json
[
    {
        "UserID": 1,
        "FirstName": "John",
        "LastName": "Doe",
        "Email": "john.doe@example.com",
        "DateOfBirth": "1990-01-01T00:00:00"
    },
    ...
]
```

#### GET: /api/UserProfile/{id}

Retrieve a single user profile by ID.

**Response Example:**

```json
{
    "UserID": 1,
    "FirstName": "John",
    "LastName": "Doe",
    "Email": "john.doe@example.com",
    "DateOfBirth": "1990-01-01T00:00:00"
}
```

#### POST: /api/UserProfile

Create a new user profile.

**Request Body:**

```json
{
    "FirstName": "Jane",
    "LastName": "Doe",
    "Email": "jane.doe@example.com",
    "DateOfBirth": "1992-03-14T00:00:00"
}
```

**Response:**

Returns the newly created user profile.

#### PUT: /api/UserProfile/{id}

Update an existing user profile.

**Request Body:**

```json
{
    "UserID": 1,
    "FirstName": "John",
    "LastName": "Smith",
    "Email": "john.smith@example.com",
    "DateOfBirth": "1990-01-01T00:00:00"
}
```

**Response:**

Returns the updated user profile.

#### DELETE: /api/UserProfile/{id}

Delete a user profile.

---

### Post Endpoints

#### GET: /api/Post

Retrieve all posts.

#### GET: /api/Post/{id}

Retrieve a single post by ID.

#### POST: /api/Post

Create a new post for a user.

**Request Body:**

```json
{
    "UserID": 1,
    "Title": "My First Post",
    "Content": "This is the content of my first post.",
    "DatePosted": "2024-11-13T00:00:00"
}
```

#### PUT: /api/Post/{id}

Update an existing post.

**Request Body:**

```json
{
    "Title": "Updated Title",
    "Content": "Updated content of the post."
}
```

#### DELETE: /api/Post/{id}

Delete a post by ID.

---

## Security Best Practices

### 1. **SQL Injection Prevention**

- All database queries are executed using **Entity Framework Core**, which uses parameterized queries to prevent SQL injection attacks.
- Input data is validated and sanitized before being processed.

### 2. **Cross-Site Scripting (XSS) Protection**

- Input data such as user names, email, and post content is sanitized using **`WebUtility.HtmlEncode`** to prevent **XSS** attacks.

### 3. **Model Validation**

- Incoming data is validated using the built-in model validation (`ModelState.IsValid`), ensuring that only valid data is accepted and processed.
  
### 4. **Authentication and Authorization**

- For production environments, you may want to implement authentication and authorization using **JWT (JSON Web Tokens)** or **OAuth** for securing API endpoints.

### 5. **Error Handling**

- Custom error messages are returned in case of invalid inputs or database issues, preventing exposure of sensitive information.

### 6. **Rate Limiting**

- Consider implementing rate limiting or throttling to prevent abuse and protect the API from excessive traffic or DoS (Denial of Service) attacks.

---

## Usage Examples

- **Postman Collection**: A Postman collection is included for quick testing of all API endpoints, with example requests and responses.
- **Frontend UI**: The frontend allows users to interact with the API, perform CRUD operations on user profiles and posts, and view results in a simple interface.

---

## Conclusion

This API provides a basic, secure solution for managing user profiles and posts. By following best practices such as model validation, SQL injection prevention, and input sanitization, we ensure that the system is secure and reliable. The frontend interface offers an easy-to-use way to interact with the API and perform CRUD operations on both users and posts.

