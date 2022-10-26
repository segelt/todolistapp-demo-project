# ToDo Task App

Simple To-do List backend application written using .NET 6. User may store tasks, modify tasks,
set a task as completed and get a collection of pending and overdue tasks. Pending tasks are tasks that are not past or do not
have a due date, overdue tasks are tasks that are past their due date.

### Endpoints
---

- /create-task  : Creates a task
- /update-task  : Updates a task, based on its ID.
- /pending-tasks : Retrieves a collection of pending tasks
- /overdue-tasks : Retrieves a collection of overdue tasks

This application is written as a backend application. Swagger UI is used for frontend.
Application supports Docker deployment, and there is a docker compose file at the root of the project that can be deployed immediately.

### Architecture
---

[Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) is implemented in this application:
TodoListApp: Web Project. It is the main entrypoint of the application, includes endpoints
TodoListApp.Application: Contains business logic of the application.
TodoListApp.Domain: Contains the domain of the project. Domain project has no dependencies to any other project.
TodoListApp.Infrastructure: Manages and implements the application's dependencies on external resources (database). Dependencies
are wired up on this project.

Database access is managed by the repository layer. Business logic (Services) uses the repository layer to work with database.
Web endpoints use service layer to perform their tasks.

### Tests
---

This application is designed in a way that all code can be tested. Dependencies to classes are passed, not generated. For instance,
service layer does not generate new instances of repository layer, instead repository instances are passed through IoC container. Hard
dependencies such as "DateTime.Now" method is removed, so that all logic can be unit tested using mocking.

Test project contains tests for PendingTasks, OverdueTasks. 

### Deployment
---
The application can be executed either by the docker compose file, or it can be built and compiled into an executable using
Visual Studio. This requires the .NET 6 runtime, which is supported by Visual Studio 2022.

- Docker deployment:

    cd to root of the project\
    docker compose up -d --build\
    Navigate to http://localhost:4500/swagger/index.html from any browser

    Docker compose maps the container port 80 to port 4500.

- Deployment using visual studio:

    The application can be built and executed using Visual Studio. There is a build configuration *ToDoListApp* that can be run. A PostgreSql instance needs to be running on the host machine.
    A valid connection string should be passed either by setting the "**Conn_Str**" environment variable, or by supplying the connection string on AppSettings file, by setting the **ConnectionStrings.TodoAppDb** key.
    The application can be reached from: http://localhost:5179/swagger/index.html

Database migrations are executed at the startup of the application.

### Future Works
---

- Using a better logging provider
- Exception handling middleware
- https support
