using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    public class ConsumeItemAnimationScriptableObject : ScriptableObject
    {
        internal static string IdFieldName => nameof(_id);

        [SerializeField] int _id;

        public int Id => _id;
    }
}