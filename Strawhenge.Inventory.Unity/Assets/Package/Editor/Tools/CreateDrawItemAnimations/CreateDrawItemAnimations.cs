using Strawhenge.Inventory.Unity.Animation;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class CreateDrawItemAnimations
    {
        public static void Create(CreateDrawItemAnimationsArgs args)
        {
            var (drawLeftId, drawRightId) = IdHelper.GenerateDrawItemIds(args.AnimatorController);
            var (putAwayLeftId, putAwayRightId) = IdHelper.GeneratePutAwayItemIds(args.AnimatorController);

            var layer = args.AnimatorController.layers
                .FirstOrDefault(x => x.name == args.LayerName);

            if (layer == null)
            {
                Debug.LogError($"Layer '{args.LayerName}' not found.", args.AnimatorController);
                return;
            }

            var rootStateMachine = layer.stateMachine;
            var drawStateMachine = layer.stateMachine.stateMachines
                .Select(x => x.stateMachine)
                .FirstOrDefault(x => x.behaviours.OfType<DrawItemStateMachine>().Any());
            var putAwayStateMachine = layer.stateMachine.stateMachines
                .Select(x => x.stateMachine)
                .FirstOrDefault(x => x.behaviours.OfType<PutAwayItemStateMachine>().Any());

            if (drawStateMachine == null)
            {
                Debug.LogError(
                    $"Layer '{args.LayerName}' does not contain '{nameof(DrawItemStateMachine)}'.",
                    args.AnimatorController);
                return;
            }

            if (putAwayStateMachine == null)
            {
                Debug.LogError(
                    $"Layer '{args.LayerName}' does not contain '{nameof(PutAwayItemStateMachine)}'.",
                    args.AnimatorController);
                return;
            }
            
            // TODO
        }
    }
}