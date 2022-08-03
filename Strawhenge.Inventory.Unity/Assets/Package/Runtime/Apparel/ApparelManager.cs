using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelManager
    {
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly ILogger _logger;
        readonly ApparelSlot _defaultSlot;
        readonly List<ApparelSlot> _slots;
        readonly Dictionary<string, ApparelSlotScript> _slotsByName;

        public ApparelManager(
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelLayerAccessor layerAccessor,
            ILogger logger)
        {
            _gameObjectInitializer = gameObjectInitializer;
            _layerAccessor = layerAccessor;
            _logger = logger;
            _defaultSlot = new ApparelSlot("Default");
            _slots = new List<ApparelSlot>() { _defaultSlot };
            _slotsByName = new Dictionary<string, ApparelSlotScript>();
        }

        public IReadOnlyList<ApparelSlot> Slots => _slots.AsReadOnly();

        public ApparelPiece Create(ApparelPieceScriptableObject data)
        {
            if (!_slotsByName.TryGetValue(data.Slot, out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{data.Slot}'.");
                return new ApparelPiece(data.name, _defaultSlot, new NullApparelView());
            }

            var view = new ApparelView(
                _gameObjectInitializer,
                _layerAccessor,
                data.Prefab,
                slotScript.transform,
                data.Position,
                data.Rotation,
                data.Scale);

            return new ApparelPiece(data.name, slotScript.ApparelSlot, view);
        }

        internal void AddSlot(ApparelSlotScript slot)
        {
            if (_slotsByName.ContainsKey(slot.ApparelSlot.Name))
            {
                _logger.LogWarning($"Duplicate apparel slot: '{slot.ApparelSlot.Name}'.");
                return;
            }

            _slotsByName.Add(slot.ApparelSlot.Name, slot);
            _slots.Add(slot.ApparelSlot);
        }
    }
}
