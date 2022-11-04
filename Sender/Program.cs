
// See https://aka.ms/new-console-template for more information
using System;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sender;
using Sender.Utility;

Console.WriteLine("Hello, World!");


var connectionFactory = new ConnectionFactory();
var singletonConnection = connectionFactory.CreateConnection();

var publisher = new Publisher<Student>(singletonConnection);

var listSend = new List<Student>()
{
    new Student()
    {
        Name = "An1"
    },
    new Student()
    {
        Name = "An2"
    },
    new Student()
    {
        Name = "An3"
    },
    new Student()
    {
        Name = "An4"
    },
    new Student()
    {
        Name = "An5"
    },
    new Student()
    {
        Name = "An6"
    },
    new Student()
    {
        Name = "An7"
    },
};

foreach (var student in listSend)
{
    publisher.Publish("test01.test02", student);
}


Console.WriteLine("Sender done");