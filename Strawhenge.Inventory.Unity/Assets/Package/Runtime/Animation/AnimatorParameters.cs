using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Animation
{
    static class AnimatorParameters
    {
        public static AnimatorParameter DrawItem { get; } = new("Draw Item");

        public static AnimatorParameter DrawItemId { get; } = new("Draw Item ID");

        public static AnimatorParameter PutAwayItem { get; } = new("Put Away Item");

        public static AnimatorParameter PutAwayItemId { get; } = new("Put Away Item ID");

        public static AnimatorParameter ConsumeItem { get; } = new("Consume Item");

        public static AnimatorParameter ConsumeItemId { get; } = new("Consume Item ID");

        public static AnimatorParameter HoldItemLeftId { get; } = new("Hold Item Left ID");
        
        public static AnimatorParameter HoldItemRightId { get; } = new("Hold Item Right ID");
    }
}