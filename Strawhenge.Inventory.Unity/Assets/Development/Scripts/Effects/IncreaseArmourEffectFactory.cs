using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

public class IncreaseArmourEffectFactory : IEffectFactory<IncreaseArmourEffectScriptableObject>
{
    readonly ILogger _logger;

    public IncreaseArmourEffectFactory(ILogger logger)
    {
        _logger = logger;
    }

    public Effect Create(IncreaseArmourEffectScriptableObject data)
    {
        return new IncreaseArmourEffect(data.Amount, _logger);
    }
}