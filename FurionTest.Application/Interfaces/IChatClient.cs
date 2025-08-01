namespace FurionTest.Application.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
}