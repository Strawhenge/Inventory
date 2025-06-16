using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Effects;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Procedures;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Menu;
using UnityEngine;
using UnityEngine.Events;
using UnityLogger = Strawhenge.Common.Unity.UnityLogger;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] LeftHandScript _leftHand;
        [SerializeField] RightHandScript _rightHand;
        [SerializeField] SerializedList<HolsterScript> _holsters;
        [SerializeField] SerializedList<ApparelSlotScript> _apparelSlots;
        [SerializeField] Animator _animator;

        [SerializeField, Tooltip("Optional. Will use 'this' transform if not set.")]
        Transform _dropPoint;

        [SerializeField] LootCollectionScript _lootDropPrefab;
        [SerializeField, Tooltip("Optional.")] EffectFactoryLocatorScript _effectFactoryLocator;
        [SerializeField] int _maxStoredItemsWeight;
        [SerializeField] UnityEvent<ItemScript> _itemInstantiated;
        [SerializeField] UnityEvent<ApparelPieceScript> _apparelPieceInstantiated;

        Inventory _inventory;

        public Inventory Inventory => _inventory ??= CreateInventory();

        void Awake()
        {
            _inventory ??= CreateInventory();
        }

        Inventory CreateInventory()
        {
            ComponentRefHelper.EnsureHierarchyComponent(ref _leftHand, nameof(_leftHand), this);
            ComponentRefHelper.EnsureHierarchyComponent(ref _rightHand, nameof(_rightHand), this);
            ComponentRefHelper.EnsureHierarchyComponent(ref _animator, nameof(_animator), this);

            var logger = new UnityLogger(gameObject);

            var holdItemAnimationHandler = new HoldItemAnimationHandler(_animator);
            var produceItemAnimationHandler = new ProduceItemAnimationHandler(_animator);
            var consumeItemAnimationHandler = new ConsumeItemAnimationHandler(_animator);

            _leftHand.AnimationHandler = holdItemAnimationHandler;
            _rightHand.AnimationHandler = holdItemAnimationHandler;
            var handScripts = new HandScriptsContainer(_leftHand, _rightHand);

            var holsterScripts = new HolsterScriptsContainer(_holsters.Values);

            var dropPoint = new DropPoint(
                _dropPoint == null
                    ? transform
                    : _dropPoint);

            var prefabInstantiatedEvents = new PrefabInstantiatedEvents();
            prefabInstantiatedEvents.ItemInstantiated += item => _itemInstantiated.Invoke(item);
            prefabInstantiatedEvents.ApparelPieceInstantiated += apparel => _apparelPieceInstantiated.Invoke(apparel);

            var itemProceduresFactory = new ItemProceduresFactory(
                handScripts,
                holsterScripts,
                produceItemAnimationHandler,
                consumeItemAnimationHandler,
                dropPoint,
                prefabInstantiatedEvents);

            var slotScripts = new ApparelSlotScriptsContainer(_apparelSlots.Values);

            var inventoryMenu = SingleInventoryMenuInScene.Instance;
            var lootMenu = SingleLootMenuScriptInScene.Instance;

            var lootDrop = new LootDrop(
                _lootDropPrefab,
                inventoryMenu,
                lootMenu,
                dropPoint: dropPoint);

            var apparelViewFactory = new ApparelViewFactory(
                slotScripts,
                lootDrop,
                prefabInstantiatedEvents,
                logger);

            var effectFactoryLocator = _effectFactoryLocator == null
                ? NullEffectFactoryLocator.Instance
                : _effectFactoryLocator;

            var inventory = new Inventory(
                itemProceduresFactory,
                apparelViewFactory,
                effectFactoryLocator,
                ResourcesItemRepository.GetOrCreateInstance(),
                logger);

            foreach (var holsterName in holsterScripts.HolsterNames)
                inventory.Holsters.Add(holsterName);

            foreach (var slotName in slotScripts.SlotNames)
                inventory.ApparelSlots.Add(slotName);

            inventory.StoredItems.SetWeightCapacity(_maxStoredItemsWeight);
            return inventory;
        }

        [ContextMenu(nameof(Interrupt))]
        public void Interrupt() => Inventory.Interrupt();
    }
}