using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Item")]
    public class ItemScriptableObject : ScriptableObject, IItemData
    {
        [SerializeField] ItemScript prefab;
        [SerializeField] ItemSize size;
        [SerializeField] SerializedHoldItemData leftHandHoldItemData;
        [SerializeField] SerializedHoldItemData rightHandHoldItemData;
        [SerializeField] SerializedHolsterItemData[] holsterItemData;

        string IItemData.Name => name;

        ItemScript IItemData.Prefab => prefab;

        ItemSize IItemData.Size => size;

        IHoldItemData IItemData.LeftHandHoldData => leftHandHoldItemData;

        IHoldItemData IItemData.RightHandHoldData => rightHandHoldItemData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => holsterItemData;
    }
}