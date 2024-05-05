using System;
using Strawhenge.Common.Factories;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Apparel.Effects
{
    public class EffectFactory : IEffectFactory
    {
        readonly IAbstractFactory _abstractFactory;
        readonly ILogger _logger;

        public EffectFactory(IAbstractFactory abstractFactory, ILogger logger)
        {
            _abstractFactory = abstractFactory;
            _logger = logger;
        }

        public Effect Create(IEffectData data)
        {
            return data
                .Create(_abstractFactory)
                .Reduce(() =>
                {
                    _logger.LogError($"Effect factory for '{data.GetType().Name}' not found.");
                    return NullEffect.Instance;
                });
        }
    }
}