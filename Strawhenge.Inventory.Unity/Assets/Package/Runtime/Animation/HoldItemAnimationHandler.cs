using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class HoldItemAnimationHandler : IHoldItemAnimationHandler
    {
        private readonly Animator animator;

        public HoldItemAnimationHandler(Animator animator)
        {
            this.animator = animator;
        }

        public void Hold(int animationId) => animator.SetInteger(AnimatorParameters.HoldItem_AnimationId, animationId);

        public void Unhold() => animator.SetInteger(AnimatorParameters.HoldItem_AnimationId, 0);
    }

}