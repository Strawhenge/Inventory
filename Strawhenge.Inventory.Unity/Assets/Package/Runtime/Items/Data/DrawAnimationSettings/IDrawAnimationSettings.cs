using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    public interface IDrawAnimationSettings
    {
        Maybe<string> DrawLeftHandTrigger { get; }

        Maybe<string> DrawRightHandTrigger { get; }

        Maybe<string> PutAwayLeftHandTrigger { get; }

        Maybe<string> PutAwayRightHandTrigger { get; }
    }
}