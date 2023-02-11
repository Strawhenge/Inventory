using Strawhenge.Common.Unity.AnimatorBehaviours;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class DrawItemStateMachine : StateMachineBehaviour, IHasDestroyedEvent
    {
        public event Action Destroyed;

        public Action OnEnded = () => { };

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash) => OnEnded();

        void OnDestroy() => Destroyed?.Invoke();
    }
}