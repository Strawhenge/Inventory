using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity
{
    public class LootCollectionScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject[] _items;
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;
        [SerializeField] UnityEvent<ILootCollectionInfo> _stateChanged;

        void Awake()
        {
            var items = _items
                .Select(x => x.ToItemData());

            var apparelPieces = _apparelPieces
                .Select(x => x.ToApparelPieceData());

            Source = new LootCollectionSource(items, apparelPieces);
        }

        void Start()
        {
            Source.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        public LootCollectionSource Source { get; private set; }

        public void Add(ItemData item) => Source.Add(item);

        public void Add(ApparelPieceData apparelPiece) => Source.Add(apparelPiece);

        public void Merge(LootCollectionSource source) => Source.Merge(source);

        public LootCollectionSource CloneContainer() => Source.Clone();

        void OnStateChanged() => _stateChanged.Invoke(Source);
    }
}