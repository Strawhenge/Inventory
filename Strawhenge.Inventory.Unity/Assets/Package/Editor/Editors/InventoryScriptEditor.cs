using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Editors
{
    [CustomEditor(typeof(InventoryScript))]
    public class InventoryScriptEditor : UnityEditor.Editor
    {
        InventoryScript _target;
        ItemManagerEditorHelper _itemManager;
        ApparelManagerEditorHelper _apparelManager;

        void OnEnable()
        {
            _target ??= target as InventoryScript;

            _itemManager ??= new ItemManagerEditorHelper(_target);
            _apparelManager ??= new ApparelManagerEditorHelper(_target);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            if (GUILayout.Button(nameof(Inventory.Interrupt)))
                _target.Inventory.Interrupt();
            EditorGUI.EndDisabledGroup();
            
            _itemManager.Inspect();
            _apparelManager.Inspect();
        }
    }
}