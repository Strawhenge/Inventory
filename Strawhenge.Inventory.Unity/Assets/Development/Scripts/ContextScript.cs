using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Unity;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContextScript : MonoBehaviour
{
    [SerializeField] InventoryMenuScript _inventoryMenu;
    [SerializeField] LootMenuScript _lootMenu;
    [SerializeField] Camera _camera;

    void Awake()
    {
        ComponentRefHelper.EnsureSceneComponent(ref _inventoryMenu, nameof(_inventoryMenu), this);
        ComponentRefHelper.EnsureSceneComponent(ref _lootMenu, nameof(_lootMenu), this);
        ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventoryMenu.IsOpen)
                _inventoryMenu.Close();
            else
                _inventoryMenu.Open();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _lootMenu.IsOpen)
            _lootMenu.Close();

        if (Input.GetMouseButton(1))
            TryLoot();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            ReloadScene();
    }

    void TryLoot()
    {
        if (!Input.GetMouseButton(1) ||
            !Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            return;

        if (hit.transform.TryGetComponent<InventoryScript>(out var inventory))
        {
            _lootMenu.Open(inventory.Inventory.Loot());
            return;
        }

        if (hit.transform.TryGetComponent<LootCollectionScript>(out var lootCollection))
            _lootMenu.Open(lootCollection.Source);
    }

    [ContextMenu(nameof(ReloadScene))]
    public void ReloadScene()
    {
        Debug.Log("Reloading Scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}