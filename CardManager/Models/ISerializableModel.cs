using CardManager.SerializationDtos;

namespace CardManager.Models;

public interface ISerializableModel<T> where T : IModelSerialization
{
    T ToDto();
}
