using Strawhenge.Inventory.Containers;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory
{
    public class EquippedItems : IEquippedItems
    {
        private readonly IHands hands;
        private readonly IHolsters holsters;

        public EquippedItems(IHands hands, IHolsters holsters)
        {
            this.hands = hands;
            this.holsters = holsters;
        }

        public Maybe<IItem> GetItemInLeftHand()
        {
            return hands.ItemInLeftHand;
        }

        public Maybe<IItem> GetItemInRightHand()
        {
            return hands.ItemInRightHand;
        }

        public IEnumerable<IItem> GetItemsInHolsters()
        {
            return holsters
                .GetAll()
                .Select(x => x.CurrentItem.AsEnumerable())
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
