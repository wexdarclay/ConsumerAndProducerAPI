using Consumer.Models;
using System.Text.Json;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace Consumer.Handlers
{
	internal class MBEMessageHandler : IHandler<MBECarrierNotifications>
	{
		public Task HandleAsync(Message<MBECarrierNotifications> message, CancellationToken cancellationToken)
		{
			Console.Write("MBE Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");
			return Task.CompletedTask;
		}
	}
}
