using FunctionalUtilities;

namespace Strawhenge.Inventory.Effects
{
    public class NullEffectFactoryLocator : IEffectFactoryLocator
    {
        public static IEffectFactoryLocator Instance { get; } = new NullEffectFactoryLocator();

        NullEffectFactoryLocator()
        {
        }

        public Maybe<IEffectFactory<TData>> Find<TData>() => Maybe.None<IEffectFactory<TData>>();
    }
}