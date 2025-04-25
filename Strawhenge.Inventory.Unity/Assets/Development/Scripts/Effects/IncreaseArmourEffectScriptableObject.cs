using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Effects;
using UnityEngine;

[CreateAssetMenu(menuName = "Strawhenge/Development/Increase Armour")]
public class IncreaseArmourEffectScriptableObject : EffectScriptableObject
{
    [SerializeField, Min(0)] int _amount;

    public int Amount => _amount;

    public override EffectData Data => EffectData.From(this);
}