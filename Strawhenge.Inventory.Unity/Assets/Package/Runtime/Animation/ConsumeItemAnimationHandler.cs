using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.AnimatorBehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    class ConsumeItemAnimationHandler
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

        public void Consume(int consumeItemId)
        {
            _events.PrepareIfRequired();

            _animator.SetInteger(AnimatorParameters.ConsumeItemId, consumeItemId);
            _animator.SetTrigger(AnimatorParameters.ConsumeItem);
        }

        void OnConsumeEnded() => Consumed?.Invoke();
    }
}