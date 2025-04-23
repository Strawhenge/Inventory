using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

public class IncreaseHealthEffectFactory : IEffectFactory<IncreaseHealthEffectScriptableObject>
{
    readonly ILogger _logger;

    public IncreaseHealthEffectFactory(ILogger logger)
    {
        _logger = logger;
    }

    public Effect Create(IncreaseHealthEffectScriptableObject data)
    {
        return new IncreaseHealthEffect(data.Amount, _logger);
    }
}