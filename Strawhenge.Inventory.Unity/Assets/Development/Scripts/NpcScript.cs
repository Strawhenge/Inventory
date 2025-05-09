using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    [SerializeField] InventoryScript _inventory;
    [SerializeField] LootMenuScript _itemContainerMenu;
    [SerializeField] Camera _camera;

    InventoryLootSource _lootSource;

    void Awake()
    {
        ComponentRefHelper.EnsureHierarchyComponent(ref _inventory, nameof(_inventory), this);
        ComponentRefHelper.EnsureSceneComponent(ref _itemContainerMenu, nameof(_itemContainerMenu), this);
        ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);

        _lootSource = new InventoryLootSource(_inventory.Inventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _itemContainerMenu.Close();

        if (ClickedOnNpc())
            _itemContainerMenu.Open(_lootSource);
    }

    bool ClickedOnNpc()
    {
        return Input.GetMouseButton(0) &&
               Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) &&
               hit.transform.root == transform;
    }
}