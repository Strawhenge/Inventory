﻿using Strawhenge.Inventory.Items.HolsterForItem;
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
        readonly EditorTarget<IItemManager> _target;

        IItem _item;
        bool _displayLeftHand;
        bool _displayRightHand;
        bool _displayHolsters;
        bool _displayInventory;
        IItem _displayItem;

        public ItemManagerEditorHelper(Func<IItemManager> getTarget)
        {
            _target = new EditorTarget<IItemManager>(getTarget);
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
                var script = (ItemScript)EditorGUILayout.ObjectField(null, typeof(ItemScript), allowSceneObjects: true);
                var scriptableObject =
                    (ItemScriptableObject)EditorGUILayout.ObjectField(null, typeof(ItemScriptableObject),
                        allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                if (script != null)
                    _item = _target.Instance.Manage(script);
                else if (scriptableObject != null)
                    _item = _target.Instance.Manage(scriptableObject);
            }

            InspectHands();
            InspectHolsters();
            InspectInventory();

            EditorGUI.EndDisabledGroup();
        }

        void InspectHands()
        {
            _displayLeftHand = EditorGUILayout.Foldout(_displayLeftHand, $"In Left Hand ({GetLeftHandItemString()})",
                toggleOnLabelClick: true);
            if (_displayLeftHand && _target.HasInstance)
            {
                _target.Instance
                    .ItemInLeftHand.Do(InspectItem);
            }

            _displayRightHand = EditorGUILayout.Foldout(_displayRightHand, $"In Right Hand ({GetRightHandItemString()})",
                toggleOnLabelClick: true);
            if (_displayRightHand && _target.HasInstance)
            {
                _target.Instance
                    .ItemInRightHand.Do(InspectItem);
            }
        }

        void InspectHolsters()
        {
            _displayHolsters = EditorGUILayout.Foldout(_displayHolsters, $"In Holsters ({GetHolsterItemCount()})",
                toggleOnLabelClick: true);
            if (_displayHolsters)
            {
                foreach (var item in _target.Instance.ItemsInHolsters)
                {
                    InspectItemWithToggle(item);
                }
            }
        }

        void InspectInventory()
        {
            _displayInventory = EditorGUILayout.Foldout(_displayInventory, $"In Inventory ({GetInventoryCountString()})",
                toggleOnLabelClick: true);

            if (_displayInventory)
            {
                foreach (var item in _target.Instance.ItemsInInventory)
                    InspectItemWithToggle(item);
            }
        }

        void InspectItemWithToggle(IItem item)
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

        void InspectItem(IItem item)
        {
            EditorGUILayout.HelpBox(GetItemInfoString(item), MessageType.Info);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(IItemManager.AddItemToInventory)))
                _target.Instance.AddItemToInventory(item);

            if (GUILayout.Button(nameof(IItemManager.RemoveItemFromInventory)))
                _target.Instance.RemoveItemFromInventory(item);

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
            var lines = new[]
            {
                item.Name,
                item.IsTwoHanded ? "Two Handed" : "One Handed",
                $"Holster: {(item.Holsters.FirstOrDefault(x => x.IsEquipped) is IHolsterForItem holster ? holster.HolsterName : "None")}"
            };

            return string.Join(Environment.NewLine, lines);
        }

        string GetHolsterItemCount() => _target.HasInstance
            ? _target.Instance.ItemsInHolsters.Count().ToString()
            : "NA";

        string GetInventoryCountString() => _target.HasInstance
            ? _target.Instance.ItemsInInventory.Count().ToString()
            : "NA";

        string GetLeftHandItemString() => _target.HasInstance
            ? GetItemInHandString(_target.Instance.ItemInLeftHand)
            : "NA";

        string GetRightHandItemString() => _target.HasInstance
            ? GetItemInHandString(_target.Instance.ItemInRightHand)
            : "NA";

        string GetItemInHandString(Maybe<IItem> item) => item
            .Map(x => x.Name)
            .Reduce(() => "none");
    }
}