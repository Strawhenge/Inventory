using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelRepository : IApparelRepository
    {
        readonly Dictionary<string, ApparelPieceScriptableObject> _scriptableObjects;

        public ApparelRepository(ISettings settings)
        {
            _scriptableObjects = Resources
                .LoadAll<ApparelPieceScriptableObject>(path: settings.ApparelScriptableObjectsPath)
                .ToDictionary(apparel => apparel.name, apparel => apparel);
        }

        public Maybe<ApparelPieceData> FindByName(string name)
        {
            return _scriptableObjects
                .MaybeGetValue(name)
                .Map(x => x.ToApparelPieceData());
        }
    }
}