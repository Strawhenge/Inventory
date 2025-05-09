using Strawhenge.Common.Logging;
using System;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class InventoryMenuScriptContainer : IInventoryMenu
    {
        readonly ILogger _logger;
        NewMenu.InventoryMenuScript _menu;

        public InventoryMenuScriptContainer(ILogger logger)
        {
            _logger = logger;
        }

        public event Action Opened;

        public event Action Closed;

        public bool IsOpen { get; private set; }

        public void Open()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(NewMenu.InventoryMenuScript)}'.");
                return;
            }

            _menu.Open();
            IsOpen = true;
            Opened?.Invoke();
        }

        public void Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(NewMenu.InventoryMenuScript)}'.");
                return;
            }

            _menu.Close();
            IsOpen = false;
            Closed?.Invoke();
        }

        internal void Set(NewMenu.InventoryMenuScript menu)
        {
            _menu = menu;
        }

        internal void Clear()
        {
            _menu = null;
        }
    }
}