using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public abstract class EventScriptableObject : ScriptableObject
    {
        public abstract void Invoke(GameObject gameObject);
    }
}
