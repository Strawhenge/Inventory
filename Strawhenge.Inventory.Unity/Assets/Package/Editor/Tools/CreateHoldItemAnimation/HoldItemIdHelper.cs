using Strawhenge.Inventory.Unity.Animation;
using System.Linq;
using UnityEditor.Animations;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateHoldItemAnimation
{
    static class HoldItemIdHelper
    {
        public static int Generate(AnimatorController animatorController)
        {
            int highestId = 0;
            foreach (var layer in animatorController.layers)
            {
                highestId = layer.stateMachine.defaultState.transitions
                    .SelectMany(x => x.conditions
                        .Where(y =>
                            y.parameter == AnimatorParameters.HoldItemLeftId.Name ||
                            y.parameter == AnimatorParameters.HoldItemRightId.Name)
                        .Select(y => (int)y.threshold))
                    .Prepend(highestId)
                    .Max();
            }

            return highestId + 1;
        }
    }
}