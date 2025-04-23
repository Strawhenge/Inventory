using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

public class IncreaseHealthEffectFactory : IEffectFactory<IncreaseHealthEffectData>
{
    readonly ILogger _logger;

    public IncreaseHealthEffectFactory(ILogger logger)
    {
        _logger = logger;
    }

    public Effect Create(IncreaseHealthEffectData data)
    {
        return new IncreaseHealthEffect(data.Amount, _logger);
    }
}