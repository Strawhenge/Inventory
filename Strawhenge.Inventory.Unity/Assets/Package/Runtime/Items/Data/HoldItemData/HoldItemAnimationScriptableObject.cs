using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldItemData
{
    public class HoldItemAnimationScriptableObject : ScriptableObject
    {
        internal static string IdFieldName => nameof(_id);

        [SerializeField] int _id;

        public int Id => _id;
    }
}