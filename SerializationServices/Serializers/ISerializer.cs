namespace SerializationServices.Serializers;

internal interface ISerializer<T>
{
    T Deserialize(string content);
    string Serialize(T data);
}