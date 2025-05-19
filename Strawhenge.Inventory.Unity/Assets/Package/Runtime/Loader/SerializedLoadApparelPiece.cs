using Strawhenge.Inventory.Loader;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [Serializable]
    class SerializedLoadApparelPiece
    {
        [SerializeField] ApparelPieceScriptableObject _apparel;

        public LoadApparelPiece Map() => new(_apparel.ToApparelPiece());
    }
}