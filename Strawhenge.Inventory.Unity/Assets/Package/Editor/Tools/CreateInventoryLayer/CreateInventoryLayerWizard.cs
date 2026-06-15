using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools
{
    public class CreateInventoryLayerWizard : ScriptableWizard
    {
        const string Name = "Inventory Animator Layer";

        [MenuItem("Strawhenge/Inventory/" + Name)]
        public static void ShowEditorWindow()
        {
            DisplayWizard<CreateInventoryLayerWizard>(Name, "Create");
        }

        [SerializeField] AnimatorController _animatorController;
        [SerializeField] string _layerName;
        [SerializeField] AvatarMask _avatarMask;

        void OnEnable()
        {
            _animatorController = LastUsed.AnimatorController;
        }
        
        void OnWizardUpdate()
        {
            isValid =
                _animatorController != null &&
                !string.IsNullOrWhiteSpace(_layerName);

            if (_animatorController != null)
                LastUsed.AnimatorController = _animatorController;
        }

        void OnWizardCreate()
        {
            CreateInventoryLayer.Create(_animatorController, _layerName, _avatarMask);
        }
    }
}