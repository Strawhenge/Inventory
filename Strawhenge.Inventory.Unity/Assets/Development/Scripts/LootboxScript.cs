using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Unity;
using System;
using UnityEngine;

public class LootboxScript : MonoBehaviour
{
    [SerializeField] FixedItemContainerScript _itemContainer;
    [SerializeField] ItemContainerMenuScript _itemContainerMenu;
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

    public void OnStateChanged(IFixedItemContainerInfo info)
    {
        if (info.Count == 0)
            Destroy(gameObject);
    }
}