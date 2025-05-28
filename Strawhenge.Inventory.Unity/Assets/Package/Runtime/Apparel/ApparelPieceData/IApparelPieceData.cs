using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel.ApparelPieceData
{
    public interface IApparelPieceData
    {
        ApparelPieceScript Prefab { get; }

        Vector3 Position { get; }

        Quaternion Rotation { get; }

        Vector3 Scale { get; }
    }
}