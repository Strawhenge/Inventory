namespace Strawhenge.Inventory.Effects
{
    public class NullEffect : Effect
    {
        public static Effect Instance { get; } = new NullEffect();

        NullEffect()
        {
        }

        protected override void PerformApply()
        {
        }

        protected override void PerformRevert()
        {
        }
    }
}