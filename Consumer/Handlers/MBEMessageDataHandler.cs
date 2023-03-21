//using Consumer.Models.MBECarrierNotificationData;
//using System.Text.Json;
//using Wex.Libraries.Kafka;
//using Wex.Libraries.Kafka.Consumer;

//namespace Consumer.Handlers
//{
//	internal class MBEMessageDataHandler : IHandler<MBECarrierNotificationsData>
//	{
//		public Task HandleAsync(Message<MBECarrierNotificationsData> message, CancellationToken cancellationToken)
//		{
//			Console.Write("MBE Message");
//			Console.WriteLine(JsonSerializer.Serialize(message.Value));
//			Console.WriteLine("------------------------------------------------");
//			return Task.CompletedTask;
//		}
//	}
//}
