using Consumer.Models.CDBCarrierMessages;
using Newtonsoft.Json;
using System.Text.Json;
using System.Xml;
using Wex.Libraries.Kafka;
using Wex.Libraries.Kafka.Consumer;

namespace Consumer.Handlers
{
    internal class CDBMessageHandler : IHandler<CDBCarrierNotifications>
	{
		public Task HandleAsync(Message<CDBCarrierNotifications> message, CancellationToken cancellationToken)
		{
			Console.Write("CDB Message");
			//Console.WriteLine(JsonSerializer.Serialize(message.Value));
			string json = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented);
			Console.WriteLine(json);
			Console.WriteLine("------------------------------------------------");

			return Task.CompletedTask;
		}
	}
}
