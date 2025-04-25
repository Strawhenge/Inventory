using Autofac;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

class AutofacEffectFactoryLocator : IEffectFactoryLocator
{
    readonly ILifetimeScope _scope;

    public AutofacEffectFactoryLocator(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public Maybe<IEffectFactory<TData>> Find<TData>() =>
        _scope.TryResolve<IEffectFactory<TData>>(out var instance)
            ? Maybe.Some(instance)
            : Maybe.None<IEffectFactory<TData>>();
}