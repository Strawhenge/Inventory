using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    public class NullConsumeAnimationSettings : IConsumeAnimationSettings
    {
        public static IConsumeAnimationSettings Instance { get; } = new NullConsumeAnimationSettings();

        NullConsumeAnimationSettings()
        {
        }


        public Maybe<int> ConsumeLeftHandId => Maybe.None<int>();
        
        public Maybe<int> ConsumeRightHandId => Maybe.None<int>();
    }
}