using FunctionalUtilities;
using Strawhenge.Common.Factories;

namespace Strawhenge.Inventory.Apparel.Effects
{
    public interface IEffectData
    {
        Maybe<Effect> Create(IAbstractFactory abstractFactory);
    }
}