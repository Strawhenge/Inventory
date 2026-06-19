using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateHoldItemAnimation
{
    static class CreateHoldItemAnimation
    {
        public static void Create(
            AnimatorController animatorController,
            AnimationClip animation,
            string name,
            string layerName,
            bool mirrorAnimation,
            Hand hand)
        {
            var id = HoldItemIdHelper.Generate(animatorController);

            var layer = animatorController.layers
                .FirstOrDefault(x => x.name == layerName);

            if (layer == null)
            {
                Debug.LogError($"Layer '{layerName}' not found.", animatorController);
                return;
            }

            var state = layer.stateMachine.AddState(name);
            state.motion = animation;
            state.mirror = mirrorAnimation;

            if (hand.HasFlag(Hand.Left))
            {
                var beginTransition = layer.stateMachine.defaultState.AddTransition(state);
                beginTransition.hasExitTime = false;
                beginTransition
                    .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.HoldItemLeftId.Name);
            }

            if (hand.HasFlag(Hand.Right))
            {
                var beginTransition = layer.stateMachine.defaultState.AddTransition(state);
                beginTransition.hasExitTime = false;
                beginTransition
                    .AddCondition(AnimatorConditionMode.Equals, id, AnimatorParameters.HoldItemRightId.Name);
            }

            var endTransition = state.AddTransition(layer.stateMachine.defaultState);
            endTransition.hasExitTime = false;
            if (hand.HasFlag(Hand.Left))
                endTransition
                    .AddCondition(AnimatorConditionMode.NotEqual, id, AnimatorParameters.HoldItemLeftId.Name);
            if (hand.HasFlag(Hand.Right))
                endTransition
                    .AddCondition(AnimatorConditionMode.NotEqual, id, AnimatorParameters.HoldItemRightId.Name);

            EditorUtility.SetDirty(animatorController);
            AssetDatabase.SaveAssets();

            CreateScriptableObject(name, id);
        }

        static void CreateScriptableObject(string name, int id)
        {
            var scriptableObject = ScriptableObject.CreateInstance<HoldItemAnimationScriptableObject>();
            var serializedObject = new SerializedObject(scriptableObject);
            serializedObject.FindProperty(HoldItemAnimationScriptableObject.IdFieldName).intValue = id;
            serializedObject.ApplyModifiedProperties();

            var directoryPath = SelectionHelper.GetAssetDirectoryPath();
            var scriptableObjectPath = $"{directoryPath}/{name}.asset";
            AssetDatabase.CreateAsset(scriptableObject, scriptableObjectPath);
        }
    }
}