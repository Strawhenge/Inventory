using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class AnimationEventReceiverScript : MonoBehaviour
    {
        public event Action GrabItemFromHolster;
        public event Action ReleaseItemIntoHolster;

        public void OnGrabItem() => GrabItemFromHolster?.Invoke();

        public void OnReleaseItem() => ReleaseItemIntoHolster?.Invoke();
    }
}