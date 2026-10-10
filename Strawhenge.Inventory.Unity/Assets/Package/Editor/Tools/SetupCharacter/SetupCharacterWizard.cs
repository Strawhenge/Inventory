using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.SetupCharacter
{
    public class SetupCharacterWizard : ScriptableWizard
    {
        const string Name = "Set Up Character...";

        [MenuItem("Strawhenge/Inventory/" + Name, priority = 0)]
        public static void ShowEditorWindow()
        {
            var wizard = DisplayWizard<SetupCharacterWizard>("Set Up Inventory Character", "Set Up");
            wizard._character = Selection.activeGameObject;
            wizard.OnWizardUpdate();
        }

        [MenuItem("GameObject/Strawhenge/Inventory/Set Up Character...", priority = 10)]
        static void ShowEditorWindowFromHierarchy() => ShowEditorWindow();

        [SerializeField, Tooltip("The character root. Must have an Animator on it or one of its children.")]
        GameObject _character;

        [SerializeField, Tooltip("Adds the draw/put away/consume layer and parameters to the Animator Controller.")]
        bool _createInventoryLayer = true;

        [SerializeField] string _layerName = SetupCharacter.DefaultLayerName;

        [SerializeField, Tooltip("Optional. Restricts the inventory layer to part of the body, e.g. the arms.")]
        AvatarMask _avatarMask;

        [SerializeField, Tooltip("Total weight of items that can be stored. Only applied if currently 0.")]
        int _maxStoredItemsWeight = 100;

        void OnWizardUpdate()
        {
            helpString = "Adds an InventoryScript, creates left and right hand item points on the humanoid " +
                         "hand bones, and prepares the Animator Controller. Existing setup is kept.";

            errorString = string.Empty;

            if (_character == null)
                errorString = "Select a character.";
            else if (EditorUtility.IsPersistent(_character))
                errorString = "Select a character in the scene or open its prefab in Prefab Mode.";
            else if (_character.GetComponentInChildren<Animator>(includeInactive: true) == null)
                errorString = "The character needs an Animator on it or one of its children.";

            isValid = string.IsNullOrEmpty(errorString);
        }

        void OnWizardCreate()
        {
            var report = SetupCharacter.Run(
                _character,
                _createInventoryLayer,
                _layerName,
                _avatarMask,
                _maxStoredItemsWeight);

            var message = report.ToString();

            if (report.HasErrors)
                Debug.LogError("Inventory character setup failed.\n\n" + message, _character);
            else
                Debug.Log("Inventory character setup complete.\n\n" + message, _character);

            EditorUtility.DisplayDialog(
                report.HasErrors ? "Inventory setup failed" : "Inventory setup complete",
                message,
                "OK");

            var inventory = _character.GetComponent<InventoryScript>();
            if (inventory != null)
                Selection.activeObject = inventory;
        }
    }
}
