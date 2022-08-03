using Strawhenge.Inventory.Apparel;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotScriptableObject _slot;

        public ApparelSlot ApparelSlot { get; private set; }

        void Awake()
        {
            ApparelSlot = new ApparelSlot(_slot.name);
        }
    }
}
