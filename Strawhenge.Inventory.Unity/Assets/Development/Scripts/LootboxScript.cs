using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Menu;
using UnityEngine;

public class LootboxScript : MonoBehaviour
{
    [SerializeField] LootCollectionScript _itemContainer;
    [SerializeField] LootMenuScript _itemContainerMenu;
    [SerializeField] Camera _camera;

    void Awake()
    {
        ComponentRefHelper.EnsureHierarchyComponent(ref _itemContainer, nameof(_itemContainer), this);
        ComponentRefHelper.EnsureSceneComponent(ref _itemContainerMenu, nameof(_itemContainerMenu), this);
        ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _itemContainerMenu.Close();

        if (ClickedOnLootbox())
            _itemContainerMenu.Open(_itemContainer.Source);
    }

    bool ClickedOnLootbox()
    {
        return Input.GetMouseButton(0) &&
               Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) &&
               hit.transform.root == transform;
    }

    public void OnStateChanged(ILootCollectionInfo info)
    {
        if (info.Count == 0)
            Destroy(gameObject);
    }
}