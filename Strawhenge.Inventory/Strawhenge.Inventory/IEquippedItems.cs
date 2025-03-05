using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    public interface IEquippedItems
    {
        Maybe<Item> GetItemInLeftHand();

        Maybe<Item> GetItemInRightHand();

        IEnumerable<Item> GetItemsInHolsters();
    }
}
