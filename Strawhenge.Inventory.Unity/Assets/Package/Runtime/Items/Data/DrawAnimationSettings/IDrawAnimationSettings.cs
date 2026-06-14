using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    public interface IDrawAnimationSettings
    {
        Maybe<int> DrawLeftHandId { get; }

        Maybe<int> DrawRightHandId { get; }

        Maybe<int> PutAwayLeftHandId { get; }

        Maybe<int> PutAwayRightHandId { get; }
    }
}