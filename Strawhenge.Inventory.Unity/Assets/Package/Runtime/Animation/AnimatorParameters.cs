using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    static class AnimatorParameters
    {
        public static int DrawItem => Animator.StringToHash("Draw Item");

        public static int DrawItemId => Animator.StringToHash("Draw Item ID");

        public static int PutAwayItem => Animator.StringToHash("Put Away Item");

        public static int PutAwayItemId => Animator.StringToHash("Put Away Item ID");

        public static int ConsumeItem => Animator.StringToHash("Consume Item");

        public static int ConsumeItemId => Animator.StringToHash("Consume Item ID");
    }
}