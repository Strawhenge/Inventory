using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterScript : MonoBehaviour
    {
        [FormerlySerializedAs("holster"), SerializeField] 
        HolsterScriptableObject _holster;

        public string HolsterName => _holster.Name;
    }
}