# 📦 EventSystem - Backend

Event management system built with ASP.NET Core Web API.  
Provides a set of endpoints to manage events, users, attendance, and more.

---

## 🛠️ Requirements

- ✅ [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- ✅ SQL Server
- ✅ Visual Studio 2022 or Visual Studio Code

---

## 🚀 How to Run

```bash
git clone https://github.com/Mahmoud-Ahmed-23/EventSystem.git
cd EventSystem/BackEnd
dotnet restore
dotnet ef database update
dotnet run
```

> Make sure to configure your database connection in the `appsettings.json` file.

---

## ⚙️ Technologies Used

| Technology           | Usage                            |
|---------------------|---------------------------------|
| ASP.NET Core Web API | Creating the API endpoints       |
| Entity Framework Core| Database access                  |
| SQL Server          | Data storage                    |
| AutoMapper          | Mapping between entities and DTOs|
| Swagger             | API documentation and testing    |
| ASP.NET Identity    | User management and authorization|
| Hangfire (optional) | Background job scheduling        |

---

## 📁 Project Structure

```bash
BackEnd/
├── Controllers/
├── DTOs/
├── Entities/
├── Mappings/
├── Repositories/
├── Services/
├── Program.cs
├── appsettings.json
└── EventSystem.csproj
```

---

## 📌 API Features

- Event registration and management.
- Attendance tracking.
- User and role management.
- JWT Authentication support (if enabled).
- Full API documentation via Swagger.

---

## 📄 Documentation

Once the project is running, you can access Swagger UI at:

```
https://localhost:{PORT}/swagger
```

---

## 🙋‍♂️ About Me

Developed by [Mahmoud Ahmed](https://github.com/Mahmoud-Ahmed-23)  
📬 For feedback or contributions, feel free to open an Issue or Pull Request.

---

## ⭐ Contribute

- 🌟 Star the repository if you like it  
- 🍴 Fork it to make your own changes  
- 🛠️ Submit Pull Requests for any improvements  
