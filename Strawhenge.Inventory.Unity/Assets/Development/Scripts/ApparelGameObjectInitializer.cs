using Autofac;
using Strawhenge.Inventory.Unity.Apparel;
using UnityEngine;

class ApparelGameObjectInitializer : IApparelGameObjectInitializer
{
    readonly ILifetimeScope _scope;

    public ApparelGameObjectInitializer(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public void Initialize(GameObject apparel)
    {
        foreach (var script in apparel.GetComponentsInChildren<MonoBehaviour>())
            _scope.InjectUnsetProperties(script);
    }
}