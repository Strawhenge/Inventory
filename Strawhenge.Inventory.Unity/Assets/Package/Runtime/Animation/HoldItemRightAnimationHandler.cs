using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    class HoldItemRightAnimationHandler : IHoldItemAnimationHandler
    {
        readonly Animator _animator;

        public HoldItemRightAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void Hold(int id)
        {
            _animator.SetInteger(AnimatorParameters.HoldItemRightId, id);
        }

        public void Unhold()
        {
            _animator.SetInteger(AnimatorParameters.HoldItemRightId, 0);
        }
    }
}