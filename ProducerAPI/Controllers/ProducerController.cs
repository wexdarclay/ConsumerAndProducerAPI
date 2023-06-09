﻿using Microsoft.AspNetCore.Mvc;
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
		IKafkaProducer<CDBCarrierNotifications> _kafkaProducer;
		CancellationTokenSource _cts;
		public ProducerController(IKafkaProducer<CDBCarrierNotifications> producer)
		{
			_kafkaProducer = producer;
			_cts = new CancellationTokenSource();
		}

		//SendingCDBCarrierNotification
		[HttpPost("/send-predefined-cdb-carrier-notification")]
		public async Task<Message<CDBCarrierNotifications>> PostPredefinedCDBCarrierNotifications()
		{
			Console.WriteLine("sending cdb predefined");

			var domainEntity = new CDBCarrierNotifications();
			domainEntity.cdb_carrier_notificationrecords = new List<CDBCarrierNotificationRecord>();
			domainEntity.cdb_carrier_notificationrecords.Add(new CDBCarrierNotificationRecord()
			{
				carrier_record_id = Guid.NewGuid().ToString(),
				status = "test",
				client_id = "1",
				client_name = "test",
				division_name = "test",
				member_id = "12",
				plan_name = "test",
				plan_id = "123",
				carrier_name = "test",
				carrier_notification_name_type = "test",
				carrier_notification_description = "test",
				member_name = "test",
				first_name = "test",
				middle_initial = "test",
				last_name = "test",
				member_ssn = "123-45-6789",
				member_dob = "01/01/1990",
				effective_date = "01/01/1990",
				new_data_text = "test",
				individual_identifier = "test",
				dependent_id = "1234",
				dependent_name = "test",
				dependent_first_name = "test",
				dependent_middle_initial = "test",
				dependent_last_name = "test",
				dependent_ssn = "987-65-4321",
				dependent_dob = "01/01/1990",
				relationship = "test",
				dependent_new_data_text = "test",
				entered_date = "01/01/2023",
				processed_date = "01/01/2023",
				generated_date_time = "01/01/2023",
				enrollment_date = "01/01/2023"
			});

			var kafka_message = new Message<CDBCarrierNotifications>(
				idempotencyKey: Guid.NewGuid().ToString(),
				domainEntity
				);

			try
			{
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", "test-key", kafka_message, _cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};

			return kafka_message;
		}

		//SendingCDBCarrierNotification
		[HttpPost("/send-cdb-carrier-notification")]
		public async Task PostCDBCarrierNotifications([FromBody] CDBCarrierNotificationRecord record)
		{
			Console.WriteLine("sending cdb");

			var domainEntity = new CDBCarrierNotifications();
			domainEntity.cdb_carrier_notificationrecords = new List<CDBCarrierNotificationRecord>();
			domainEntity.cdb_carrier_notificationrecords.Add(new CDBCarrierNotificationRecord()
			{
				carrier_record_id = record.carrier_record_id,
				status = record.status,
				client_id = record.client_id,
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
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", DateTime.Now.ToString(), kafka_message, _cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};
		}

		//SendingCDBThreePassTwoFail
		[HttpPost("/send-multiple-carrier-notification")]
		public async Task PostNotifications([FromBody] CDBCarrierNotifications records)
		{
			Console.WriteLine("sending multiple");

			var domainEntity = new CDBCarrierNotifications();
			domainEntity = records;

			var kafka_message = new Message<CDBCarrierNotifications>(
				idempotencyKey: Guid.NewGuid().ToString(),
				domainEntity
				);

			try
			{
				await _kafkaProducer.ProduceAsync("processing.cdb.carrier-notifications", "test-key", kafka_message, _cts.Token);
				Console.WriteLine("Completed Post");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			};
		}
	}
}