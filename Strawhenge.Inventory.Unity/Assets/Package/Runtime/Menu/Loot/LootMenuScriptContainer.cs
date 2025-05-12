using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Loot;
using System;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class LootMenuScriptContainer : ILootMenu
    {
        readonly ILogger _logger;
        LootMenuScript _menu;

        public LootMenuScriptContainer(ILogger logger)
        {
            _logger = logger;
        }

        public event Action Opened;
        public event Action Closed;

        public bool IsOpen => _menu.IsOpen;

        public void Open(ILootSource source)
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(LootMenuScript)}'.");
                return;
            }

            _menu.Open(source);
            Opened?.Invoke();
        }

        public void Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(LootMenuScript)}'.");
                return;
            }

            _menu.Close();
            Closed?.Invoke();
        }

        internal void Set(LootMenuScript menu)
        {
            if (!ReferenceEquals(_menu, null))
            {
                _logger.LogError($"'{nameof(LootMenuScript)}' is already set.");
                return;
            }

            _menu = menu;
            _menu.Opened += () => Opened?.Invoke();
            _menu.Closed += () => Closed?.Invoke();
        }
    }
}