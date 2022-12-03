using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory
{
    public interface IEquippedItems
    {
        Maybe<IItem> GetItemInLeftHand();

        Maybe<IItem> GetItemInRightHand();

        IEnumerable<IItem> GetItemsInHolsters();
    }
}
