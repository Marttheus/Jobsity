# Jobsity - Chat Challenge

## Start the Jobsity.Presentation.Web, Jobsity.API and Jobsity.Worker projects to have the chat 100% functional.

### - TODO:
- [x] Allow registered users to log in and talk with other users in a chatroom.
- [x] Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code.
- [x] Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code).
- [x] The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.
- [x] Have the chat messages ordered by their timestamps and show only the last 50
messages.
- [ ] Unit test the functionality you prefer.
- [x] Have more than one chatroom.
- [x] Use .NET identity for users authentication.
- [x] Handle messages that are not understood or any exceptions raised within the bot.
- [ ] Build an installer.

### Change the connection strings for SQLServer and RabbitMQ inside appsettings.Development.json in the Jobsity.API project and inside appsettings.json in the Jobsity.Worker project
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=jobsity; Trusted_Connection=True;",
    "RabbitMq": "amqp://guest:guest@127.0.0.1:5672"
  }
}
```
```
{
  "ConnectionStrings": {
    "RabbitMq": "amqp://guest:guest@127.0.0.1:5672"
  }
}
```


### Inside the src folder run the following command to run the migrations:
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
- SignalR
- RabbitMQ (Message Broker)

## Architecture:

- Domain Driven Design
