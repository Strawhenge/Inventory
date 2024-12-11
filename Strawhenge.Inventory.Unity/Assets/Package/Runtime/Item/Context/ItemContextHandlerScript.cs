using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Context
{
    public abstract class ItemContextHandlerScript : MonoBehaviour
    {
        public abstract void ContextIn(ItemContext context);

        public abstract void ContextOut(ItemContext context);
    }
}