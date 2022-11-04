using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer;

public class RabbitMqManager
{
    private readonly IModel _channel;
    public RabbitMqManager(IModel channel)
    {
        _channel = channel;
    }
    public void Register<T>(string exchange, string topic, string queueName, IRabbitmqMessageHandler<T> handler)
    {
            _channel.ExchangeDeclare(exchange: exchange, type: "topic");

            _channel.QueueDeclare(queueName, durable: true, exclusive: false);
            _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: topic);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (_, ea) =>
            {
                var (message, routingKey) = GetStringAndRoutingKey(ea);
                var receivedObj = JsonSerializer.Deserialize<T>(message);

                Console.WriteLine($"Routing key received: {routingKey}");
                handler.Handle(receivedObj);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
    
    private static (string, string) GetStringAndRoutingKey(BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        return (message, routingKey);
    }
}