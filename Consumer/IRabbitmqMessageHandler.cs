namespace Consumer;

public interface IRabbitmqMessageHandler<T>
{
    void Handle(T receivedObject);
}