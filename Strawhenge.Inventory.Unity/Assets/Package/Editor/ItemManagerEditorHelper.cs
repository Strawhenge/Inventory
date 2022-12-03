using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Linq;
using FunctionalUtilities;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class ItemManagerEditorHelper
    {
        readonly EditorTarget<IItemManager> target;

        IItem item;
        bool displayLeftHand;
        bool displayRightHand;
        bool displayHolsters;
        bool displayInventory;
        IItem displayItem;

        public ItemManagerEditorHelper(Func<IItemManager> getTarget)
        {
            target = new EditorTarget<IItemManager>(getTarget);
        }

        public void Inspect()
        {
            EditorGUILayout.LabelField("Item Manager", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(!target.HasInstance);

            if (item != null)
            {
                if (GUILayout.Button("Clear"))
                    item = null;
                else
                    InspectItem(item);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                var script = (ItemScript)EditorGUILayout.ObjectField(null, typeof(ItemScript), allowSceneObjects: true);
                var scriptableObject =
                    (ItemScriptableObject)EditorGUILayout.ObjectField(null, typeof(ItemScriptableObject),
                        allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                if (script != null)
                    item = target.Instance.Manage(script);
                else if (scriptableObject != null)
                    item = target.Instance.Manage(scriptableObject);
            }

            InspectHands();
            InspectHolsters();
            InspectInventory();

            EditorGUI.EndDisabledGroup();
        }

        void InspectHands()
        {
            displayLeftHand = EditorGUILayout.Foldout(displayLeftHand, $"In Left Hand ({GetLeftHandItemString()})",
                toggleOnLabelClick: true);
            if (displayLeftHand && target.HasInstance)
            {
                target.Instance
                    .ItemInLeftHand.Do(InspectItem);
            }

            displayRightHand = EditorGUILayout.Foldout(displayRightHand, $"In Right Hand ({GetRightHandItemString()})",
                toggleOnLabelClick: true);
            if (displayRightHand && target.HasInstance)
            {
                target.Instance
                    .ItemInRightHand.Do(InspectItem);
            }
        }

        void InspectHolsters()
        {
            displayHolsters = EditorGUILayout.Foldout(displayHolsters, $"In Holsters ({GetHolsterItemCount()})",
                toggleOnLabelClick: true);
            if (displayHolsters)
            {
                foreach (var item in target.Instance.ItemsInHolsters)
                {
                    InspectItemWithToggle(item);
                }
            }
        }

        void InspectInventory()
        {
            displayInventory = EditorGUILayout.Foldout(displayInventory, $"In Inventory ({GetInventoryCountString()})",
                toggleOnLabelClick: true);

            if (displayInventory)
            {
                foreach (var item in target.Instance.ItemsInInventory)
                    InspectItemWithToggle(item);
            }
        }

        void InspectItemWithToggle(IItem item)
        {
            bool show = EditorGUILayout.Foldout(item == displayItem, item.Name, toggleOnLabelClick: true);

            if (show)
            {
                displayItem = item;
            }
            else if (item == displayItem)
            {
                displayItem = null;
            }

            if (show)
                InspectItem(item);
        }

        void InspectItem(IItem item)
        {
            EditorGUILayout.HelpBox(GetItemInfoString(item), MessageType.Info);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(IItemManager.AddItemToInventory)))
                target.Instance.AddItemToInventory(item);

            if (GUILayout.Button(nameof(IItemManager.RemoveItemFromInventory)))
                target.Instance.RemoveItemFromInventory(item);

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(IItem.HoldLeftHand)))
                item.HoldLeftHand();
            if (GUILayout.Button(nameof(IItem.HoldRightHand)))
                item.HoldRightHand();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(IItem.Drop)))
                item.Drop();
            if (GUILayout.Button(nameof(IItem.PutAway)))
                item.PutAway();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(IItem.ClearFromHands)))
                item.ClearFromHands();
            if (GUILayout.Button(nameof(IItem.UnequipFromHolster)))
                item.UnequipFromHolster();

            EditorGUILayout.EndHorizontal();

            foreach (var holster in item.Holsters)
            {
                EditorGUILayout.LabelField(holster.HolsterName);
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(nameof(IHolsterForItem.Equip)))
                    holster.Equip();
                if (GUILayout.Button(nameof(IHolsterForItem.Unequip)))
                    holster.Unequip();

                EditorGUILayout.EndHorizontal();
            }
        }

        string GetItemInfoString(IItem item)
        {
            var lines = new string[]
            {
                item.Name,
                item.IsTwoHanded ? "Two Handed" : "One Handed",
                $"Holster: {(item.Holsters.FirstOrDefault(x => x.IsEquipped) is IHolsterForItem holster ? holster.HolsterName : "None")}"
            };

            return string.Join(Environment.NewLine, lines);
        }

        string GetHolsterItemCount() => target.HasInstance
            ? target.Instance.ItemsInHolsters.Count().ToString()
            : "NA";

        string GetInventoryCountString() => target.HasInstance
            ? target.Instance.ItemsInInventory.Count().ToString()
            : "NA";

        string GetLeftHandItemString() => target.HasInstance
            ? GetItemInHandString(target.Instance.ItemInLeftHand)
            : "NA";

        string GetRightHandItemString() => target.HasInstance
            ? GetItemInHandString(target.Instance.ItemInRightHand)
            : "NA";

        string GetItemInHandString(Maybe<IItem> item) => item
            .Map(x => x.Name)
            .Reduce(() => "none");
    }
}