using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Animation;
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    public class CreateDrawItemAnimationsWizard : ScriptableWizard
    {
        const string Name = "Draw Item Animations...";

        [MenuItem("Assets/Create/Strawhenge/Inventory/" + Name)]
        public static void ShowEditorWindow()
        {
            DisplayWizard<CreateDrawItemAnimationsWizard>(Name, "Create");
        }

        AnimatorController _animatorController;
        string[] _layerNames = Array.Empty<string>();
        int _selectedLayerIndex;
        string _name;
        AnimationClip _drawLeftHandAnimation;
        AnimationClip _putAwayLeftHandAnimation;
        AnimationClip _drawRightHandAnimation;
        AnimationClip _putAwayRightHandAnimation;

        void OnEnable()
        {
            _animatorController = LastUsed.AnimatorController;
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

            _drawLeftHandAnimation = EditorGUILayout.ObjectField(
                label: "Draw Left Hand",
                obj: _drawLeftHandAnimation,
                objType: typeof(AnimationClip),
                allowSceneObjects: false) as AnimationClip;

            _putAwayLeftHandAnimation = EditorGUILayout.ObjectField(
                label: "Put Away Left Hand",
                obj: _putAwayLeftHandAnimation,
                objType: typeof(AnimationClip),
                allowSceneObjects: false) as AnimationClip;

            _drawRightHandAnimation = EditorGUILayout.ObjectField(
                label: "Draw Right Hand",
                obj: _drawRightHandAnimation,
                objType: typeof(AnimationClip),
                allowSceneObjects: false) as AnimationClip;

            _putAwayRightHandAnimation = EditorGUILayout.ObjectField(
                label: "Put Away Right Hand",
                obj: _putAwayRightHandAnimation,
                objType: typeof(AnimationClip),
                allowSceneObjects: false) as AnimationClip;

            isValid =
                _animatorController != null &&
                _layerNames.Length > 0 &&
                !string.IsNullOrWhiteSpace(_name) &&
                (
                    _drawLeftHandAnimation != null ||
                    _putAwayLeftHandAnimation != null ||
                    _drawRightHandAnimation != null ||
                    _putAwayRightHandAnimation != null);

            return result;
        }

        void OnWizardCreate()
        {
            CreateDrawItemAnimations.Create(new CreateDrawItemAnimationsArgs(
                _animatorController,
                _layerNames[_selectedLayerIndex],
                _name,
                Maybe.NotNull(_drawLeftHandAnimation),
                Maybe.NotNull(_putAwayLeftHandAnimation),
                Maybe.NotNull(_drawRightHandAnimation),
                Maybe.NotNull(_putAwayRightHandAnimation)));
        }

        static string[] GetLayers(AnimatorController animatorController)
        {
            return animatorController != null
                ? animatorController
                    .GetLayersContaining<DrawItemStateMachine, PutAwayItemStateMachine>()
                    .Select(x => x.name)
                    .ToArray()
                : Array.Empty<string>();
        }
    }
}