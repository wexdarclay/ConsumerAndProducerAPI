using Consumer.Models;
using System.Text.Json;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace Consumer.Handlers
{
	internal class LoggingMessageHandler : IHandler<LoggingCarrierNotifications>
	{
		public Task HandleAsync(Message<LoggingCarrierNotifications> message, CancellationToken cancellationToken)
		{
			Console.Write("Logging Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");
			return Task.CompletedTask;
		}
	}
}
