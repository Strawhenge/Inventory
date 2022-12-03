using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class ProduceItemAnimationHandler : IProduceItemAnimationHandler
    {
        readonly Animator _animator;
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

            var drawItemStateMachine = animator.GetBehaviour<DrawItemStateMachine>();
            drawItemStateMachine.OnEnded = OnDrawItemEnded;

            var putAwayItemStateMachine = animator.GetBehaviour<PutAwayItemStateMachine>();
            putAwayItemStateMachine.OnEnded = OnPutAwayItemEnded;
        }

        public void DrawItem(int animationId)
        {
            _animator.SetInteger(AnimatorParameters.DrawItemAnimationId, animationId);
            _animator.SetTrigger(AnimatorParameters.DrawItem);
        }

        public void PutAwayItem(int animationId)
        {
            _animator.SetInteger(AnimatorParameters.PutAwayItemAnimationId, animationId);
            _animator.SetTrigger(AnimatorParameters.PutAwayItem);
        }

        public void Interupt()
        {
            _animator.ResetTrigger(AnimatorParameters.Interrupt);
            _animator.SetTrigger(AnimatorParameters.Interrupt);
        }

        void OnGrabItem() => GrabItem?.Invoke();

        void OnReleaseItem() => ReleaseItem?.Invoke();

        void OnDrawItemEnded() => DrawEnded?.Invoke();

        void OnPutAwayItemEnded() => PutAwayEnded?.Invoke();
    }
}