using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    [Serializable]
    public class SerializedDrawAnimationSettings : IDrawAnimationSettings
    {
        [SerializeField] DrawItemAnimationScriptableObject _drawLeftHand;
        [SerializeField] DrawItemAnimationScriptableObject _drawRightHand;
        [SerializeField] PutAwayItemAnimationScriptableObject _putAwayLeftHand;
        [SerializeField] PutAwayItemAnimationScriptableObject _putAwayRightHand;

        public Maybe<int> DrawLeftHandId => _drawLeftHand != null
            ? Maybe.Some(_drawLeftHand.Id)
            : Maybe.None<int>();

        public Maybe<int> DrawRightHandId => _drawRightHand != null
            ? Maybe.Some(_drawRightHand.Id)
            : Maybe.None<int>();

        public Maybe<int> PutAwayLeftHandId => _putAwayLeftHand != null
            ? Maybe.Some(_putAwayLeftHand.Id)
            : Maybe.None<int>();

        public Maybe<int> PutAwayRightHandId => _putAwayRightHand != null
            ? Maybe.Some(_putAwayRightHand.Id)
            : Maybe.None<int>();
    }
}