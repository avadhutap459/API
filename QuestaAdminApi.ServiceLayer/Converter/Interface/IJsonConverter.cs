

namespace QuestaAdminApi.ServiceLayer
{
    public interface IJsonConverter
    {
        string JsonSerializeObject<T>(T RequestObject);
        T DeserializeObject<T>(string JsonString);
    }
}
