using FunctionalUtilities;
using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public static class MonobehaviourExtensions
    {
        // TODO move to Common
        public static Maybe<Coroutine> InvokeAsSoonAs(
            this MonoBehaviour monoBehaviour,
            Func<bool> condition,
            Action action)
        {
            if (condition())
            {
                action();
                return Maybe.None<Coroutine>();
            }

            return monoBehaviour.StartCoroutine(WaitUntilConditionMetCoroutine());

            IEnumerator WaitUntilConditionMetCoroutine()
            {
                yield return new WaitUntil(condition);
                action();
            }
        }
    }
}