using Strawhenge.Inventory.Containers;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    public class EquippedItems : IEquippedItems
    {
        readonly IHands _hands;
        readonly IHolsters _holsters;

        public EquippedItems(IHands hands, IHolsters holsters)
        {
            _hands = hands;
            _holsters = holsters;
        }

        public Maybe<Item> GetItemInLeftHand()
        {
            return _hands.ItemInLeftHand;
        }

        public Maybe<Item> GetItemInRightHand()
        {
            return _hands.ItemInRightHand;
        }

        public IEnumerable<Item> GetItemsInHolsters()
        {
            return _holsters
                .GetAll()
                .Select(x => x.CurrentItem.AsEnumerable())
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
