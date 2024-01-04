using Microsoft.Extensions.DependencyInjection;
using SerializationServices.Serializers;

namespace SerializationServices;

public static class Container
{
    public static IServiceCollection RegisterSerializationServices(this IServiceCollection builder)
        => builder
        .AddSingleton(typeof(IJsonSerializer<>), typeof(JsonSerializer<>))
        .AddSingleton<ISerializationExecutive, SerializationExecutive>();
}
