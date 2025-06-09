namespace Strawhenge.Inventory.Unity.Items
{
    public class HandScriptsContainer
    {
        public HandScriptsContainer(LeftHandScript left, RightHandScript right)
        {
            Left = left;
            Right = right;
        }

        public HandScript Left { get; }

        public HandScript Right { get; }
    }
}