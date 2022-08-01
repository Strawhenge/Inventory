namespace Strawhenge.Inventory.Items
{
    public abstract class ItemSize
    {
        public static ItemSize OneHanded => new OneHandedItem();

        public static ItemSize TwoHanded => new TwoHandedItem();

        public abstract bool IsTwoHanded { get; }

        class OneHandedItem : ItemSize
        {
            public override bool IsTwoHanded => false;
        }

        class TwoHandedItem : ItemSize
        {
            public override bool IsTwoHanded => true;
        }
    }
}