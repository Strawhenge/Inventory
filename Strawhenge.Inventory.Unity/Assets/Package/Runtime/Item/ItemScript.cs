using Strawhenge.Common;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;

        internal void ContextIn(Context itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextIn(itemContext));

        internal void ContextOut(Context itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextOut(itemContext));
    }
}