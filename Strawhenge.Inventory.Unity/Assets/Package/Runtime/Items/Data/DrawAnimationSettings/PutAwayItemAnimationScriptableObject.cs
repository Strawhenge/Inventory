using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    public class PutAwayItemAnimationScriptableObject : ScriptableObject
    {
        internal static string IdFieldName => nameof(_id); 

        [SerializeField] int _id;

        public int Id => _id;
    }
}