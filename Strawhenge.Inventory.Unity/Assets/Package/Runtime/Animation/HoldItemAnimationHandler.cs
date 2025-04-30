using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class HoldItemAnimationHandler
    {
        readonly Animator _animator;

        public HoldItemAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void Hold(int animationId) => _animator.SetInteger(AnimatorParameters.HoldItemAnimationId, animationId);

        public void Unhold() => _animator.SetInteger(AnimatorParameters.HoldItemAnimationId, 0);
    }
}