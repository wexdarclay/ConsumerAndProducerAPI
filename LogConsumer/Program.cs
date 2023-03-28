using Wex.Libraries.Kafka.Configuration;
using Wex.Libraries.Kafka.Consumer;
using Wex.Libraries.Kafka.DependencyInjection;
using LogConsumer.Models.LogCarrierMessages;
using LogConsumer.Handlers;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
	e.Cancel = true;
	cts.Cancel();
};

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices((hostContext, services) =>
   {
	   services
			.AddKafka(WexDivision.Health, typeof(Program).Assembly,"Kafka-Public")
			.AddConsumer(builder =>
			{
				builder
				.AddTopic("logging.mbe.carrier-notifications", "Liveness")
				.AddSubject<LogCarrierNotifications>("health.mbe.carrier-notification.log");
			})
			.AddTransient<IHandler<LogCarrierNotifications>, LogMessageHandler>();
   })
   .ConfigureAppConfiguration((hostingContext, config) =>
   {
	   config.AddUserSecrets<Program>();
   })
   .UseConsoleLifetime();

var host = builder.Build();
await host.RunAsync(cts.Token);