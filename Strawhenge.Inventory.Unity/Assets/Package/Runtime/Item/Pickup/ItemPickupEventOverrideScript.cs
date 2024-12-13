using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemPickupEventOverrideScript : ItemPickupScript
    {
        [SerializeField] UnityEvent _onPickup;
        
        protected override void OnPickup()
        {
            _onPickup.Invoke();
        }
    }
}