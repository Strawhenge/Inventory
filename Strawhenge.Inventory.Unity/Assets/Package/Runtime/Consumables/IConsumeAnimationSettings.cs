using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Consumables
{
    public interface IConsumeAnimationSettings
    {
        Maybe<string> ConsumeLeftHandTrigger { get; }

        Maybe<string> ConsumeRightHandTrigger { get; }
    }
}