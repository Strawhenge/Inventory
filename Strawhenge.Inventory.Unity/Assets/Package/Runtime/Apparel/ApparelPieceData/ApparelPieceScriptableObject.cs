using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Effects;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel.ApparelPieceData
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Apparel/Apparel Piece")]
    public class ApparelPieceScriptableObject : ScriptableObject, IApparelPieceData
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] ApparelSlotScriptableObject _slot;
        [SerializeField] Vector3 _position;
        [SerializeField] Vector3 _rotation;
        [SerializeField] Vector3 _scale = new Vector3(1, 1, 1);
        [SerializeField] EffectScriptableObject[] _effects;

        ApparelPiece _apparelPiece;

        GameObject IApparelPieceData.Prefab => _prefab;

        Vector3 IApparelPieceData.Position => _position;

        Quaternion IApparelPieceData.Rotation => Quaternion.Euler(_rotation);

        Vector3 IApparelPieceData.Scale => _scale;

        public ApparelPiece ToApparelPiece()
        {
            if (_apparelPiece == null)
                _apparelPiece = BuildApparelPiece();

            return _apparelPiece;
        }

        ApparelPiece BuildApparelPiece()
        {
            var effects = _effects
                .Select(x => x.Data)
                .ToArray();

            string slotName;
            if (_slot != null)
            {
                slotName = _slot.name;
            }
            else
            {
                Debug.LogError($"{nameof(_slot)} missing.", this);
                slotName = "Missing.";
            }

            return ApparelPieceBuilder
                .Create(name, slotName, effects, x => x.Set<IApparelPieceData>(this))
                .Build();
        }
    }
}