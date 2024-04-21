using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Data;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelRepository : IApparelRepository
    {
        readonly IApparelPieceData[] _scriptableObjects;

        public ApparelRepository(ISettings settings)
        {
            _scriptableObjects = Resources
                .LoadAll<ApparelPieceScriptableObject>(path: settings.ApparelScriptableObjectsPath)
                .ToArray<IApparelPieceData>();
        }

        public Maybe<IApparelPieceData> FindByName(string name)
        {
            return _scriptableObjects.FirstOrNone(
                x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}