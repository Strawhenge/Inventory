using Strawhenge.Inventory.Unity.Items;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    /// <summary>
    /// A temporary copy of an item's held prefab, placed under a hand or holster point so its
    /// offset can be posed with the Scene view handles. Never saved to the scene.
    /// </summary>
    class OffsetPreview
    {
        GameObject _instance;
        Vector3 _prefabScale;
        Animator _poseAnimator;
        AnimationClip _poseClip;
        float _poseTime;

        public bool IsActive => _instance != null;

        public Transform Transform => _instance != null ? _instance.transform : null;

        public bool IsScaleChanged =>
            _instance != null && (_instance.transform.localScale - _prefabScale).sqrMagnitude > 1e-8f;

        public void Start(ItemScript prefab, Transform point, Vector3 position, Vector3 rotation)
        {
            Stop();

            _instance = Object.Instantiate(prefab.gameObject, point, false);
            _instance.name = $"[Preview] {prefab.name}";

            // Every object in the preview, not just the root, so nothing leaks into the saved scene.
            foreach (var child in _instance.GetComponentsInChildren<Transform>(includeInactive: true))
                child.gameObject.hideFlags = HideFlags.DontSave;

            _prefabScale = prefab.transform.localScale;

            Set(position, rotation);

            Selection.activeGameObject = _instance;
            UnityEditor.Tools.current = Tool.Move;
            SceneView.lastActiveSceneView?.FrameSelected();
        }

        public void Stop()
        {
            StopPose();

            if (_instance != null)
            {
                if (Selection.activeGameObject == _instance)
                    Selection.activeGameObject = null;

                Object.DestroyImmediate(_instance);
            }

            _instance = null;
        }

        public void Set(Vector3 position, Vector3 rotation)
        {
            if (_instance == null)
                return;

            _instance.transform.localPosition = position;
            _instance.transform.localEulerAngles = rotation;
        }

        public void ResetScale()
        {
            if (_instance != null)
                _instance.transform.localScale = _prefabScale;
        }

        /// <summary>
        /// Samples an animation clip onto the character so the item can be posed against the
        /// pose it will actually be held in. The character's pose is restored by <see cref="StopPose"/>.
        /// </summary>
        public void SetPose(Animator animator, AnimationClip clip, float normalizedTime)
        {
            if (animator == null || clip == null)
            {
                StopPose();
                return;
            }

            _poseAnimator = animator;
            _poseClip = clip;
            _poseTime = Mathf.Clamp01(normalizedTime) * clip.length;

            if (!AnimationMode.InAnimationMode())
                AnimationMode.StartAnimationMode();

            SamplePose();
        }

        public void SamplePose()
        {
            if (_poseAnimator == null || _poseClip == null || !AnimationMode.InAnimationMode())
                return;

            AnimationMode.BeginSampling();
            AnimationMode.SampleAnimationClip(_poseAnimator.gameObject, _poseClip, _poseTime);
            AnimationMode.EndSampling();
        }

        public void StopPose()
        {
            if (_poseClip != null && AnimationMode.InAnimationMode())
                AnimationMode.StopAnimationMode();

            _poseAnimator = null;
            _poseClip = null;
        }
    }
}
