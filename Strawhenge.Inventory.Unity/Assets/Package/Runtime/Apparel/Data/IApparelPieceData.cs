using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public interface IApparelPieceData
    {
        string Name { get; }

        GameObject Prefab { get; }

        string Slot { get; }

        Vector3 Position { get; }

        Quaternion Rotation { get; }

        Vector3 Scale { get; }

        IReadOnlyList<IEffectData> Effects { get; }
    }
}