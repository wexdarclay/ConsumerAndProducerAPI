using Consumer.Models.MBECarrierNotificationLog;
using Wex.Libraries.Kafka.Consumer;
using Wex.Libraries.Kafka;
using Consumer.Models.MBECarrierNofificationLogError;
using System.Text.Json;

namespace Consumer.Handlers
{
	internal class MBEMessageLogErrorHandler : IHandler<MBECarrierNotificationsLogError>
	{
		public Task HandleAsync(Message<MBECarrierNotificationsLogError> message, CancellationToken cancellationToken)
		{
			Console.Write("Logging Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");
			return Task.CompletedTask;
		}
	}
}
