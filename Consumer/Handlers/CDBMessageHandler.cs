using Consumer.Models;
using System.Text.Json;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace Consumer.Handlers
{
	internal class CDBMessageHandler : IHandler<CDBCarrierNotifications>
	{
		public Task HandleAsync(Message<CDBCarrierNotifications> message, CancellationToken cancellationToken)
		{
			Console.Write("CDB Message");
			Console.WriteLine(JsonSerializer.Serialize(message.Value));
			Console.WriteLine("------------------------------------------------");

			return Task.CompletedTask;
		}
	}
}
