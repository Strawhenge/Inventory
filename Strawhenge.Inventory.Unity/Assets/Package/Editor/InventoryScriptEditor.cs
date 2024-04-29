using Strawhenge.Inventory.Unity.Monobehaviours;
using UnityEditor;

namespace Strawhenge.Inventory.Unity.Editor
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

            _itemManager ??= new ItemManagerEditorHelper(
                () => _target.Inventory);

            _apparelManager ??= new ApparelManagerEditorHelper(
                () => _target.Inventory);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _itemManager.Inspect();
            _apparelManager.Inspect();
        }
    }
}