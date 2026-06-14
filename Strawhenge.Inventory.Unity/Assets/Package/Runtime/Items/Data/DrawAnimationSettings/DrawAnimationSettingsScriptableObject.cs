using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Draw Animation Settings")]
    public class DrawAnimationSettingsScriptableObject : ScriptableObject, IDrawAnimationSettings
    {
        [SerializeField] int _drawLeftHandId;
        [SerializeField] int _drawRightHandId;
        [SerializeField] int _putAwayLeftHandId;
        [SerializeField] int _putAwayRightHandId;

        public Maybe<int> DrawLeftHandId => _drawLeftHandId > 0
            ? Maybe.Some(_drawLeftHandId)
            : Maybe.None<int>();

        public Maybe<int> DrawRightHandId => _drawRightHandId > 0
            ? Maybe.Some(_drawRightHandId)
            : Maybe.None<int>();

        public Maybe<int> PutAwayLeftHandId => _putAwayLeftHandId > 0
            ? Maybe.Some(_putAwayLeftHandId)
            : Maybe.None<int>();

        public Maybe<int> PutAwayRightHandId => _putAwayRightHandId > 0
            ? Maybe.Some(_putAwayRightHandId)
            : Maybe.None<int>();
    }
}