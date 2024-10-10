---

# 🚀 TaskTrackerAPI

TaskTrackerAPI is a powerful and user-friendly **RESTful API** designed for task management. Built with **ASP.NET Core**, it supports user registration and authentication via **JWT** and allows users to track their tasks linked to specific accounts.

## ✨ Key Features

- 📋 **Task Management**: Create, update, delete, and complete tasks.
- 🔑 **User Authentication and Registration**: Register new users, authenticate via JWT tokens.
- 🧑‍💼 **Task Assignment to Users**: Tasks are assigned to specific users, making tracking more convenient.
- 🔄 **Automatic Database Migrations**: On application startup, the API automatically applies any pending migrations to the database, ensuring it is always up-to-date.
- 🪶 **Logging:** Comprehensive logging capabilities using Serilog, enabling efficient monitoring and troubleshooting of application behavior.

## 🛠️ Tech Stack

- **ASP.NET Core 8.0** — API backbone.
- **Entity Framework Core** — ORM for database management.
- **Docker** — containerization for easy deployment.
- **JWT** — for authentication and authorization.
- **nUnit** — for project testing.

## 🚀 Getting Started

### 🔧 Local Setup

1. Clone the repository:

    ```bash
    git clone https://github.com/sollq/task-tracker-api.git
    cd TaskTrackerAPI
    ```

2. Restore dependencies and build the project:

    ```bash
    dotnet restore
    dotnet build
    ```

3. Run the project:

    ```bash
    dotnet run
    ```

### 🐳 Running with Docker

1. Build the Docker image:

    ```bash
    docker build -t tasktrackerapi .
    ```

2. Run the container:

    ```bash
    docker run -p 8080:8080 tasktrackerapi
    ```

### ✅ Testing

The project uses **nUnit** for testing. Run the tests with:

```bash
dotnet test
```

## 📚 API Endpoints

### Tasks

| Method  | Endpoint                        | Description                             |
|---------|----------------------------------|-----------------------------------------|
| `GET`   | `/api/tasks/taskFor/{username}`  | Get all tasks for a user                |
| `GET`   | `/api/tasks/taskFor/{id:int}`    | Get a task by its ID                    |
| `POST`  | `/api/tasks`                    | Create a new task                       |
| `PUT`   | `/api/tasks/{id:int}`            | Update a task by its ID                 |
| `PUT`   | `/api/tasks/complete/{id:int}`   | Complete a task by its ID               |
| `DELETE`| `/api/tasks/{id:int}`            | Delete a task by its ID                 |

### Users

| Method  | Endpoint                 | Description                             |
|---------|--------------------------|-----------------------------------------|
| `POST`  | `/api/user/register`      | Register a new user                     |
| `POST`  | `/api/user/login`         | User authentication                     |

## ⚙️ CI/CD with Docker

The project is automatically built and deployed via **GitHub Actions** upon pushing changes to the `master` branch. Here’s an example workflow:

```yaml
name: Docker Image CI

on:
  push:
    branches: [ "master" ]
  pull_request:
    branches: [ "master" ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    - name: Build the Docker image
      run: docker build . --file Dockerfile --tag tasktrackerapi:$(date +%s)
```

## 🛠️ Docker Image Builds

- Using Docker ensures easy deployment and maintenance of the project in containers.
- Containerize the app for any environment with minimal effort.

## 📞 Contact

If you have any questions or suggestions about the project, feel free to contact me via Telegram:  
[![Telegram](https://img.shields.io/badge/Telegram-Contact-blue)](https://t.me/xsisd)

---

Thanks for reading! I'm constantly working to improve this project and would love your feedback.

---
