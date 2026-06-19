using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    class HoldItemLeftAnimationHandler : IHoldItemAnimationHandler
    {
        readonly Animator _animator;

        public HoldItemLeftAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void Hold(int id)
        {
            _animator.SetInteger(AnimatorParameters.HoldItemLeftId, id);
        }

        public void Unhold()
        {
            _animator.SetInteger(AnimatorParameters.HoldItemLeftId, 0);
        }
    }
}