using Strawhenge.Inventory.Unity.Animation;
using System.Linq;
using UnityEditor.Animations;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class IdHelper
    {
        public static (int left, int right) GenerateDrawItemIds(AnimatorController animatorController)
        {
            var emoteLayers = animatorController.GetLayersContaining<DrawItemStateMachine>();

            int highestId = 0;
            foreach (var layer in emoteLayers)
            {
                highestId = layer.stateMachine.defaultState.transitions
                    .SelectMany(x => x.conditions
                        .Where(y => y.parameter == AnimatorParameters.DrawItemId.Name)
                        .Select(y => (int)y.threshold))
                    .Prepend(highestId)
                    .Max();
            }

            return (left: highestId + 1, right: highestId + 2);
        }

        public static (int left, int right) GeneratePutAwayItemIds(AnimatorController animatorController)
        {
            var emoteLayers = animatorController.GetLayersContaining<PutAwayItemStateMachine>();

            int highestId = 0;
            foreach (var layer in emoteLayers)
            {
                highestId = layer.stateMachine.defaultState.transitions
                    .SelectMany(x => x.conditions
                        .Where(y => y.parameter == AnimatorParameters.PutAwayItemId.Name)
                        .Select(y => (int)y.threshold))
                    .Prepend(highestId)
                    .Max();
            }

            return (left: highestId + 1, right: highestId + 2);
        }
    }
}