using RabbitMQ.Client;

namespace Sender;

public abstract class BasePublisher<TMessageType>
{
    protected readonly IModel Channel;
    private readonly IConnection _connection;


    public BasePublisher(IConnection connection)
    {
        _connection = connection;
        Channel = connection.CreateModel();
    }
    
    public abstract Task Publish(string topicString, TMessageType obj);
}