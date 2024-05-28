using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Consumables
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Consumable")]
    public class ConsumableDataScriptableObject : ScriptableObject, IConsumableData
    {
        [SerializeField] int _animationId;

        public int AnimationId => _animationId;
    }
}