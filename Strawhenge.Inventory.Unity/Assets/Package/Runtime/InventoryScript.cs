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
        static ResourcesItemRepository _itemRepository;

        [SerializeField] LeftHandScript _leftHand;
        [SerializeField] RightHandScript _rightHand;
        [SerializeField] HolsterScript[] _holsters;
        [SerializeField] ApparelSlotScript[] _apparelSlots;

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
            // TODO Optionally get from LoggerScript (requires changes to Common).
            var logger = new UnityLogger(gameObject);

            // TODO Add serialized field and get only if missing (Common library), and handle missing scenario.
            var animator = GetComponent<Animator>();

            var holdItemAnimationHandler = new HoldItemAnimationHandler(animator);
            var produceItemAnimationHandler = new ProduceItemAnimationHandler(animator);
            var consumeItemAnimationHandler = new ConsumeItemAnimationHandler(animator);

            // TODO Handle missing hand fields scenario.
            _leftHand.AnimationHandler = holdItemAnimationHandler;
            _rightHand.AnimationHandler = holdItemAnimationHandler;
            var handScripts = new HandScriptsContainer(_leftHand, _rightHand);

            var holsterScripts = new HolsterScriptsContainer(_holsters);

            var dropPoint = new DropPoint(
                _dropPoint == null
                    ? transform
                    : _dropPoint);

            // TODO Publicly expose events, so they can be accessed by code if needed.
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

            var slotScripts = new ApparelSlotScriptsContainer(_apparelSlots);

            // TODO Improve menu access.
            var inventoryMenu = new InventoryMenuScriptContainer(logger);
            inventoryMenu.Set(FindObjectOfType<InventoryMenuScript>());
            var lootMenu = new LootMenuScriptContainer(logger);
            lootMenu.Set(FindObjectOfType<LootMenuScript>());

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
                _itemRepository ??= new ResourcesItemRepository(),
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