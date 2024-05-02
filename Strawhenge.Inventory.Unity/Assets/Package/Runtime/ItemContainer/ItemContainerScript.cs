using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject[] _items;
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;
        
        FixedItemContainerSource _source;

        public IItemContainerSource Source => _source;

        void Awake()
        {
            _source = new FixedItemContainerSource(_items, _apparelPieces);
        }
    }
}