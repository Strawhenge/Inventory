using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class ItemContextHandlerScript : MonoBehaviour
    {
        public abstract void ContextIn(Context context);

        public abstract void ContextOut(Context context);
    }
}