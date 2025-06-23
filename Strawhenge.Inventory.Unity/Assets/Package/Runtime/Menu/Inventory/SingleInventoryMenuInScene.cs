using Strawhenge.Common.Logging;
using Strawhenge.Common.Unity;
using System;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class SingleInventoryMenuInScene : IInventoryMenu
    {
        public static SingleInventoryMenuInScene Instance { get; } = new();

        readonly ILogger _logger;
        InventoryMenuScript _menu;

        SingleInventoryMenuInScene()
        {
            _logger = new UnityLogger();
            SceneManager.sceneLoaded += (_, _) =>
            {
                if (!ReferenceEquals(_menu, null))
                {
                    _menu.Opened -= OnOpened;
                    _menu.Closed -= OnClosed;
                }

                _menu = Object.FindObjectOfType<InventoryMenuScript>();
                _menu.Opened += OnOpened;
                _menu.Closed += OnClosed;
            };
        }

        public event Action Opened;

        public event Action Closed;

        public bool IsOpen => !ReferenceEquals(_menu, null) && _menu.IsOpen;

        public void Open()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
                return;
            }

            _menu.Open();
        }

        public void Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
                return;
            }

            _menu.Close();
        }

        void OnOpened() => Opened?.Invoke();

        void OnClosed() => Closed?.Invoke();
    }
}