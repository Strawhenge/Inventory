using Strawhenge.Inventory.Containers;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class HandsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemInHandMenuEntryScript _leftHand;
        [SerializeField] ItemInHandMenuEntryScript _rightHand;

        internal void Set(IItemContainer leftHand, IItemContainer rightHand)
        {
            _leftHand.Set(leftHand);
            _rightHand.Set(rightHand);
        }
    }
}