using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Data
{
    [Serializable]
    public class SerializedItemData : IItemData
    {
        public string Name;
        public ItemScript Prefab;
        public ItemSize Size;
        public SerializedHoldItemData LeftHandHoldData;
        public SerializedHoldItemData RightHandHoldData;
        public SerializedHolsterItemData[] HolsterItemData;

        string IItemData.Name => Name;

        ItemScript IItemData.Prefab => Prefab;

        ItemSize IItemData.Size => Size;

        IHoldItemData IItemData.LeftHandHoldData => LeftHandHoldData;

        IHoldItemData IItemData.RightHandHoldData => RightHandHoldData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => HolsterItemData;
    }
}
