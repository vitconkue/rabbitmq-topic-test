namespace Consumer;

public class StudentHandler: IRabbitmqMessageHandler<Student>
{
    public void Handle(Student receivedObject)
    {
        Console.WriteLine($"Handling student {receivedObject.Name}");
    }
}