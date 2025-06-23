using FunctionalUtilities;

namespace Strawhenge.Inventory.Effects
{
    public interface IEffectFactoryLocator
    {
        Maybe<IEffectFactory<TData>> Find<TData>();
    }
}