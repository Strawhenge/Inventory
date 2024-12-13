using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
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
            _source = new FixedItemContainerSource(_items, _apparelPieces);
        }

        void Start()
        {
            _source.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        public IItemContainerSource Source => _source;

        public void Add(IItemData item) => _source.Add(item);

        public void Add(IApparelPieceData apparelPiece) => _source.Add(apparelPiece);

        public void MergeContainer(FixedItemContainerSource source) => _source.Merge(source);

        public FixedItemContainerSource CloneContainer() => _source.Clone();

        void OnStateChanged() => _stateChanged.Invoke(_source);
    }
}