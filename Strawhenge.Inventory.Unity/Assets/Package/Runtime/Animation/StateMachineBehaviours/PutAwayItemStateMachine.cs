using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class PutAwayItemStateMachine : StateMachineBehaviour
    {
        public Action OnEnded = () => { };

        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) => OnEnded();
    }
}
