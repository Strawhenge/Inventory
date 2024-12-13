using Strawhenge.Common;
using Strawhenge.Inventory.Unity.Items.Context;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;

        internal void ContextIn(ItemContext itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextIn(itemContext));

        internal void ContextOut(ItemContext itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextOut(itemContext));
    }
}