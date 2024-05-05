using Strawhenge.Inventory.Apparel.Effects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Apparel/ApparelPiece")]
    public class ApparelPieceScriptableObject : ScriptableObject, IApparelPieceData
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] ApparelSlotScriptableObject _slot;
        [SerializeField] Vector3 _position;
        [SerializeField] Vector3 _rotation;
        [SerializeField] Vector3 _scale = new Vector3(1, 1, 1);
        [SerializeField] EffectScriptableObject[] _effects;

        public string Name => name;

        public GameObject Prefab => _prefab;

        public string Slot => _slot.name;

        public Vector3 Position => _position;

        public Quaternion Rotation => Quaternion.Euler(_rotation);

        public Vector3 Scale => _scale;

        public IReadOnlyList<EffectData> Effects => _effects
            .Select(x => x.Data)
            .ToArray();
    }
}