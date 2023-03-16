using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Consumer.Models;
using Consumer.Handlers;
using Wex.Libraries.Kafka.Configuration;
using Wex.Libraries.Kafka.Consumer;
using Wex.Libraries.Kafka.DependencyInjection;

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
		.AddTransient<IHandler<CDBCarrierNotifications>, CDBMessageHandler>()
	   .AddConsumer(builder =>
	   {
		   builder
		   .AddTopic("processing.mbe.carrier-notifications", "Liveness")
		   .AddSubject<MBECarrierNotifications>("health.mbe.carrier-notifications.data");
	   })
		.AddTransient<IHandler<MBECarrierNotifications>, MBEMessageHandler>()
	   .AddConsumer(builder =>
	   {
		   builder
		   .AddTopic("logging.mbe.carrier-notifications", "Liveness")
		   .AddSubject<CDBCarrierNotifications>("logging.mbe.carrier-notifications");
	   })
		.AddTransient<IHandler<LoggingCarrierNotifications>, LoggingMessageHandler>();

   })
   .ConfigureAppConfiguration((hostingContext, config) =>
   {
	   config.AddUserSecrets<Program>();
   })
   .UseConsoleLifetime();

var host = builder.Build();
await host.RunAsync(cts.Token);