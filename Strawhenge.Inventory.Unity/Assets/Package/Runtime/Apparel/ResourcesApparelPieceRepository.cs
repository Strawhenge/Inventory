using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ResourcesApparelPieceRepository : IApparelPieceRepository
    {
        readonly Dictionary<string, ApparelPieceScriptableObject> _scriptableObjects;

        public ResourcesApparelPieceRepository()
        {
            _scriptableObjects = Resources
                .LoadAll<ApparelPieceScriptableObject>(string.Empty)
                .ToDictionary(apparel => apparel.name.ToLower(), apparel => apparel);
        }

        public Maybe<ApparelPiece> FindByName(string name)
        {
            return _scriptableObjects
                .MaybeGetValue(name.ToLower())
                .Map(x => x.ToApparelPiece());
        }
    }
}