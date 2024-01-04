namespace SerializationServices.Serializers;

internal abstract class Serializer<T> : ISerializer<T>
{
    public abstract T Deserialize(string content);

    public abstract string Serialize(T data);
}
