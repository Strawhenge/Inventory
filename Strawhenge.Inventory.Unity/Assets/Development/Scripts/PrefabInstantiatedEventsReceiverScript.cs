using UnityEngine;

public class PrefabInstantiatedEventsReceiverScript : MonoBehaviour
{
    public void OnInstantiated(MonoBehaviour script) =>
        Debug.Log($"Instantiated '{script.gameObject.name}'.", script);
}