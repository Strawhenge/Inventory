using Strawhenge.Common.Unity.AnimatorBehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class ConsumeItemAnimationHandler
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

        public void Consume(string trigger)
        {
            _events.PrepareIfRequired();
            _animator.SetTrigger(trigger);
        }

        public void Interrupt()
        {
            _animator.ResetTrigger(AnimatorParameters.Interrupt);
            _animator.SetTrigger(AnimatorParameters.Interrupt);
        }

        void OnConsumeEnded() => Consumed?.Invoke();
    }
}