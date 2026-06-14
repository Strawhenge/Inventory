using FunctionalUtilities;
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
            Maybe<AnimationClip> drawLeftHandAnimation,
            Maybe<AnimationClip> putAwayLeftHandAnimation,
            Maybe<AnimationClip> drawRightHandAnimation,
            Maybe<AnimationClip> putAwayRightHandAnimation)
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

        public Maybe<AnimationClip> DrawLeftHandAnimation { get; }

        public Maybe<AnimationClip> PutAwayLeftHandAnimation { get; }

        public Maybe<AnimationClip> DrawRightHandAnimation { get; }

        public Maybe<AnimationClip> PutAwayRightHandAnimation { get; }
    }
}