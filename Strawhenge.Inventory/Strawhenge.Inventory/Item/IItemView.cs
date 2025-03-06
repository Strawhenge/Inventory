using System;

namespace Strawhenge.Inventory.Items
{
    public interface IItemView
    {
        event Action Released;

        void AppearLeftHand(Action callback = null);

        void AppearRightHand(Action callback = null);

        void DrawLeftHand(Action callback = null);

        void DrawRightHand(Action callback = null);

        void PutAwayLeftHand(Action callback = null);

        void PutAwayRightHand(Action callback = null);

        void DropLeftHand(Action callback = null);

        void DropRightHand(Action callback = null);

        void SpawnAndDrop(Action callback = null);

        void LeftHandToRightHand(Action callback = null);

        void RightHandToLeftHand(Action callback = null);

        void DisappearLeftHand(Action callback = null);

        void DisappearRightHand(Action callback = null);
    }
}