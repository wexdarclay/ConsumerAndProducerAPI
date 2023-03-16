using Microsoft.AspNetCore.Mvc;
using Wex.Libraries.Kafka;
using ProducerAPI.Models;
using Wex.Libraries.Kafka.Producer;
using Wex.Libraries.Kafka.DependencyInjection;
using Wex.Libraries.Kafka.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProducerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProducerController : ControllerBase
	{
		//SendingCDBCarrierNotification
		[HttpPost("/send-predefined-cdb-carrier-notification")]
		public async Task<Message<CDBCarrierNotifications>> PostPredefinedCDBCarrierNotifications()
		{
			Console.WriteLine("sending cdb predefined");

			var cts = new CancellationTokenSource();

			var hostbuilder = Host.CreateDefaultBuilder();
			hostbuilder.ConfigureServices((hostContext, services) =>
			{
				services.AddKafka(WexDivision.Health, typeof(Program).Assembly);
				services.AddProducer<CDBCarrierNotifications>("health.cdb.carrier-notification.data", ".\\Schemas\\health_cdb_carrier_notification_data.avsc");
			});
			//hostbuilder.ConfigureAppConfiguration((hostingContext, config) =>
			//{
			//	config.AddUserSecrets<Program>();
			//});
			hostbuilder.UseConsoleLifetime();

			var host = hostbuilder.Build();
			var _kafkaProducer = host.Services.GetRequiredService<IKafkaProducer<CDBCarrierNotifications>>();
			var domainEntity = new CDBCarrierNotifications();
			domainEntity.cdb_carrier_notificationrecords = new List<CDBCarrierNotificationRecord>();
			domainEntity.cdb_carrier_notificationrecords.Add(new CDBCarrierNotificationRecord()
			{
				carrier_record_id = Guid.NewGuid().ToString(),
				status = "test",
				client_name = "test",
				division_name = "test",
				member_id = "test",
				plan_name = "test",
				plan_id = "test",
				carrier_name = "test",
				carrier_notification_name_type = "test",
				carrier_notification_description = "test",
				member_name = "test",
				first_name = "test",
				middle_initial = "test",
				last_name = "test",
				member_ssn = "test",
				member_dob = "test",
				effective_date = "test",
				new_data_text = "test",
				individual_identifier = "test",
				dependent_id = "test",
				dependent_name = "test",
				dependent_first_name = "test",
				dependent_middle_initial = "test",
				dependent_last_name = "test",
				dependent_ssn = "test",
				dependent_dob = "test",
				relationship = "test",
				dependent_new_data_text = "test",
				entered_date = "test",
				processed_date = "test",
				generated_date_time = "test",
				enrollment_date = "test"
			});

			var kafka_message = new Message<CDBCarrierNotifications>(
				idempotencyKey: Guid.NewGuid().ToString(),
				domainEntity
				);

			try
			{
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", "1", kafka_message, cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};

			await host.StopAsync(cts.Token);
			return kafka_message;
		}

		//SendingCDBCarrierNotification
		[HttpPost("/send-cdb-carrier-notification")]
		public async Task PostCDBCarrierNotifications([FromBody] CDBCarrierNotificationRecord record)
		{
			Console.WriteLine("sending cdb");

			var cts = new CancellationTokenSource();

			var hostbuilder = Host.CreateDefaultBuilder();
			hostbuilder.ConfigureServices((hostContext, services) =>
			{
				services.AddKafka(WexDivision.Health, typeof(Program).Assembly);
				services.AddProducer<CDBCarrierNotifications>("health.cdb.carrier-notification.data", ".\\Schemas\\health_cdb_carrier_notification_data.avsc");
			});
			//hostbuilder.ConfigureAppConfiguration((hostingContext, config) =>
			//{
			//	config.AddUserSecrets<Program>();
			//});
			hostbuilder.UseConsoleLifetime();

			var host = hostbuilder.Build();
			var _kafkaProducer = host.Services.GetRequiredService<IKafkaProducer<CDBCarrierNotifications>>();
			var domainEntity = new CDBCarrierNotifications();
			domainEntity.cdb_carrier_notificationrecords = new List<CDBCarrierNotificationRecord>();
			domainEntity.cdb_carrier_notificationrecords.Add(new CDBCarrierNotificationRecord()
			{
				carrier_record_id = record.carrier_record_id,
				status = record.status,
				client_name = record.client_name,
				division_name = record.division_name,
				member_id = record.member_id,
				plan_name = record.plan_name,
				plan_id = record.plan_id,
				carrier_name = record.carrier_name,
				carrier_notification_name_type = record.carrier_notification_name_type,
				carrier_notification_description = record.carrier_notification_description,
				member_name = record.member_name,
				first_name = record.first_name,
				middle_initial = record.middle_initial,
				last_name = record.last_name,
				member_ssn = record.member_ssn,
				member_dob = record.member_dob,
				effective_date = record.effective_date,
				new_data_text = record.new_data_text,
				individual_identifier = record.individual_identifier,
				dependent_id = record.dependent_id,
				dependent_name = record.dependent_name,
				dependent_first_name = record.dependent_first_name,
				dependent_middle_initial = record.dependent_middle_initial,
				dependent_last_name = record.dependent_last_name,
				dependent_ssn = record.dependent_ssn,
				dependent_dob = record.dependent_dob,
				relationship = record.relationship,
				dependent_new_data_text = record.dependent_new_data_text,
				entered_date = record.enrollment_date,
				processed_date = record.processed_date,
				generated_date_time = record.generated_date_time,
				enrollment_date = record.enrollment_date
			});

			var kafka_message = new Message<CDBCarrierNotifications>(
				idempotencyKey: Guid.NewGuid().ToString(),
				domainEntity
				);

			try
			{
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", "1", kafka_message, cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};

			await host.StopAsync(cts.Token);

		}

		//SendingCDBThreePassTwoFail
		[HttpPost("/send-multiple-carrier-notification")]
		public async Task PostNotifications([FromBody] CDBCarrierNotifications records)
		{
			Console.WriteLine("sending multiple");

			var cts = new CancellationTokenSource();

			var hostbuilder = Host.CreateDefaultBuilder();
			hostbuilder.ConfigureServices((hostContext, services) =>
			{
				services.AddKafka(WexDivision.Health, typeof(Program).Assembly);
				services.AddProducer<CDBCarrierNotifications>("health.cdb.carrier-notification.data", ".\\Schemas\\health_cdb_carrier_notification_data.avsc");
			});
			//hostbuilder.ConfigureAppConfiguration((hostingContext, config) =>
			//{
			//	config.AddUserSecrets<Program>();
			//});
			hostbuilder.UseConsoleLifetime();

			var host = hostbuilder.Build();
			var _kafkaProducer = host.Services.GetRequiredService<IKafkaProducer<CDBCarrierNotifications>>();
			var domainEntity = new CDBCarrierNotifications();
			domainEntity = records;

			var kafka_message = new Message<CDBCarrierNotifications>(
				idempotencyKey: Guid.NewGuid().ToString(),
				domainEntity
				);

			try
			{
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", "1", kafka_message, cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};

			await host.StopAsync(cts.Token);
		}
	}
}