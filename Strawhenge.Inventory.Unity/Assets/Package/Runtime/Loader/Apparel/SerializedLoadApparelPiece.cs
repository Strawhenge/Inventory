using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [Serializable]
    class SerializedLoadApparelPiece : ILoadApparelPiece
    {
        [SerializeField] ApparelPieceScriptableObject _apparel;

        public ApparelPieceData ApparelPiece => _apparel.ToApparelPieceData();
    }
}