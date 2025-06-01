using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Loader;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Procedures;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityLogger = Strawhenge.Common.Unity.UnityLogger;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] LeftHandScript _leftHand;
        [SerializeField] RightHandScript _rightHand;
        [SerializeField] HolsterScript[] _holsters;
        [SerializeField] ApparelSlotScript[] _apparelSlots;

        [SerializeField, Tooltip("Optional. Will use 'this' transform if not set.")]
        Transform _dropPoint;

        [SerializeField] int _maxStoredItemsWeight;
        [SerializeField] LootCollectionScript _lootDropPrefab;
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
            
            var apparelViewFactory = new ApparelViewFactory(
                slotScripts,
                lootDrop: null,
                prefabInstantiatedEvents,
                logger);

            return new Inventory(
                itemProceduresFactory,
                apparelViewFactory,
                effectFactoryLocator: null,
                itemRepository: null,
                logger);
        }


        public bool IsConfigurationComplete { get; private set; }


        public ApparelSlotScriptsContainer ApparelSlotScriptsContainer { private get; set; }

        public HandScriptsContainer HandScriptsContainer { private get; set; }

        public HolsterScriptsContainer HolsterScriptsContainer { private get; set; }

        public ISetLootDropPrefab LootDrop { private get; set; }

        public InventoryLoader Loader { private get; set; }

        public InventoryInfoGenerator InfoGenerator { private get; set; }

        public PrefabInstantiatedEvents PrefabInstantiatedEvents { private get; set; }

        [ContextMenu(nameof(Interrupt))]
        public void Interrupt() => Inventory.Interrupt();

        public void Load(LoadInventoryData data)
        {
            this.InvokeAsSoonAs(
                condition: () => IsConfigurationComplete,
                action: () => Loader.Load(data));
        }

        public InventoryInfo GenerateCurrentInfo() => InfoGenerator.GenerateCurrentInfo();

        void Start()
        {
            // if (_leftHand != null && _rightHand != null)
            //     HandScriptsContainer.Initialize(_leftHand, _rightHand);
            // else
            //     Debug.LogError("Hand components not set.", this);
            //
            // foreach (var holster in _holsters)
            // {
            //     HolsterScriptsContainer.Add(holster);
            //     Inventory.Holsters.Add(holster.HolsterName);
            // }
            //
            // foreach (var apparelSlot in _apparelSlots)
            // {
            //     ApparelSlotScriptsContainer.Add(apparelSlot);
            //     Inventory.ApparelSlots.Add(apparelSlot.SlotName);
            // }
            //
            // if (_lootDropPrefab != null)
            //     LootDrop.Set(_lootDropPrefab);
            //
            // Inventory.StoredItems.SetWeightCapacity(_maxStoredItemsWeight);
            //
            // PrefabInstantiatedEvents.ItemInstantiated += item => _itemInstantiated.Invoke(item);
            // PrefabInstantiatedEvents.ApparelPieceInstantiated += apparel => _apparelPieceInstantiated.Invoke(apparel);
            //
            // IsConfigurationComplete = true;
        }
    }
}