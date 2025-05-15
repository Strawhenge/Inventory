using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullDrawAnimationSettings : IDrawAnimationSettings
    {
        public static NullDrawAnimationSettings Instance { get; } = new();

        NullDrawAnimationSettings()
        {
        }

        public Maybe<string> DrawLeftHandTrigger => Maybe.None<string>();

        public Maybe<string> DrawRightHandTrigger => Maybe.None<string>();

        public Maybe<string> PutAwayLeftHandTrigger => Maybe.None<string>();

        public Maybe<string> PutAwayRightHandTrigger => Maybe.None<string>();
    }
}