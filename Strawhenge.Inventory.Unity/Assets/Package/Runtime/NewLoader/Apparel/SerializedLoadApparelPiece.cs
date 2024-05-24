using Strawhenge.Inventory.Unity.Apparel;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    [Serializable]
    class SerializedLoadApparelPiece : ILoadApparelPiece
    {
        [SerializeField] ApparelPieceScriptableObject _apparel;

        public IApparelPieceData ApparelPiece => _apparel;
    }
}