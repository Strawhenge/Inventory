using Strawhenge.Inventory.Unity.Apparel;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerScript : MonoBehaviour
    {
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;

        FixedItemContainerSource _source;

        public IItemContainerSource Source => _source;

        void Awake()
        {
            _source = new FixedItemContainerSource(_apparelPieces);
        }
    }
}