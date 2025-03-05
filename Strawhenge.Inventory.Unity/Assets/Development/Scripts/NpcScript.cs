using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Unity;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    [SerializeField] LootInventoryScript _lootInventory;
    [SerializeField] ItemContainerMenuScript _itemContainerMenu;
    [SerializeField] Camera _camera;

    void Awake()
    {
        ComponentRefHelper.EnsureHierarchyComponent(ref _lootInventory, nameof(_lootInventory), this);
        ComponentRefHelper.EnsureSceneComponent(ref _itemContainerMenu, nameof(_itemContainerMenu), this);
        ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _itemContainerMenu.Close();

        if (ClickedOnNpc() && _lootInventory.CanBeLooted(out var itemSource))
            _itemContainerMenu.Open(itemSource);
    }

    bool ClickedOnNpc()
    {
        return Input.GetMouseButton(0) &&
               Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) &&
               hit.transform.root == transform;
    }
}