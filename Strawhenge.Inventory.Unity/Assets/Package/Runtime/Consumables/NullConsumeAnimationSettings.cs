using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Consumables
{
    public class NullConsumeAnimationSettings : IConsumeAnimationSettings
    {
        public static IConsumeAnimationSettings Instance { get; } = new NullConsumeAnimationSettings();

        NullConsumeAnimationSettings()
        {
        }

        public Maybe<string> ConsumeLeftHandTrigger => Maybe.None<string>();

        public Maybe<string> ConsumeRightHandTrigger => Maybe.None<string>();
    }
}