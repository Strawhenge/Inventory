using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ResourcesApparelRepository : IApparelPieceRepository
    {
        readonly Dictionary<string, ApparelPieceScriptableObject> _scriptableObjects;

        public ResourcesApparelRepository()
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