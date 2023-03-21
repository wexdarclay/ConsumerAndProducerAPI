using LogConsumer.Models.LogCarrierMessages;
using System.Text.Json;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace LogConsumer.Handlers
{
	internal class LogMessageHandler : IHandler<LogCarrierNotifications>
	{
		public Task HandleAsync(Message<LogCarrierNotifications> message, CancellationToken cancellationToken)
		{
			Console.Write("Log Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");

			return Task.CompletedTask;
		}
	}
}
