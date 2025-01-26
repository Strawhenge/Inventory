using System;
using System.Linq.Expressions;
using System.Reflection;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;

namespace Strawhenge.Inventory.Tests._new
{
    public class ViewCallInfo
    {
        public static implicit operator ViewCallInfo(
            (string itemName, Expression<Func<IItemView, Action<Action>>> methodInvocation) tuple) =>
            new ViewCallInfo(tuple.itemName, tuple.methodInvocation.GetMethodInfo());

        public static implicit operator ViewCallInfo(
            (string itemName, string holsterName, Expression<Func<IHolsterForItemView, Action<Action>>> methodInvocation
                ) tuple) =>
            new ViewCallInfo(tuple.itemName, tuple.holsterName, tuple.methodInvocation.GetMethodInfo());

        public ViewCallInfo(string itemName, MethodInfo method)
        {
            ItemName = itemName;
            Method = method;
        }

        public ViewCallInfo(string itemName, string holsterName, MethodInfo method)
        {
            ItemName = itemName;
            HolsterName = holsterName;
            Method = method;
        }

        public string ItemName { get; }

        public Maybe<string> HolsterName { get; } = Maybe.None<string>();

        public MethodInfo Method { get; }

        public override string ToString()
        {
            return HolsterName
                .Map(holsterName => $"{ItemName} => {holsterName} => {Method.Name}")
                .Reduce(() => $"{ItemName} => {Method.Name}");
        }
    }
}