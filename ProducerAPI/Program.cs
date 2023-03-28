using Wex.Libraries.Kafka.Configuration;
using Wex.Libraries.Kafka.DependencyInjection;
using Wex.Libraries.Kafka.Producer;
using ProducerAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var cts = new CancellationTokenSource();
var aivenEnv = builder.Environment.EnvironmentName;
// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafka(WexDivision.Health, typeof(Program).Assembly, aivenEnv == "Public"? "Kafka-Public" : "Kafka-Dev");
builder.Services.AddProducer<CDBCarrierNotifications>("health.cdb.carrier-notification.data", ".\\Schemas\\health_cdb_carrier_notification_data.avsc");

builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();
//Add environment variables
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else if (app.Environment.IsEnvironment("Public"))
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Services.GetService<IKafkaProducer<CDBCarrierNotifications>>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync(cts.Token);