using Moq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using System.Linq;

namespace Strawhenge.Inventory.Tests.Context
{
    public class ItemContext
    {
        public IItem Instance { get; set; }

        public Mock<IItemView> ItemViewMock { get; set; }

        public Mock<IHolsterForItemView> FirstHolsterViewMock { get; set; }

        public Mock<IHolsterForItemView> SecondHolsterViewMock { get; set; }

        public Mock<IHolsterForItemView> ThirdHolsterViewMock { get; set; }

        public IHolsterForItem FirstHolster => Instance.Holsters.ElementAt(0);

        public IHolsterForItem SecondHolster => Instance.Holsters.ElementAt(1);

        public IHolsterForItem ThirdHolster => Instance.Holsters.ElementAt(2);
    }
}
