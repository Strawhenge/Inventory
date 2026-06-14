using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    class CreateDrawItemAnimationsArgs
    {
        public CreateDrawItemAnimationsArgs(
            AnimatorController animatorController,
            string layerName,
            string name,
            AnimationClip drawLeftHandAnimation,
            AnimationClip putAwayLeftHandAnimation,
            AnimationClip drawRightHandAnimation,
            AnimationClip putAwayRightHandAnimation)
        {
            AnimatorController = animatorController;
            LayerName = layerName;
            Name = name;
            DrawLeftHandAnimation = drawLeftHandAnimation;
            PutAwayLeftHandAnimation = putAwayLeftHandAnimation;
            DrawRightHandAnimation = drawRightHandAnimation;
            PutAwayRightHandAnimation = putAwayRightHandAnimation;
        }

        public AnimatorController AnimatorController { get; }

        public string LayerName { get; }

        public string Name { get; }

        public AnimationClip DrawLeftHandAnimation { get; }

        public AnimationClip PutAwayLeftHandAnimation { get; }

        public AnimationClip DrawRightHandAnimation { get; }

        public AnimationClip PutAwayRightHandAnimation { get; }
    }
}