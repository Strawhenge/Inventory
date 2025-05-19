using Strawhenge.Common;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;

        internal void SetContext(Context context) =>
            _contextHandlers.ForEach(handler => handler.Handle(context));
    }
}