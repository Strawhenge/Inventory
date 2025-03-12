namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class HoldItemData
    {
        public HoldItemData(IHoldItemData left, IHoldItemData right)
        {
            Left = left;
            Right = right;
        }

        public IHoldItemData Left { get; }

        public IHoldItemData Right { get; }
    }
}