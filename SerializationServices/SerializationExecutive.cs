using SerializationServices.Serializers;

namespace SerializationServices;

public class SerializationExecutive : ISerializationExecutive
{
    public string JsonSerialize<T>(T data)
    {
        return new JsonSerializer<T>().Serialize(data);
    }

    public T JsonDeserialize<T>(string json)
    {
        return new JsonSerializer<T>().Deserialize(json);
    }

    public void JsonSerializeToFile<T>(T data, string filePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        File.WriteAllText(filePath, this.JsonSerialize(data));
    }

    public T JsonDeserializeFromFile<T>(string filePath)
    {
        return this.JsonDeserialize<T>(File.ReadAllText(filePath));
    }
}
