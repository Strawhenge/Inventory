using Strawhenge.Common.Logging;
using System;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerMenuScriptContainer : IItemContainerMenu
    {
        readonly ILogger _logger;
        ItemContainerMenuScript _menu;

        public ItemContainerMenuScriptContainer(ILogger logger)
        {
            _logger = logger;
        }

        public event Action Opened;
        public event Action Closed;

        public bool IsOpen { get; private set; }

        public void Open(IItemContainerSource source)
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(ItemContainerMenuScript)}'.");
                return;
            }

            _menu.Open(source);
            IsOpen = true;
            Opened?.Invoke();
        }

        public void Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(ItemContainerMenuScript)}'.");
                return;
            }

            _menu.Close();
            IsOpen = false;
            Closed?.Invoke();
        }

        internal void Set(ItemContainerMenuScript menu)
        {
            _menu = menu;
        }
    }
}