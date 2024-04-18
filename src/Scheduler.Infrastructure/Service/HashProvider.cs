using System.Security.Cryptography;
using System.Text;
using Scheduler.Application.Common.Interfaces.Services;

namespace Scheduler.Infrastructure.Service;

public class HashProvider : IHashProvider
{
    public string GetHash(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        byte[] hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
