using Strawhenge.Common.Unity.Editor.Helpers;
using Strawhenge.Inventory.Unity.Animation;
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreatePutAwayItemAnimation
{
    public class CreatePutAwayItemAnimationWizard : ScriptableWizard
    {
        const string Name = "Put Away Item Animation...";

        [MenuItem("Assets/Create/Strawhenge/Inventory/" + Name)]
        public static void ShowEditorWindow()
        {
            DisplayWizard<CreatePutAwayItemAnimationWizard>(Name, "Create");
        }

        AnimatorController _animatorController;
        string[] _layerNames = Array.Empty<string>();
        int _selectedLayerIndex;
        string _name;
        AnimationClip _animation;
        bool _mirrorAnimation;

        void OnEnable()
        {
            _animatorController = LastUsed.AnimatorController;
            if (_animatorController != null)
                _layerNames = GetLayers(_animatorController);
        }

        protected override bool DrawWizardGUI()
        {
            var result = base.DrawWizardGUI();

            var animatorController = EditorGUILayout.ObjectField(
                label: "Animator Controller",
                obj: _animatorController,
                objType: typeof(AnimatorController),
                allowSceneObjects: false) as AnimatorController;

            if (_animatorController != animatorController)
            {
                _layerNames = GetLayers(animatorController);
                LastUsed.AnimatorController = animatorController;
            }

            _animatorController = animatorController;

            _selectedLayerIndex = EditorGUILayout.Popup(
                label: "Layer",
                selectedIndex: _selectedLayerIndex,
                displayedOptions: _layerNames);

            _name = EditorGUILayout.TextField(
                label: "Name",
                text: _name);

            _animation = EditorGUILayout.ObjectField(
                label: "Animation",
                obj: _animation,
                objType: typeof(AnimationClip),
                allowSceneObjects: false) as AnimationClip;

            if (_animation != null && string.IsNullOrWhiteSpace(_name))
                _name = _animation.name;

            _mirrorAnimation = EditorGUILayout.Toggle(
                label: "Mirror Animation",
                value: _mirrorAnimation);

            isValid =
                _animatorController != null &&
                _layerNames.Length > 0 &&
                !string.IsNullOrWhiteSpace(_name) &&
                _animation != null;

            return result;
        }

        void OnWizardCreate()
        {
            CreatePutAwayItemAnimation.Create(new CreatePutAwayItemAnimationArgs(
                _animatorController,
                _layerNames[_selectedLayerIndex],
                _name,
                _animation,
                _mirrorAnimation));
        }

        static string[] GetLayers(AnimatorController animatorController)
        {
            return animatorController != null
                ? animatorController
                    .GetLayersContaining<PutAwayItemStateMachine>()
                    .Select(x => x.name)
                    .ToArray()
                : Array.Empty<string>();
        }
    }
}