using Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard;
using Strawhenge.Inventory.Unity.Items.ItemData;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Editors
{
    [CustomEditor(typeof(ItemScriptableObject))]
    [CanEditMultipleObjects]
    public class ItemScriptableObjectEditor : UnityEditor.Editor
    {
        static readonly GUIContent EditVisuallyContent = new(
            "Edit Visually...",
            "Open the Item Wizard to pose this item's hand and holster offsets on a character.");

        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(targets.Length != 1))
            {
                if (GUILayout.Button(EditVisuallyContent, GUILayout.Height(24)))
                {
                    ItemWizardWindow.OpenPose((ItemScriptableObject)target);

                    // Opening the wizard may show a dialog, which breaks this Inspector's layout pass.
                    GUIUtility.ExitGUI();
                }
            }

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}
