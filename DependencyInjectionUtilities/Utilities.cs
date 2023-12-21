using Autofac;
using Autofac.Builder;

namespace DependencyInjectionUtilities;

public static class Utilities
{
    public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<TInterface, TClass>(
        this ContainerBuilder builder)
        where TInterface : notnull
        where TClass : TInterface
        => builder.RegisterType<TClass>().As<TInterface>();
}
