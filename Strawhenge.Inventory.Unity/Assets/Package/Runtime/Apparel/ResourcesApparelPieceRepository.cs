using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ResourcesApparelPieceRepository : IApparelPieceRepository
    {
        static ResourcesApparelPieceRepository _instance;

        public static ResourcesApparelPieceRepository GetOrCreateInstance()
        {
            if (_instance == null)
            {
                var apparelPieces = Resources
                    .LoadAll<ApparelPieceScriptableObject>(string.Empty);

                var apparelPiecesByName = new Dictionary<string, ApparelPieceScriptableObject>();
                foreach (var apparelPiece in apparelPieces)
                {
                    if (apparelPiecesByName.ContainsKey(apparelPiece.name))
                        Debug.LogWarning($"Duplicate apparel pieces named '{apparelPiece.name}'.", apparelPiece);

                    apparelPiecesByName[apparelPiece.name] = apparelPiece;
                }

                _instance = new ResourcesApparelPieceRepository(apparelPiecesByName);
            }

            return _instance;
        }

        readonly Dictionary<string, ApparelPieceScriptableObject> _apparelPiecesByName;

        ResourcesApparelPieceRepository(Dictionary<string, ApparelPieceScriptableObject> apparelPiecesByName)
        {
            _apparelPiecesByName = apparelPiecesByName;
        }

        public Maybe<ApparelPiece> FindByName(string name)
        {
            return _apparelPiecesByName
                .MaybeGetValue(name)
                .Map(x => x.ToApparelPiece());
        }
    }
}