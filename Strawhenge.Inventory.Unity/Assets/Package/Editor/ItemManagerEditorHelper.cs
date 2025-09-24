using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class ItemManagerEditorHelper
    {
        readonly InventoryScript _inventory;
        
        InventoryItem _item;
        string _locateItemText;
        bool _displayLeftHand;
        bool _displayRightHand;
        bool _displayHolsters;
        bool _displayInventory;
        InventoryItem _displayItem;

        public ItemManagerEditorHelper(InventoryScript inventory)
        {
            _inventory = inventory;
        }

        public void Inspect()
        {
            EditorGUILayout.LabelField("Item Manager", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            if (_item != null)
            {
                if (GUILayout.Button("Clear"))
                    _item = null;
                else
                    InspectItem(_item);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();

                var pickup = (ItemPickupScript)EditorGUILayout.ObjectField(
                    null,
                    typeof(ItemPickupScript),
                    allowSceneObjects: true);

                if (pickup != null)
                    _item = _inventory.Inventory.CreateItem(pickup);

                var scriptableObject = (ItemScriptableObject)EditorGUILayout.ObjectField(
                    null,
                    typeof(ItemScriptableObject),
                    allowSceneObjects: true);

                if (scriptableObject != null)
                    _item = _inventory.Inventory.CreateItem(scriptableObject);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                _locateItemText = EditorGUILayout.TextField(string.Empty, _locateItemText ?? string.Empty);
                if (GUILayout.Button(nameof(Inventory.GetItemOrCreateTemporary)))
                {
                    // _inventory.Inventory
                    //     .GetItemOrCreateTemporary(_locateItemText)
                    //     .Do(item => _item = item);
                }

                EditorGUILayout.EndHorizontal();
            }

            InspectHands();
            InspectHolsters();
            InspectInventory();

            EditorGUI.EndDisabledGroup();
        }

        void InspectHands()
        {
            _displayLeftHand = EditorGUILayout.Foldout(
                _displayLeftHand,
                $"In Left Hand ({GetLeftHandItemString()})",
                toggleOnLabelClick: true);

            if (_displayLeftHand)
            {
                _inventory.Inventory
                    .Hands
                    .LeftHand
                    .CurrentItem
                    .Do(InspectItem);
            }

            _displayRightHand = EditorGUILayout.Foldout(
                _displayRightHand,
                $"In Right Hand ({GetRightHandItemString()})",
                toggleOnLabelClick: true);

            if (_displayRightHand)
            {
                _inventory.Inventory
                    .Hands
                    .RightHand
                    .CurrentItem
                    .Do(InspectItem);
            }
        }

        void InspectHolsters()
        {
            _displayHolsters = EditorGUILayout.Foldout(
                _displayHolsters,
                $"In Holsters ({GetHolsterItemCount()})",
                toggleOnLabelClick: true);

            if (_displayHolsters)
            {
                foreach (var holster in _inventory.Inventory.Holsters)
                {
                    holster.CurrentItem.Do(InspectItemWithToggle);
                }
            }
        }

        void InspectInventory()
        {
            _displayInventory = EditorGUILayout.Foldout(_displayInventory,
                $"In Inventory ({GetInventoryCountString()})",
                toggleOnLabelClick: true);

            if (_displayInventory)
            {
                foreach (var item in _inventory.Inventory.StoredItems.Items)
                    InspectItemWithToggle(item);
            }
        }

        void InspectItemWithToggle(InventoryItem item)
        {
            bool show = EditorGUILayout.Foldout(item == _displayItem, item.Name, toggleOnLabelClick: true);

            if (show)
            {
                _displayItem = item;
            }
            else if (item == _displayItem)
            {
                _displayItem = null;
            }

            if (show)
                InspectItem(item);
        }

        void InspectItem(InventoryItem item)
        {
            EditorGUILayout.HelpBox(GetItemInfoString(item), MessageType.Info);

            item.Storable.Do(storable =>
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(nameof(Storable.AddToStorage)))
                    storable.AddToStorage();

                if (GUILayout.Button(nameof(Storable.RemoveFromStorage)))
                    storable.RemoveFromStorage();

                EditorGUILayout.EndHorizontal();
            });

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(InventoryItem.HoldLeftHand)))
                item.HoldLeftHand();
            if (GUILayout.Button(nameof(InventoryItem.HoldRightHand)))
                item.HoldRightHand();
            if (GUILayout.Button(nameof(InventoryItem.SwapHands)))
                item.SwapHands();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(InventoryItem.Drop)))
                item.Drop();
            if (GUILayout.Button(nameof(InventoryItem.PutAway)))
                item.PutAway();
            if (GUILayout.Button(nameof(InventoryItem.ClearFromHands)))
                item.ClearFromHands();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(InventoryItem.UnequipFromHolster)))
                item.UnequipFromHolster();
            if (GUILayout.Button(nameof(InventoryItem.Discard)))
                item.Discard();

            EditorGUILayout.EndHorizontal();

            item.Consumable.Do(consumable =>
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(nameof(Consumable.ConsumeLeftHand)))
                    consumable.ConsumeLeftHand();
                if (GUILayout.Button(nameof(Consumable.ConsumeRightHand)))
                    consumable.ConsumeRightHand();
                EditorGUILayout.EndHorizontal();
            });

            foreach (var holster in item.Holsters)
            {
                EditorGUILayout.LabelField(holster.HolsterName);
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(nameof(InventoryItemHolster.Equip)))
                    holster.Equip();
                if (GUILayout.Button(nameof(InventoryItemHolster.Unequip)))
                    holster.Unequip();

                EditorGUILayout.EndHorizontal();
            }
        }

        string GetItemInfoString(InventoryItem item)
        {
            var lines = new[]
            {
                item.Name,
                item.IsTwoHanded ? "Two Handed" : "One Handed",
                $"Holster: {(item.Holsters.FirstOrDefault(x => x.IsEquipped) is InventoryItemHolster holster ? holster.HolsterName : "None")}"
            };

            return string.Join(Environment.NewLine, lines);
        }

        string GetHolsterItemCount() => Application.isPlaying
            ? _inventory.Inventory.Holsters.Count(x => x.CurrentItem.HasSome(out _)).ToString()
            : "NA";

        string GetInventoryCountString() => Application.isPlaying
            ? _inventory.Inventory.StoredItems.Items.Count().ToString()
            : "NA";

        string GetLeftHandItemString() => Application.isPlaying
            ? GetItemInHandString(_inventory.Inventory.Hands.LeftHand.CurrentItem)
            : "NA";

        string GetRightHandItemString() => Application.isPlaying
            ? GetItemInHandString(_inventory.Inventory.Hands.RightHand.CurrentItem)
            : "NA";

        string GetItemInHandString(Maybe<InventoryItem> item) => item
            .Map(x => x.Name)
            .Reduce(() => "none");
    }
}