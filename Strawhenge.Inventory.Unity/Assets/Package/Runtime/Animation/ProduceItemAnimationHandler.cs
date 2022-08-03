﻿using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class ProduceItemAnimationHandler : IProduceItemAnimationHandler
    {
        private readonly Animator animator;
        public event Action GrabItem;
        public event Action ReleaseItem;
        public event Action DrawEnded;
        public event Action PutAwayEnded;

        public ProduceItemAnimationHandler(Animator animator)
        {
            this.animator = animator;

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
            animator.SetInteger(AnimatorParameters.DrawItem_AnimationId, animationId);
            animator.SetTrigger(AnimatorParameters.DrawItem);
        }

        public void PutAwayItem(int animationId)
        {
            animator.SetInteger(AnimatorParameters.PutAwayItem_AnimationId, animationId);
            animator.SetTrigger(AnimatorParameters.PutAwayItem);
        }

        public void Interupt()
        {
            animator.ResetTrigger(AnimatorParameters.Interupt);
            animator.SetTrigger(AnimatorParameters.Interupt);
        }

        private void OnGrabItem() => GrabItem?.Invoke();

        private void OnReleaseItem() => ReleaseItem?.Invoke();

        private void OnDrawItemEnded() => DrawEnded?.Invoke();

        private void OnPutAwayItemEnded() => PutAwayEnded?.Invoke();
    }
}