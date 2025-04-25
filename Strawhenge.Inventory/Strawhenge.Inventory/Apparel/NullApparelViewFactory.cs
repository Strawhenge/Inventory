namespace Strawhenge.Inventory.Apparel
{
    public class NullApparelViewFactory : IApparelViewFactory
    {
        public static IApparelViewFactory Instance { get; } = new NullApparelViewFactory();

        NullApparelViewFactory()
        {
        }

        public IApparelView Create(ApparelPieceData data) => NullApparelView.Instance;
    }
}