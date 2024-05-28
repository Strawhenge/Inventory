using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Consumables;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Data
{
    public interface IItemData
    {
        string Name { get; }

        ItemScript Prefab { get; }

        ItemSize Size { get; }

        IHoldItemData LeftHandHoldData { get; }

        IHoldItemData RightHandHoldData { get; }

        IEnumerable<IHolsterItemData> HolsterItemData { get; }
        
        Maybe<IConsumableData> ConsumableData { get; }
    }
}
