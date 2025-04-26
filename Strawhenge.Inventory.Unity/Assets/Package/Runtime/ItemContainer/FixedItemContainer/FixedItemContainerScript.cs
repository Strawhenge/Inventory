using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity
{
    public class FixedItemContainerScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject[] _items;
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;
        [SerializeField] UnityEvent<IFixedItemContainerInfo> _stateChanged;

        FixedItemContainerSource _source;

        void Awake()
        {
            var items = _items.Select(x => x.ToItemData());
            _source = new FixedItemContainerSource(items, _apparelPieces);
        }

        void Start()
        {
            _source.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        public IItemContainerSource Source => _source;

        public void Add(ItemData item) => _source.Add(item);

        public void Add(IApparelPieceData apparelPiece) => _source.Add(apparelPiece);

        public void MergeContainer(FixedItemContainerSource source) => _source.Merge(source);

        public FixedItemContainerSource CloneContainer() => _source.Clone();

        void OnStateChanged() => _stateChanged.Invoke(_source);
    }
}