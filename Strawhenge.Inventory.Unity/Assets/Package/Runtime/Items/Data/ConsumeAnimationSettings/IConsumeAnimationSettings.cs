using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    public interface IConsumeAnimationSettings
    {
        Maybe<int> ConsumeLeftHandId { get; }

        Maybe<int> ConsumeRightHandId { get; }
    }
}