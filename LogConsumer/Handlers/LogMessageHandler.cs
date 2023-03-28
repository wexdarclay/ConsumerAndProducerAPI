using LogConsumer.Models.LogCarrierMessages;
using Newtonsoft.Json;
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
			//string json = JsonSerializer.Serialize(message.Value);
			var options = new JsonSerializerOptions
			{
					WriteIndented= true
			};
			
			string json = System.Text.Json.JsonSerializer.Serialize(message, options);

			Console.WriteLine(json);
			Console.WriteLine("------------------------------------------------");

			return Task.CompletedTask;
		}
	}
}
