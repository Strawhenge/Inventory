using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class HolsterScript : MonoBehaviour
    {
        [SerializeField] HolsterScriptableObject holster;

        public string HolsterName => holster.Name;  
    }
}
