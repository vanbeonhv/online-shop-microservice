namespace Contracts.Common.Interfaces;

public interface ISerializeService
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string str);
}