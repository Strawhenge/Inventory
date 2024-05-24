using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    [RequireComponent(typeof(InventoryScript))]
    public class LoadInventoryScript : MonoBehaviour
    {
        [SerializeField] SerializedLoadInventoryItem[] _items;

        InventoryScript _inventory;

        public NewLoader.InventoryLoader Loader { get; set; }

        void Awake()
        {
            _inventory = GetComponent<InventoryScript>();
        }

        void Start()
        {
            StartCoroutine(Load());

            IEnumerator Load()
            {
                yield return new WaitUntil(() => _inventory.IsConfigurationComplete);

                Loader.Load(new LoadInventoryData(_items, Array.Empty<ILoadApparelPiece>()));
            }
        }
    }
}