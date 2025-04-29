using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class ItemContextHandlerScript : MonoBehaviour
    {
        public abstract void Handle(Context context);
    }
}