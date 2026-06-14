using Strawhenge.Inventory.Unity.Animation;
using System.Linq;
using UnityEditor.Animations;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class PutAwayIdHelper
    {
        public static int GeneratePutAwayItemId(AnimatorController animatorController)
        {
            var layers = animatorController.GetLayersContaining<PutAwayItemStateMachine>();

            int highestId = 0;
            foreach (var layer in layers)
            {
                highestId = layer.stateMachine.defaultState.transitions
                    .SelectMany(x => x.conditions
                        .Where(y => y.parameter == AnimatorParameters.PutAwayItemId.Name)
                        .Select(y => (int)y.threshold))
                    .Prepend(highestId)
                    .Max();
            }

            return highestId + 1;
        }
    }
}
