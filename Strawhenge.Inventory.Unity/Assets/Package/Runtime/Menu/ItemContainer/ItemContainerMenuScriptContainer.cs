using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Loot;
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

        public bool IsOpen => _menu.IsOpen;

        public void Open(ILootSource source)
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(ItemContainerMenuScript)}'.");
                return;
            }

            _menu.Open(source);
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
            Closed?.Invoke();
        }

        internal void Set(ItemContainerMenuScript menu)
        {
            if (!ReferenceEquals(_menu, null))
            {
                _logger.LogError($"'{nameof(ItemContainerMenuScript)}' is already set.");
                return;
            }

            _menu = menu;
            _menu.Opened += () => Opened?.Invoke();
            _menu.Closed += () => Closed?.Invoke();
        }
    }
}