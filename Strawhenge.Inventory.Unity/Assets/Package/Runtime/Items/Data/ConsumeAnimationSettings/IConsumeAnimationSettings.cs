using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    public interface IConsumeAnimationSettings
    {
        Maybe<string> ConsumeLeftHandTrigger { get; }

        Maybe<string> ConsumeRightHandTrigger { get; }
    }
}