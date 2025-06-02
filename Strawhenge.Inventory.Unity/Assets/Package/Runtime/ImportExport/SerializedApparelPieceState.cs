using Strawhenge.Inventory.ImportExport;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.ImportExport
{
    [Serializable]
    class SerializedApparelPieceState
    {
        [SerializeField] ApparelPieceScriptableObject _apparel;

        public ApparelPieceState Map() => new(_apparel.ToApparelPiece());
    }
}