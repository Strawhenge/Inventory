using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Effects
{
    public class EffectFactory
    {
        readonly IEffectFactoryLocator _factoryLocator;
        readonly ILogger _logger;

        public EffectFactory(IEffectFactoryLocator factoryLocator, ILogger logger)
        {
            _factoryLocator = factoryLocator;
            _logger = logger;
        }

        public Effect Create(EffectData data)
        {
            return data
                .Create(_factoryLocator)
                .Reduce(() =>
                {
                    _logger.LogError($"Effect factory for '{data.DataType.Name}' not found.");
                    return NullEffect.Instance;
                });
        }
    }
}