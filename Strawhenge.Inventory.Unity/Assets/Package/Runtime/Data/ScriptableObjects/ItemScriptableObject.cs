using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Consumables;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Item")]
    public class ItemScriptableObject : ScriptableObject, IItemData
    {
        [FormerlySerializedAs("prefab"), SerializeField]
        ItemScript _prefab;

        [FormerlySerializedAs("size"), SerializeField]
        ItemSize _size;

        [FormerlySerializedAs("leftHandHoldItemData"), SerializeField]
        SerializedHoldItemData _leftHandHoldItemData;

        [FormerlySerializedAs("rightHandHoldItemData"), SerializeField]
        SerializedHoldItemData _rightHandHoldItemData;

        [FormerlySerializedAs("holsterItemData"), SerializeField]
        SerializedHolsterItemData[] _holsterItemData;

        string IItemData.Name => name;

        ItemScript IItemData.Prefab => _prefab;

        ItemSize IItemData.Size => _size;

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldItemData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldItemData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => _holsterItemData;

        Maybe<IConsumableData> IItemData.ConsumableData => Maybe.None<IConsumableData>();
    }
}