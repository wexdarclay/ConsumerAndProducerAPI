using Consumer.Models.MBECarrierNotificationLog;
using System.Text.Json;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace Consumer.Handlers
{
    internal class MBEMessageLogHandler : IHandler<MBECarrierNotificationsLog>
	{
		public Task HandleAsync(Message<MBECarrierNotificationsLog> message, CancellationToken cancellationToken)
		{
			Console.Write("Logging Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");
			return Task.CompletedTask;
		}
	}
}
