using Consumer.Handlers;
using Wex.Libraries.Kafka.Configuration;
using Wex.Libraries.Kafka.Consumer;
using Wex.Libraries.Kafka.DependencyInjection;
using Consumer.Models.CDBCarrierMessages;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
	e.Cancel = true;
	cts.Cancel();
};

var builder = Host.CreateDefaultBuilder()
   .ConfigureServices((hostContext, services) =>
   {
	   services
			.AddKafka(WexDivision.Health, typeof(Program).Assembly)
			.AddConsumer(builder =>
			{
				builder
				.AddTopic("processing.cdb.carrier-notifications", "Liveness")
				.AddSubject<CDBCarrierNotifications>("health.cdb.carrier-notification.data");
			})
			.AddTransient<IHandler<CDBCarrierNotifications>, CDBMessageHandler>();
   })
   .ConfigureAppConfiguration((hostingContext, config) =>
   {
	   config.AddUserSecrets<Program>();
   })
   .UseConsoleLifetime();

var host = builder.Build();
await host.RunAsync(cts.Token);