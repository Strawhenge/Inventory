using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ResourcesApparelRepository : IApparelRepository
    {
        readonly Dictionary<string, ApparelPieceScriptableObject> _scriptableObjects;

        public ResourcesApparelRepository(ISettings settings)
        {
            _scriptableObjects = Resources
                .LoadAll<ApparelPieceScriptableObject>(path: settings.ApparelScriptableObjectsPath)
                .ToDictionary(apparel => apparel.name.ToLower(), apparel => apparel);
        }

        public Maybe<ApparelPieceData> FindByName(string name)
        {
            return _scriptableObjects
                .MaybeGetValue(name.ToLower())
                .Map(x => x.ToApparelPieceData());
        }
    }
}