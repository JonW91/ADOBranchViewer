namespace Server.Contracts;

public interface IPersistApiData
{
    Task<bool> PushApiDataToDatabase();
}