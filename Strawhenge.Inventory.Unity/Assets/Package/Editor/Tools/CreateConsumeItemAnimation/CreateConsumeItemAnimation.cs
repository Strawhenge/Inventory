using Strawhenge.Common.Unity.Editor.Helpers;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateConsumeItemAnimation
{
    static class CreateConsumeItemAnimation
    {
        public static void Create(CreateConsumeItemAnimationArgs args)
        {
            var id = ParameterIdHelper.Generate(args.AnimatorController, AnimatorParameters.ConsumeItemId);

            var layer = args.AnimatorController.layers
                .FirstOrDefault(x => x.name == args.LayerName);

            if (layer == null)
            {
                Debug.LogError($"Layer '{args.LayerName}' not found.", args.AnimatorController);
                return;
            }

            var rootStateMachine = layer.stateMachine;
            var consumeStateMachine = layer.stateMachine.stateMachines
                .Select(x => x.stateMachine)
                .FirstOrDefault(x => x.behaviours.OfType<ConsumeItemStateMachine>().Any());

            if (consumeStateMachine == null)
            {
                Debug.LogError(
                    $"Layer '{args.LayerName}' does not contain '{nameof(ConsumeItemStateMachine)}'.",
                    args.AnimatorController);
                return;
            }

            var state = consumeStateMachine.AddState(args.Name);
            state.motion = args.Animation;
            state.mirror = args.MirrorAnimation;

            var beginTransition = rootStateMachine.defaultState.AddTransition(state);
            beginTransition
                .AddCondition(AnimatorConditionMode.If, 0, AnimatorParameters.ConsumeItem.Name);
            beginTransition
                .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.ConsumeItemId.Name);
            beginTransition.hasExitTime = false;

            var endedTransition = state.AddExitTransition();
            endedTransition.hasExitTime = true;

            EditorUtility.SetDirty(args.AnimatorController);
            AssetDatabase.SaveAssets();

            CreateScriptableObject(args.Name, id);
        }

        static void CreateScriptableObject(string name, int id)
        {
            var scriptableObject = ScriptableObject.CreateInstance<ConsumeItemAnimationScriptableObject>();
            var serializedObject = new SerializedObject(scriptableObject);
            serializedObject.FindProperty(ConsumeItemAnimationScriptableObject.IdFieldName).intValue = id;
            serializedObject.ApplyModifiedProperties();

            var directoryPath = SelectionHelper.GetAssetDirectoryPath();
            var scriptableObjectPath = $"{directoryPath}/{name}.asset";
            AssetDatabase.CreateAsset(scriptableObject, scriptableObjectPath);
        }
    }
}