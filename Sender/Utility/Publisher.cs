using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Sender.Utility;

public class Publisher<TMessageType>: BasePublisher<TMessageType>, IDisposable
{
    public Publisher(IConnection connection) : base(connection)
    {
    }
    public override async Task Publish(string topicString, TMessageType obj)
    {
            Channel.ExchangeDeclare(exchange: "default", 
                type: "topic");

            var message = JsonSerializer.Serialize(obj);
            
            var body = Encoding.UTF8.GetBytes(message); 
            Channel.BasicPublish(exchange: "default", routingKey: topicString, basicProperties: null, body: body);
    }

    private void ReleaseUnmanagedResources()
    {
        this.Channel.Close();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Publisher()
    {
        ReleaseUnmanagedResources();
    }
}