using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Effects
{
    public abstract class EffectData
    {
        public static EffectData<TData> From<TData>(TData data) => new EffectData<TData>(data);

        internal abstract Type DataType { get; }

        internal abstract Maybe<Effect> Create(IEffectFactoryLocator factoryLocator);
    }

    public class EffectData<TData> : EffectData
    {
        readonly TData _data;

        internal EffectData(TData data)
        {
            _data = data;
        }

        internal override Type DataType => typeof(TData);

        internal override Maybe<Effect> Create(IEffectFactoryLocator factoryLocator) =>
            factoryLocator
                .Find<TData>()
                .Map(factory => factory.Create(_data));
    }
}