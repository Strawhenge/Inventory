using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.DrawAnimationSettings
{
    [Serializable]
    public class SerializedDrawAnimationSettings : IDrawAnimationSettings
    {
        [SerializeField] string _drawLeftHandTrigger = "Draw Item Left Hand";
        [SerializeField] string _drawRightHandTrigger = "Draw Item Right Hand";
        [SerializeField] string _putAwayLeftHandTrigger = "Put Away Item Left Hand";
        [SerializeField] string _putAwayRightHandTrigger = "Put Away Item Right Hand";

        public Maybe<string> DrawLeftHandTrigger => string.IsNullOrWhiteSpace(_drawLeftHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_drawLeftHandTrigger);

        public Maybe<string> DrawRightHandTrigger => string.IsNullOrWhiteSpace(_drawRightHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_drawRightHandTrigger);

        public Maybe<string> PutAwayLeftHandTrigger => string.IsNullOrWhiteSpace(_putAwayLeftHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_putAwayLeftHandTrigger);

        public Maybe<string> PutAwayRightHandTrigger => string.IsNullOrWhiteSpace(_putAwayRightHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_putAwayRightHandTrigger);
    }
}