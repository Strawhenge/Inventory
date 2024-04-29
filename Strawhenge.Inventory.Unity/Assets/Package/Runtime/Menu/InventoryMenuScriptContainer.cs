using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryMenuScriptContainer : IInventoryMenu
    {
        readonly ILogger _logger;
        InventoryMenuScript _menu;

        public InventoryMenuScriptContainer(ILogger logger)
        {
            _logger = logger;
        }

        internal void Set(InventoryMenuScript menu)
        {
            _menu = menu;
        }

        internal void Clear()
        {
            _menu = null;
        }

        void IInventoryMenu.Open()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
                return;
            }

            _menu.Open();
        }

        void IInventoryMenu.Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
                return;
            }

            _menu.Close();
        }
    }
}