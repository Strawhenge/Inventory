using Strawhenge.Inventory.Unity.Monobehaviours;
using UnityEditor;

namespace Strawhenge.Inventory.Unity.Editor
{
    [CustomEditor(typeof(InventoryScript))]
    public class InventoryScriptEditor : UnityEditor.Editor
    {
        new InventoryScript target;
        ItemManagerEditorHelper itemManager;
        ApparelManagerEditorHelper apparelManager;

        void OnEnable()
        {
            target ??= base.target as InventoryScript;

            itemManager ??= new ItemManagerEditorHelper(
                () => target.ItemManager);

            apparelManager ??= new ApparelManagerEditorHelper(
                () => target.ApparelManager);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            itemManager.Inspect();
            apparelManager.Inspect();
        }
    }
}
