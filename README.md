# Jobsity - Chat Challenge

Start the Jobsity.API and Jobsity.Presentation.Web projects to have the chat 100% functional.

Change the connection string inside appsettings.Development.json in the Jobsity.API project
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=jobsity; Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```


Inside the src folder run the following command to run the migrations:
```
dotnet ef database update --context DataContext --startup-project Jobsity.API --project Jobsity.Infra.Data
```

## Technologies:

- ASP.NET 6.0
- ASP.NET WebApi Core with JWT Bearer Authentication
- ASP.NET Identity Core
- Entity Framework Core 6.0
- .NET Core Native DI
- AutoMapper
- Swagger UI with JWT support
- .NET DevPack
- .NET DevPack.Identity

## Architecture:

- Domain Driven Design
