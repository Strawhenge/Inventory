using Strawhenge.Inventory.Unity.Animation;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class CreateInventoryLayer
    {
        public static void Create(
            AnimatorController animatorController,
            string layerName,
            AvatarMask avatarMask = null)
        {
            AddParameters(animatorController);
            AddLayer(animatorController, layerName, avatarMask);

            EditorUtility.SetDirty(animatorController);
            AssetDatabase.SaveAssets();
        }

        static void AddParameters(AnimatorController animatorController)
        {
            animatorController.EnsureParametersExist(
                (AnimatorParameters.DrawItem, AnimatorControllerParameterType.Trigger),
                (AnimatorParameters.DrawItemId, AnimatorControllerParameterType.Int),
                (AnimatorParameters.PutAwayItem, AnimatorControllerParameterType.Trigger),
                (AnimatorParameters.PutAwayItemId, AnimatorControllerParameterType.Int),
                (AnimatorParameters.ConsumeItem, AnimatorControllerParameterType.Trigger),
                (AnimatorParameters.ConsumeItemId, AnimatorControllerParameterType.Int));
        }

        static void AddLayer(AnimatorController animatorController, string layerName, AvatarMask avatarMask)
        {
            var layer = animatorController.CreateLayer(layerName, avatarMask);

            var rootStateMachine = layer.stateMachine;
            rootStateMachine.AddState("Default");

            var drawItemStateMachine = rootStateMachine.AddStateMachine("Draw Item");
            drawItemStateMachine.AddStateMachineBehaviour<DrawItemStateMachine>();
            var putAwayItemStateMachine = rootStateMachine.AddStateMachine("Put Away Item");
            putAwayItemStateMachine.AddStateMachineBehaviour<PutAwayItemStateMachine>();
            var consumeItemStateMachine = rootStateMachine.AddStateMachine("Consume Item");
            consumeItemStateMachine.AddStateMachineBehaviour<ConsumeItemStateMachine>();
        }
    }
}