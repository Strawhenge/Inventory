using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Tests
{
    class ApparelViewFactoryFake : IApparelViewFactory
    {
        readonly ApparelViewCallTracker _tracker;

        public ApparelViewFactoryFake(ApparelViewCallTracker tracker)
        {
            _tracker = tracker;
        }

        public IApparelView Create(ApparelPiece apparelPiece)
        {
            var view = new ApparelViewFake(apparelPiece.Name);
            _tracker.Track(view);
            return view;
        }
    }
}