using Strawhenge.Common.Unity.AnimatorBehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class ConsumeItemAnimationHandler : IConsumeItemAnimationHandler
    {
        readonly Animator _animator;
        readonly StateMachineEvents<ConsumeItemStateMachine> _events;

        public ConsumeItemAnimationHandler(Animator animator)
        {
            _animator = animator;

            _events = animator.AddEvents<ConsumeItemStateMachine>(
                subscribe: stateMachine => stateMachine.OnEnded = OnConsumeEnded,
                unsubscribe: _ => { });
        }

        public event Action Consumed;

        public void Consume(int animationId, bool invert)
        {
            _events.PrepareIfRequired();

            _animator.SetInteger(AnimatorParameters.ConsumeItemAnimationId, animationId);
            _animator.SetBool(AnimatorParameters.ConsumeItemInverted, invert);
            _animator.ResetTrigger(AnimatorParameters.ConsumeItem);
            _animator.SetTrigger(AnimatorParameters.ConsumeItem);
        }
        
        public void Interrupt()
        {
            _animator.ResetTrigger(AnimatorParameters.Interrupt);
            _animator.SetTrigger(AnimatorParameters.Interrupt);
        }

        void OnConsumeEnded()
        {
            _animator.ResetTrigger(AnimatorParameters.ConsumeItem);
            Consumed?.Invoke();
        }
    }
}