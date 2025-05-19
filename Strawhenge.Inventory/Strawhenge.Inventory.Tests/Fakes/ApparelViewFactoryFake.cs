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

        public IApparelView Create(ApparelPiece data)
        {
            var view = new ApparelViewFake(data.Name);
            _tracker.Track(view);
            return view;
        }
    }
}