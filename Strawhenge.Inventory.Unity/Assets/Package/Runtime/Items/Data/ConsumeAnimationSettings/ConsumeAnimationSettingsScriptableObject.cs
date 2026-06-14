using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Consume Animation Settings")]
    public class ConsumeAnimationSettingsScriptableObject : ScriptableObject, IConsumeAnimationSettings
    {
        [SerializeField, Min(0)] int _consumeLeftHandId;
        [SerializeField, Min(0)] int _consumeRightHandId;

        public Maybe<int> ConsumeLeftHandId => _consumeLeftHandId > 0
            ? Maybe.Some(_consumeLeftHandId)
            : Maybe.None<int>();

        public Maybe<int> ConsumeRightHandId => _consumeRightHandId > 0
            ? Maybe.Some(_consumeRightHandId)
            : Maybe.None<int>();
    }
}