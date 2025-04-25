using Strawhenge.Inventory.Effects;
using ILogger = Strawhenge.Common.Logging.ILogger;

public class IncreaseArmourEffect : Effect
{
    readonly int _amount;
    readonly ILogger _logger;

    public IncreaseArmourEffect(int amount, ILogger logger)
    {
        _amount = amount;
        _logger = logger;
    }

    protected override void PerformApply()
    {
        _logger.LogInformation($"Increased armour by {_amount}.");
    }

    protected override void PerformRevert()
    {
        _logger.LogInformation($"Decreased armour by {_amount}.");
    }
}