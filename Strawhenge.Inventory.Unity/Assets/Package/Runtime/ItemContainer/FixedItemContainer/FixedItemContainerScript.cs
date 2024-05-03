using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class FixedItemContainerScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject[] _items;
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;

        FixedItemContainerSource _source;

        public IItemContainerSource Source => _source;

        public void Add(IItemData item) => _source.Add(item);

        public void Add(IApparelPieceData apparelPiece) => _source.Add(apparelPiece);

        void Awake()
        {
            _source = new FixedItemContainerSource(_items, _apparelPieces);
        }
    }
}