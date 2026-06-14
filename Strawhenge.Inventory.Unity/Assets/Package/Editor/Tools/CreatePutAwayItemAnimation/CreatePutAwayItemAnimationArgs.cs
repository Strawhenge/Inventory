using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    class CreatePutAwayItemAnimationArgs
    {
        public CreatePutAwayItemAnimationArgs(
            AnimatorController animatorController,
            string layerName,
            string name,
            AnimationClip animation)
        {
            AnimatorController = animatorController;
            LayerName = layerName;
            Name = name;
            Animation = animation;
        }

        public AnimatorController AnimatorController { get; }

        public string LayerName { get; }

        public string Name { get; }

        public AnimationClip Animation { get; }
    }
}
