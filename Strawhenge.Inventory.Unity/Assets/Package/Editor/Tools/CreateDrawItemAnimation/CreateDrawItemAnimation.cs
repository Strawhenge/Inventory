using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class CreateDrawItemAnimation
    {
        public static void Create(CreateDrawItemAnimationArgs args)
        {
            var id = DrawItemIdHelper.Generate(args.AnimatorController);

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

            if (drawStateMachine == null)
            {
                Debug.LogError(
                    $"Layer '{args.LayerName}' does not contain '{nameof(DrawItemStateMachine)}'.",
                    args.AnimatorController);
                return;
            }

            var state = drawStateMachine.AddState(args.Name);
            state.motion = args.Animation;

            var beginTransition = rootStateMachine.defaultState.AddTransition(state);
            beginTransition
                .AddCondition(AnimatorConditionMode.If, 0, AnimatorParameters.DrawItem.Name);
            beginTransition
                .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.DrawItemId.Name);
            beginTransition.hasExitTime = false;

            var endedTransition = state.AddExitTransition();
            endedTransition.hasExitTime = true;

            EditorUtility.SetDirty(args.AnimatorController);
            AssetDatabase.SaveAssets();

            CreateScriptableObject(args.Name, id);
        }

        static void CreateScriptableObject(string name, int id)
        {
            var scriptableObject = ScriptableObject.CreateInstance<DrawItemAnimationScriptableObject>();
            var serializedObject = new SerializedObject(scriptableObject);
            serializedObject.FindProperty(DrawItemAnimationScriptableObject.IdFieldName).intValue = id;
            serializedObject.ApplyModifiedProperties();

            var directoryPath = SelectionHelper.GetAssetDirectoryPath();
            var scriptableObjectPath = $"{directoryPath}/{name}.asset";
            AssetDatabase.CreateAsset(scriptableObject, scriptableObjectPath);
        }
    }
}