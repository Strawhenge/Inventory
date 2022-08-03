﻿using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Holster")]
    public class HolsterScriptableObject : ScriptableObject
    {
        public string Name => name;
    }
}