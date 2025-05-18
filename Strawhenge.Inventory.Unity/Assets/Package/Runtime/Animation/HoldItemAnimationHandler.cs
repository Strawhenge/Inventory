using System.Collections.Generic;
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

        public void Hold(IEnumerable<string> flags)
        {
            foreach (var flag in flags)
                _animator.SetBool(flag, true);
        }

        public void Unhold(IEnumerable<string> flags)
        {
            foreach (var flag in flags)
                _animator.SetBool(flag, false);
        }
    }
}