using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Unity.NewMenu;
using UnityEngine;

public class ToggleMenuScript : MonoBehaviour
{
    [SerializeField] InventoryMenuScript _menu;

    void Awake()
    {
        ComponentRefHelper.EnsureHierarchyComponent(ref _menu, nameof(_menu), this);

        if (_menu == null)
            enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_menu.IsOpen)
                _menu.Close();
            else
                _menu.Open();
        }
    }
}