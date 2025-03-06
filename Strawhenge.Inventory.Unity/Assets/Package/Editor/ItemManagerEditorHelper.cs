using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Items.Storables;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class ItemManagerEditorHelper
    {
        readonly EditorTarget<IInventory> _target;

        Item _item;
        bool _displayLeftHand;
        bool _displayRightHand;
        bool _displayHolsters;
        bool _displayInventory;
        Item _displayItem;

        public ItemManagerEditorHelper(Func<IInventory> getTarget)
        {
            _target = new EditorTarget<IInventory>(getTarget);
        }

        public void Inspect()
        {
            EditorGUILayout.LabelField("Item Manager", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(!_target.HasInstance);

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

                var scriptableObject = (ItemScriptableObject)EditorGUILayout.ObjectField(
                    null,
                    typeof(ItemScriptableObject),
                    allowSceneObjects: true);

                EditorGUILayout.EndHorizontal();

                if (pickup != null)
                    _item = _target.Instance.CreateItem(pickup);
                else if (scriptableObject != null)
                    _item = _target.Instance.CreateItem(scriptableObject);
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

            if (_displayLeftHand && _target.HasInstance)
            {
                _target.Instance
                    .LeftHand
                    .CurrentItem
                    .Do(InspectItem);
            }

            _displayRightHand = EditorGUILayout.Foldout(
                _displayRightHand,
                $"In Right Hand ({GetRightHandItemString()})",
                toggleOnLabelClick: true);

            if (_displayRightHand && _target.HasInstance)
            {
                _target.Instance
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
                foreach (var holster in _target.Instance.Holsters)
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
                foreach (var item in _target.Instance.StoredItems.Items)
                    InspectItemWithToggle(item);
            }
        }

        void InspectItemWithToggle(Item item)
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

        void InspectItem(Item item)
        {
            EditorGUILayout.HelpBox(GetItemInfoString(item), MessageType.Info);

            item.Storable.Do(storable =>
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(nameof(IStorable.AddToStorage)))
                    storable.AddToStorage();

                if (GUILayout.Button(nameof(IStorable.RemoveFromStorage)))
                    storable.RemoveFromStorage();

                EditorGUILayout.EndHorizontal();
            });

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(Item.HoldLeftHand)))
                item.HoldLeftHand();
            if (GUILayout.Button(nameof(Item.HoldRightHand)))
                item.HoldRightHand();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(Item.Drop)))
                item.Drop();
            if (GUILayout.Button(nameof(Item.PutAway)))
                item.PutAway();
            if (GUILayout.Button(nameof(Item.ClearFromHands)))
                item.ClearFromHands();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(Item.UnequipFromHolster)))
                item.UnequipFromHolster();
            if (GUILayout.Button(nameof(Item.Discard)))
                item.Discard();

            EditorGUILayout.EndHorizontal();

            item.Consumable.Do(consumable =>
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(nameof(IConsumable.ConsumeLeftHand)))
                    consumable.ConsumeLeftHand();
                if (GUILayout.Button(nameof(IConsumable.ConsumeRightHand)))
                    consumable.ConsumeRightHand();
                EditorGUILayout.EndHorizontal();
            });

            foreach (var holster in item.Holsters)
            {
                EditorGUILayout.LabelField(holster.HolsterName);
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(nameof(HolsterForItem.Equip)))
                    holster.Equip();
                if (GUILayout.Button(nameof(HolsterForItem.Unequip)))
                    holster.Unequip();

                EditorGUILayout.EndHorizontal();
            }
        }

        string GetItemInfoString(Item item)
        {
            var lines = new[]
            {
                item.Name,
                item.IsTwoHanded ? "Two Handed" : "One Handed",
                $"Holster: {(item.Holsters.FirstOrDefault(x => x.IsEquipped) is HolsterForItem holster ? holster.HolsterName : "None")}"
            };

            return string.Join(Environment.NewLine, lines);
        }

        string GetHolsterItemCount() => _target.HasInstance
            ? _target.Instance.Holsters.Count(x => x.CurrentItem.HasSome(out _)).ToString()
            : "NA";

        string GetInventoryCountString() => _target.HasInstance
            ? _target.Instance.StoredItems.Items.Count().ToString()
            : "NA";

        string GetLeftHandItemString() => _target.HasInstance
            ? GetItemInHandString(_target.Instance.LeftHand.CurrentItem)
            : "NA";

        string GetRightHandItemString() => _target.HasInstance
            ? GetItemInHandString(_target.Instance.RightHand.CurrentItem)
            : "NA";

        string GetItemInHandString(Maybe<Item> item) => item
            .Map(x => x.Name)
            .Reduce(() => "none");
    }
}