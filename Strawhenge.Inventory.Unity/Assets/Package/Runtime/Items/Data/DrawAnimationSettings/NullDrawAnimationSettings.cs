using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    public class NullDrawAnimationSettings : IDrawAnimationSettings
    {
        public static IDrawAnimationSettings Instance { get; } = new NullDrawAnimationSettings();

        NullDrawAnimationSettings()
        {
        }

        public Maybe<int> DrawLeftHandId => Maybe.None<int>();

        public Maybe<int> DrawRightHandId => Maybe.None<int>();

        public Maybe<int> PutAwayLeftHandId => Maybe.None<int>();

        public Maybe<int> PutAwayRightHandId => Maybe.None<int>();
    }
}