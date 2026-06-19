using Strawhenge.Common.Unity.Editor.Helpers;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreatePutAwayItemAnimation
{
    static class CreatePutAwayItemAnimation
    {
        public static void Create(CreatePutAwayItemAnimationArgs args)
        {
            var id = ParameterIdHelper.Generate(args.AnimatorController, AnimatorParameters.PutAwayItemId);

            var layer = args.AnimatorController.layers
                .FirstOrDefault(x => x.name == args.LayerName);

            if (layer == null)
            {
                Debug.LogError($"Layer '{args.LayerName}' not found.", args.AnimatorController);
                return;
            }

            var rootStateMachine = layer.stateMachine;
            var putAwayStateMachine = layer.stateMachine.stateMachines
                .Select(x => x.stateMachine)
                .FirstOrDefault(x => x.behaviours.OfType<PutAwayItemStateMachine>().Any());

            if (putAwayStateMachine == null)
            {
                Debug.LogError(
                    $"Layer '{args.LayerName}' does not contain '{nameof(PutAwayItemStateMachine)}'.",
                    args.AnimatorController);
                return;
            }

            var state = putAwayStateMachine.AddState(args.Name);
            state.motion = args.Animation;
            state.mirror = args.MirrorAnimation;

            var beginTransition = rootStateMachine.defaultState.AddTransition(state);
            beginTransition
                .AddCondition(AnimatorConditionMode.If, 0, AnimatorParameters.PutAwayItem.Name);
            beginTransition
                .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.PutAwayItemId.Name);
            beginTransition.hasExitTime = false;

            var endedTransition = state.AddExitTransition();
            endedTransition.hasExitTime = true;

            EditorUtility.SetDirty(args.AnimatorController);
            AssetDatabase.SaveAssets();

            CreateScriptableObject(args.Name, id);
        }

        static void CreateScriptableObject(string name, int id)
        {
            var scriptableObject = ScriptableObject.CreateInstance<PutAwayItemAnimationScriptableObject>();
            var serializedObject = new SerializedObject(scriptableObject);
            serializedObject.FindProperty(PutAwayItemAnimationScriptableObject.IdFieldName).intValue = id;
            serializedObject.ApplyModifiedProperties();

            var directoryPath = SelectionHelper.GetAssetDirectoryPath();
            var scriptableObjectPath = $"{directoryPath}/{name}.asset";
            AssetDatabase.CreateAsset(scriptableObject, scriptableObjectPath);
        }
    }
}