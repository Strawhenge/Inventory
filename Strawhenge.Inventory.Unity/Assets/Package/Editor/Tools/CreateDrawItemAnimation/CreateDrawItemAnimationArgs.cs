using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.CreateDrawItemAnimation
{
    class CreateDrawItemAnimationArgs
    {
        public CreateDrawItemAnimationArgs(AnimatorController animatorController,
            string layerName,
            string name,
            AnimationClip animation, 
            bool mirrorAnimation)
        {
            AnimatorController = animatorController;
            LayerName = layerName;
            Name = name;
            Animation = animation;
            MirrorAnimation = mirrorAnimation;
        }

        public AnimatorController AnimatorController { get; }

        public string LayerName { get; }

        public string Name { get; }

        public AnimationClip Animation { get; }
        
        public bool MirrorAnimation { get; }
    }
}