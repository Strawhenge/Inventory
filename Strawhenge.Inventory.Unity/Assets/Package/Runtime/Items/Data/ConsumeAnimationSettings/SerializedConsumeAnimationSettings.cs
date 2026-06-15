using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings
{
    [Serializable]
    public class SerializedConsumeAnimationSettings : IConsumeAnimationSettings
    {
        [SerializeField] ConsumeItemAnimationScriptableObject _consumeLeftHand;
        [SerializeField] ConsumeItemAnimationScriptableObject _consumeRightHand;

        public Maybe<int> ConsumeLeftHandId => _consumeLeftHand != null
            ? Maybe.Some(_consumeLeftHand.Id)
            : Maybe.None<int>();

        public Maybe<int> ConsumeRightHandId => _consumeRightHand != null
            ? Maybe.Some(_consumeRightHand.Id)
            : Maybe.None<int>();
    }
}