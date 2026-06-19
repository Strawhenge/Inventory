using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateHoldItemAnimation
{
    public class CreateHoldItemAnimationWizard : ScriptableWizard
    {
        const string Name = "Hold Item Animation...";

        [MenuItem("Assets/Create/Strawhenge/Inventory/" + Name)]
        public static void ShowEditorWindow()
        {
            DisplayWizard<CreateHoldItemAnimationWizard>(Name, "Create");
        }

        AnimatorController _animatorController;
        string[] _layerNames = Array.Empty<string>();
        int _selectedLayerIndex;
        string _name;
        AnimationClip _animation;
        bool _mirrorAnimation;
        Hand _hand;

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

            _hand = (Hand)EditorGUILayout.EnumPopup(
                label: "Hand",
                selected: _hand);

            isValid =
                _animatorController != null &&
                _layerNames.Length > 0 &&
                !string.IsNullOrWhiteSpace(_name) &&
                _animation != null &&
                (_hand == Hand.Left || _hand == Hand.Right);

            return result;
        }

        void OnWizardCreate()
        {
            CreateHoldItemAnimation.Create(
                _animatorController,
                _animation,
                _name,
                _layerNames[_selectedLayerIndex],
                _mirrorAnimation,
                _hand);
        }

        static string[] GetLayers(AnimatorController animatorController) =>
            animatorController.layers
                .Select(x => x.name)
                .ToArray();
    }
}