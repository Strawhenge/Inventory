using Autofac;
using FunctionalUtilities;
using Strawhenge.Common.Factories;

class AutofacAbstractFactory : IAbstractFactory
{
    readonly ILifetimeScope _scope;

    public AutofacAbstractFactory(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public T Create<T>() => _scope.Resolve<T>();

    public Maybe<T> TryCreate<T>() => _scope.TryResolve<T>(out var instance)
        ? Maybe.Some(instance)
        : Maybe.None<T>();
}