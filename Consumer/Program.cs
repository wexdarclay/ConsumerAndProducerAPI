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
var aivenEnv = "";

var builder = Host.CreateDefaultBuilder();
builder.ConfigureServices((hostContext, services) =>
   {
	   aivenEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
	   services.AddKafka(WexDivision.Health, typeof(Program).Assembly, aivenEnv == "Development"? "Kafka-Dev" : "Kafka-Public")
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