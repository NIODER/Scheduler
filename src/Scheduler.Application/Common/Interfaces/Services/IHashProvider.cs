namespace Scheduler.Application.Common.Interfaces.Services;

public interface IHashProvider
{
    string GetHash(string data);
}