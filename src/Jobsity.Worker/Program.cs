using Jobsity.Worker.Workers;
using MassTransit;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices((context, collection) =>
    {
        collection.AddMassTransit(x =>
        {
            x.AddConsumer<QueueStockQuoteConsumer>();

            x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
            {
                c.Host(configuration.GetConnectionString("RabbitMq"));
                c.ReceiveEndpoint("stock-queue", e =>
                {
                    e.Consumer<QueueStockQuoteConsumer>(context);
                });
            }));
        });

        collection.AddMassTransitHostedService();
    })
    .Build();

var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
await host.StartAsync(source.Token);

Console.WriteLine("Waiting for new requests.");

while (true);