namespace Strawhenge.Inventory.Apparel
{
    public abstract class UnequipPreference
    {
        public static UnequipPreference Disappear { get; } = new PreferDisappear();

        public static UnequipPreference Drop { get; } = new PreferDrop();

        public abstract void PerformUnequip(IApparelView view);

        class PreferDisappear : UnequipPreference
        {
            public override void PerformUnequip(IApparelView view) => view.Unequip();
        }

        class PreferDrop : UnequipPreference
        {
            public override void PerformUnequip(IApparelView view) => view.Drop();
        }
    }
}