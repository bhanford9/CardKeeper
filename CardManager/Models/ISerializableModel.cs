using CardManager.SerializationDtos;

namespace CardManager.Models;

public interface ISerializableModel<TDto> where TDto : IModelSerialization
{
    TDto ToDto();
}
