using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Consumer.Handlers;
using Wex.Libraries.Kafka.Configuration;
using Wex.Libraries.Kafka.Consumer;
using Wex.Libraries.Kafka.DependencyInjection;
using Consumer.Models.CDBCarrierMessages;
using Consumer.Models.MBECarrierNotificationLog;
using Consumer.Models.MBECarrierNotificationData;
using Consumer.Models.MBECarrierNofificationLogError;

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
			//ADDING THE CONSUMER FOR: HEALTH.CDB.CARRIER-NOTIFICATION.DATA 
			.AddConsumer(builder =>
			{
				builder
				.AddTopic("processing.cdb.carrier-notifications", "Liveness")
				.AddSubject<CDBCarrierNotifications>("health.cdb.carrier-notification.data");
			})
			.AddTransient<IHandler<CDBCarrierNotifications>, CDBMessageHandler>()
			//ADDING THE CONSUMER FOR: HEALTH.MBE.CARRIER - NOTIFICATION.LOG
			.AddConsumer(builder =>
			{
				builder
				.AddTopic("logging.mbe.carrier-notifications", "Liveness")
				.AddSubject<MBECarrierNotificationsData>("health.mbe.carrier-notifications.log");
			})
			.AddTransient<IHandler<MBECarrierNotificationsLog>, MBEMessageLogHandler>()
			//ADDING THE CONSUMER FOR: HEALTH.MBE.CARRIER - NOTIFICATION.LOG - ERROR
			.AddConsumer(builder =>
			{
				builder
				.AddTopic("logging.mbe.carrier-notifications", "Liveness")
				.AddSubject<MBECarrierNotificationsLogError>("health.mbe.carrier-notification.log-error");
			})
			.AddTransient<IHandler<MBECarrierNotificationsLogError>, MBEMessageLogErrorHandler>();
   })
   .ConfigureAppConfiguration((hostingContext, config) =>
   {
	   config.AddUserSecrets<Program>();
   })
   .UseConsoleLifetime();

var host = builder.Build();
await host.RunAsync(cts.Token);