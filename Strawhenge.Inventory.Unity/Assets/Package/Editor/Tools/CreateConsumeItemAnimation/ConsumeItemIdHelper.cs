using Strawhenge.Inventory.Unity.Animation;
using System.Linq;
using UnityEditor.Animations;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateConsumeItemAnimation
{
    static class ConsumeItemIdHelper
    {
        public static int Generate(AnimatorController animatorController)
        {
            var layers = animatorController.GetLayersContaining<ConsumeItemStateMachine>();

            int highestId = 0;
            foreach (var layer in layers)
            {
                highestId = layer.stateMachine.defaultState.transitions
                    .SelectMany(x => x.conditions
                        .Where(y => y.parameter == AnimatorParameters.ConsumeItemId.Name)
                        .Select(y => (int)y.threshold))
                    .Prepend(highestId)
                    .Max();
            }

            return highestId + 1;
        }
    }
}
