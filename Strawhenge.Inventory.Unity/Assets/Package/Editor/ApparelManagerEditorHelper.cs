using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class ApparelManagerEditorHelper
    {
        readonly EditorTarget<Inventory> _target;
        InventoryApparelPiece _piece;
        bool _displaySlots;

        public ApparelManagerEditorHelper(Func<Inventory> getTarget)
        {
            _target = new EditorTarget<Inventory>(getTarget);
        }

        public void Inspect()
        {
            EditorGUILayout.LabelField("Apparel Manager", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(!_target.HasInstance);

            if (_piece != null)
            {
                if (GUILayout.Button("Clear"))
                    _piece = null;
                else
                    InspectPiece(_piece);
            }
            else
            {
                var scriptableObject = (ApparelPieceScriptableObject)EditorGUILayout.ObjectField(null,
                    typeof(ApparelPieceScriptableObject), allowSceneObjects: true);
                if (scriptableObject != null)
                    _piece = _target.Instance.CreateApparelPiece(scriptableObject);
            }

            InspectSlots();

            EditorGUI.EndDisabledGroup();
        }

        void InspectSlots()
        {
            _displaySlots = EditorGUILayout.Foldout(_displaySlots, $"Slots", toggleOnLabelClick: true);

            if (!_displaySlots)
                return;

            foreach (var slot in _target.Instance.ApparelSlots)
            {
                EditorGUILayout.LabelField($"{slot.Name}:");

                slot.CurrentPiece
                    .Do(InspectPiece);
            }
        }

        void InspectPiece(InventoryApparelPiece piece)
        {
            var info = $"{piece.Name}{Environment.NewLine}Slot: {piece.SlotName}";

            EditorGUILayout.HelpBox(info, MessageType.Info);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(InventoryApparelPiece.Equip)))
                piece.Equip();

            if (GUILayout.Button(nameof(InventoryApparelPiece.Unequip)))
                piece.Unequip();

            EditorGUILayout.EndHorizontal();
        }
    }
}