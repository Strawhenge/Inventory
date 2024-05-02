using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Data;
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

        public Maybe<IApparelPieceData> FindByName(string name)
        {
            if (_scriptableObjects.TryGetValue(name, out var apparel))
                return Maybe.Some<IApparelPieceData>(apparel);

            return Maybe.None<IApparelPieceData>();
        }
    }
}