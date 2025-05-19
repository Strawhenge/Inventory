namespace Strawhenge.Inventory.Apparel
{
    public class NullApparelView : IApparelView
    {
        public static IApparelView Instance { get; } = new NullApparelView();

        NullApparelView()
        {
        }

        public void Show()
        {
        }

        public void Hide()
        {
        }

        public void Drop()
        {
        }
    }
}