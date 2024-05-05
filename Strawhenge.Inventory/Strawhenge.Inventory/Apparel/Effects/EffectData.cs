using FunctionalUtilities;
using Strawhenge.Common.Factories;

namespace Strawhenge.Inventory.Apparel.Effects
{
    public abstract class EffectData
    {
        public static EffectData<TData> From<TData>(TData data) => new EffectData<TData>(data);

        internal abstract Maybe<Effect> Create(IAbstractFactory abstractFactory);
    }

    public class EffectData<TData> : EffectData
    {
        readonly TData _data;

        internal EffectData(TData data)
        {
            _data = data;
        }

        internal override Maybe<Effect> Create(IAbstractFactory abstractFactory) =>
            abstractFactory
                .TryCreate<IEffectFactory<TData>>()
                .Map(factory => factory.Create(_data));
    }
}