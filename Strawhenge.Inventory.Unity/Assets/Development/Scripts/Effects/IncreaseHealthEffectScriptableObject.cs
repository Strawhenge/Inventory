using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Effects;
using UnityEngine;

[CreateAssetMenu(menuName = "Strawhenge/Development/Increase Health")]
public class IncreaseHealthEffectScriptableObject : EffectScriptableObject
{
    [SerializeField, Min(0)] int _amount;

    public override EffectData Data => EffectData.From(new IncreaseHealthEffectData(_amount));
}