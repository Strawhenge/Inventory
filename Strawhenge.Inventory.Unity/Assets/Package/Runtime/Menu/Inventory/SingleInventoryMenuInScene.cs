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
            _logger = new UnityLogger(); // TODO Single instance logger (Requires change to Common).
            SceneManager.sceneLoaded += (_, _) => _menu = Object.FindObjectOfType<InventoryMenuScript>();
        }

        public event Action Opened;

        public event Action Closed;

        public bool IsOpen { get; private set; }

        public void Open()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
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
                _logger.LogError($"Missing '{nameof(InventoryMenuScript)}'.");
                return;
            }

            _menu.Close();
            IsOpen = false;
            Closed?.Invoke();
        }
    }
}