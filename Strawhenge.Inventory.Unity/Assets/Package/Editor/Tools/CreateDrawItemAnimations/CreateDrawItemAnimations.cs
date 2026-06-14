using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    static class CreateDrawItemAnimations
    {
        public static void Create(CreateDrawItemAnimationsArgs args)
        {
            var (drawLeftId, drawRightId) = IdHelper.GenerateDrawItemIds(args.AnimatorController);
            var (putAwayLeftId, putAwayRightId) = IdHelper.GeneratePutAwayItemIds(args.AnimatorController);

            if (!args.DrawLeftHandAnimation.HasSome()) drawLeftId = 0;
            if (!args.DrawRightHandAnimation.HasSome()) drawRightId = 0;
            if (!args.PutAwayLeftHandAnimation.HasSome()) putAwayLeftId = 0;
            if (!args.PutAwayRightHandAnimation.HasSome()) putAwayRightId = 0;

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

            args.DrawLeftHandAnimation.Do(clip =>
                CreateAnimation(clip, drawLeftId, args.Name, drawStateMachine, rootStateMachine));
            args.PutAwayLeftHandAnimation.Do(clip =>
                CreateAnimation(clip, putAwayLeftId, args.Name, putAwayStateMachine, rootStateMachine));
            args.DrawRightHandAnimation.Do(clip =>
                CreateAnimation(clip, drawRightId, args.Name, drawStateMachine, rootStateMachine));
            args.PutAwayRightHandAnimation.Do(clip =>
                CreateAnimation(clip, putAwayRightId, args.Name, putAwayStateMachine, rootStateMachine));

            EditorUtility.SetDirty(args.AnimatorController);
            AssetDatabase.SaveAssets();

            CreateScriptableObject(args.Name, drawLeftId, drawRightId, putAwayLeftId, putAwayRightId);
        }

        static void CreateAnimation(
            AnimationClip clip,
            int id,
            string name,
            AnimatorStateMachine stateMachine,
            AnimatorStateMachine rootStateMachine)
        {
            var state = stateMachine.AddState(name);
            state.motion = clip;

            var beginTransition = rootStateMachine.defaultState.AddTransition(state);
            beginTransition
                .AddCondition(AnimatorConditionMode.If, 0, AnimatorParameters.DrawItem.Name);
            beginTransition
                .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.DrawItemId.Name);
            beginTransition.hasExitTime = false;

            var endedTransition = state.AddExitTransition();
            endedTransition.hasExitTime = true;
        }

        static void CreateScriptableObject(
            string name,
            int drawLeftId,
            int drawRightId,
            int putAwayLeftId,
            int putAwayRightId)
        {
            var scriptableObject = ScriptableObject.CreateInstance<DrawAnimationSettingsScriptableObject>();
            var serializedObject = new SerializedObject(scriptableObject);

            serializedObject
                .FindProperty(DrawAnimationSettingsScriptableObject.DrawLeftHandIdFieldName)
                .intValue = drawLeftId;
            serializedObject
                .FindProperty(DrawAnimationSettingsScriptableObject.DrawRightHandIdFieldName)
                .intValue = drawRightId;
            serializedObject
                .FindProperty(DrawAnimationSettingsScriptableObject.PutAwayLeftHandIdFieldName)
                .intValue = putAwayLeftId;
            serializedObject
                .FindProperty(DrawAnimationSettingsScriptableObject.PutAwayRightHandIdFieldName)
                .intValue = putAwayRightId;

            serializedObject.ApplyModifiedProperties();

            var directoryPath = SelectionHelper.GetAssetDirectoryPath();
            var scriptableObjectPath = $"{directoryPath}/{name}.asset";
            AssetDatabase.CreateAsset(scriptableObject, scriptableObjectPath);
        }
    }
}