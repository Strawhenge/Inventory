using Strawhenge.Common.Logging;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Loot;
using System;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class SingleLootMenuScriptInScene : ILootMenu
    {
        public static SingleLootMenuScriptInScene Instance { get; } = new();

        readonly ILogger _logger;
        LootMenuScript _menu;

        SingleLootMenuScriptInScene()
        {
            _logger = GlobalUnityLogger.Instance;
            SceneManager.sceneLoaded += (_, _) =>
            {
                if (!ReferenceEquals(_menu, null))
                {
                    _menu.Opened -= OnOpened;
                    _menu.Closed -= OnClosed;
                }

                _menu = Object.FindObjectOfType<LootMenuScript>();
                if (ReferenceEquals(_menu, null))
                {
                    _logger.LogInformation($"'{nameof(LootMenuScript)}' not found in scene.");
                    return;
                }

                _menu.Opened += OnOpened;
                _menu.Closed += OnClosed;
            };
        }

        public event Action Opened;

        public event Action Closed;

        public bool IsOpen => !ReferenceEquals(_menu, null) && _menu.IsOpen;

        public void Open(ILootSource source)
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(LootMenuScript)}'.");
                return;
            }

            _menu.Open(source);
        }

        public void Close()
        {
            if (ReferenceEquals(_menu, null))
            {
                _logger.LogError($"Missing '{nameof(LootMenuScript)}'.");
                return;
            }

            _menu.Close();
        }

        void OnOpened() => Opened?.Invoke();

        void OnClosed() => Closed?.Invoke();
    }
}