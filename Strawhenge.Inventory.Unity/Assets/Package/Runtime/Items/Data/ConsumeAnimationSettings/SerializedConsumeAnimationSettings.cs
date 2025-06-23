using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    [Serializable]
    public class SerializedConsumeAnimationSettings : IConsumeAnimationSettings
    {
        [SerializeField] string _consumeLeftHandTrigger;
        [SerializeField] string _consumeRightHandTrigger;

        public Maybe<string> ConsumeLeftHandTrigger => string.IsNullOrWhiteSpace(_consumeLeftHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_consumeLeftHandTrigger);

        public Maybe<string> ConsumeRightHandTrigger => string.IsNullOrWhiteSpace(_consumeRightHandTrigger)
            ? Maybe.None<string>()
            : Maybe.Some(_consumeRightHandTrigger);
    }
}