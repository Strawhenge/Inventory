using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotScriptableObject _slot;

        public string SlotName => _slot.name;
    }
}
