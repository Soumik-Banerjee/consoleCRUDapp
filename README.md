# ConsoleAppCRUD - A .NET Core Console Application for CRUD Operations with MSSQL

## Table of Contents
1. [Introduction](#introduction)
2. [Features](#features)
3. [Requirements](#requirements)
4. [Setup](#setup)
5. [Usage](#usage)
6. [CRUD Operations](#crud-operations)
7. [NuGet Packages](#nuget-packages)
8. [Project Structure](#project-structure)
9. [How it Works](#how-it-works)
10. [License](#license)

---

## Introduction

ConsoleAppCRUD is a .NET Core console application that demonstrates how to connect to a SQL Server database and perform basic CRUD (Create, Read, Update, Delete) operations on an `Employees` table. The application prompts the user to select a CRUD operation, input necessary details, and execute database commands.

This is a learning project to help developers understand how to interact with a database in a console environment using ADO.NET.

---

## Features

- **Create**: Insert new employee records.
- **Read**: Fetch and display all employees from the database.
- **Update**: Modify existing employee records.
- **Delete**: Remove employee records from the database.
- Uses **Windows Authentication** or **SQL Server Authentication** to connect to SQL Server.
- Configuration can be stored in an `appsettings.json` file for flexibility.

---

## Requirements

- **.NET Core SDK 8.0** or higher
- **SQL Server** (MSSQL)
- Windows OS (if using **Trusted Connection** for Windows Authentication)

---

## Setup

### Step 1: Clone the Repository

```bash
git clone https://github.com/your-repo/ConsoleAppCRUD.git
cd ConsoleAppCRUD
```

### Step 2: Install .NET Core SDK

Make sure that the .NET Core SDK 8.0 (or later) is installed on your system. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/8.0).

### Step 3: Create a Database and Table in SQL Server

Before running the application, make sure you have a SQL Server database with an `Employees` table. Use the following SQL script to create the table:

```sql
CREATE DATABASE SAMPLE_DB;
GO

USE SAMPLE_DB;
GO

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Age INT,
    Position NVARCHAR(100)
);
```

### Step 4: Modify `appsettings.json`

You can store the connection string in an `appsettings.json` file, or you can hardcode it. To use `appsettings.json`, create a file called `appsettings.json` in the root of your project with the following content:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=SAMPLE_DB;Trusted_Connection=True;"
  }
}
```

Replace `your_server_name` with your actual SQL Server name. If you are using SQL Server Authentication, your connection string should include `User Id` and `Password`.

For example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=SAMPLE_DB;User Id=myUsername;Password=myPassword;"
  }
}
```

### Step 5: Install NuGet Packages

Run the following command to install necessary NuGet packages for configuration handling:

```bash
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```

These packages are necessary for reading configuration from `appsettings.json`.

### Step 6: Build and Run the Application

To build and run the application, use the following commands:

```bash
dotnet build
dotnet run
```

---

## Usage

Once you run the application, you will be prompted to choose a CRUD operation:

```bash
Choose CRUD Operation:
1: Create
2: Read
3: Update
4: Delete
```

### Choose the option by typing the corresponding number (e.g., `1` for Create). Depending on the option, you will be prompted to enter employee details or the employee ID for update or delete operations.

---

## CRUD Operations

1. **Create**: Insert a new employee.
    - You will be prompted to enter the **Name**, **Age**, and **Position** of the employee.
  
2. **Read**: Fetch and display all employees.
    - The app will retrieve and print all employee records from the `Employees` table.
  
3. **Update**: Modify an existing employee's details.
    - You will be asked for the **Employee ID** and then the new **Name**, **Age**, and **Position**.

4. **Delete**: Remove an employee from the database.
    - You will be asked for the **Employee ID** of the employee you wish to delete.

---

## NuGet Packages

The following NuGet packages are used in this application:

1. **Microsoft.Extensions.Configuration**:
   - Provides APIs for reading configuration settings.
   - [NuGet Link](https://www.nuget.org/packages/Microsoft.Extensions.Configuration)

2. **Microsoft.Extensions.Configuration.Json**:
   - Adds support for reading JSON configuration files (`appsettings.json`).
   - [NuGet Link](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json)

---

## Project Structure

```
ConsoleAppCRUD/
│
├── appsettings.json (optional, if you decide to use it)
│
├── Program.cs        // Main application logic, contains CRUD operations
│
├── ConsoleAppCRUD.csproj   // Project configuration
│
└── README.md        // This documentation
```

### `Program.cs`
- This file contains the core application logic for handling user inputs, connecting to the database, and performing CRUD operations.

---

## How it Works

### 1. **Connection to Database**

The application uses ADO.NET's `SqlConnection` to connect to a SQL Server database. You can either hardcode the connection string or retrieve it from an `appsettings.json` file.

### 2. **CRUD Operations**

The user interacts with the application via a text-based menu system, selecting which operation to perform. Based on the choice, the application performs one of the following:

- **Create**: Inserts a new employee into the database.
- **Read**: Queries and displays all employee records.
- **Update**: Prompts for the employee's ID and updates their details in the database.
- **Delete**: Prompts for the employee's ID and deletes their record from the database.

### 3. **SQL Queries**

The application uses parameterized SQL queries to interact with the database. This ensures that user input is handled safely, avoiding SQL injection vulnerabilities.

### Example of a Create Query:

```csharp
string query = "INSERT INTO Employees (Name, Age, Position) VALUES (@name, @age, @position)";
using (SqlCommand cmd = new SqlCommand(query, conn))
{
    cmd.Parameters.AddWithValue("@name", name);
    cmd.Parameters.AddWithValue("@age", age);
    cmd.Parameters.AddWithValue("@position", position);
    cmd.ExecuteNonQuery();
}
```

---

## License

This project is open-source and available under the MIT License.    