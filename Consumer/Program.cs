using RabbitMQ.Client;
using Consumer;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory();

using var connection = factory.CreateConnection();
using (var channel = connection.CreateModel())
{
    var rabbitMqManager = new RabbitMqManager(channel);
    rabbitMqManager.Register("default", "test01.*", "booking", new StudentHandler());
    Console.ReadLine();
    channel.Close();
}

connection.Close();

