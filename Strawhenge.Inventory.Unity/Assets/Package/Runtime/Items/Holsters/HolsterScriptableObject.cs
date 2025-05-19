using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Holster")]
    public class HolsterScriptableObject : ScriptableObject
    {
        public string Name => name;
    }
}