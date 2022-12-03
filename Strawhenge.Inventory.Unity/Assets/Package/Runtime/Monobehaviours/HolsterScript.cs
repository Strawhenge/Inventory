using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class HolsterScript : MonoBehaviour
    {
        [FormerlySerializedAs("holster"), SerializeField] 
        HolsterScriptableObject _holster;

        public string HolsterName => _holster.Name;
    }
}