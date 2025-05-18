using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.AnimatorBehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class ProduceItemAnimationHandler
    {
        readonly Animator _animator;
        readonly StateMachineEvents<DrawItemStateMachine> _drawItemEvents;
        readonly StateMachineEvents<PutAwayItemStateMachine> _putAwayItemEvents;

        public event Action GrabItem;
        public event Action ReleaseItem;
        public event Action DrawEnded;
        public event Action PutAwayEnded;

        public ProduceItemAnimationHandler(Animator animator)
        {
            _animator = animator;

            var eventsScript = animator.GetOrAddComponent<AnimationEventReceiverScript>();
            eventsScript.GrabItemFromHolster += OnGrabItem;
            eventsScript.ReleaseItemIntoHolster += OnReleaseItem;

            _drawItemEvents = animator.AddEvents<DrawItemStateMachine>(
                subscribe: stateMachine => stateMachine.OnEnded = OnDrawItemEnded,
                unsubscribe: _ => { });

            _putAwayItemEvents = animator.AddEvents<PutAwayItemStateMachine>(
                subscribe: stateMachine => stateMachine.OnEnded = OnPutAwayItemEnded,
                unsubscribe: _ => { });
        }

        public void DrawItem(string trigger)
        {
            _drawItemEvents.PrepareIfRequired();
            _animator.SetTrigger(trigger);
        }

        public void PutAwayItem(string trigger)
        {
            _putAwayItemEvents.PrepareIfRequired();
            _animator.SetTrigger(trigger);
        }

        void OnGrabItem() => GrabItem?.Invoke();

        void OnReleaseItem() => ReleaseItem?.Invoke();

        void OnDrawItemEnded() => DrawEnded?.Invoke();

        void OnPutAwayItemEnded() => PutAwayEnded?.Invoke();
    }
}