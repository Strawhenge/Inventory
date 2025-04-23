using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

public class IncreaseHealthEffect : Effect
{
    readonly int _amount;
    readonly ILogger _logger;


    public IncreaseHealthEffect(int amount, ILogger logger)
    {
        _amount = amount;
        _logger = logger;
    }


    protected override void PerformApply()
    {
        _logger.LogInformation($"Increased health by {_amount}.");
    }

    protected override void PerformRevert()
    {
        _logger.LogInformation($"Decreased health by {_amount}.");
    }
}