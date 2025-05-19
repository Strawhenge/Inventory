using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity.Items.ItemData;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Loot
{
    public class LootCollectionScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject[] _items;
        [SerializeField] ApparelPieceScriptableObject[] _apparelPieces;
        [SerializeField] UnityEvent<ILootCollectionInfo> _stateChanged;

        void Awake()
        {
            var items = _items
                .Select(x => x.ToItem());

            var apparelPieces = _apparelPieces
                .Select(x => x.ToApparelPiece());

            Source = new LootCollectionSource(items, apparelPieces);
        }

        void Start()
        {
            Source.StateChanged += OnStateChanged;
            OnStateChanged();
        }

        public LootCollectionSource Source { get; private set; }

        public void Add(Item item) => Source.Add(item);

        public void Add(ApparelPiece apparelPiece) => Source.Add(apparelPiece);

        public void Merge(LootCollectionSource source) => Source.Merge(source);

        public LootCollectionSource CloneContainer() => Source.Clone();

        void OnStateChanged() => _stateChanged.Invoke(Source);
    }
}