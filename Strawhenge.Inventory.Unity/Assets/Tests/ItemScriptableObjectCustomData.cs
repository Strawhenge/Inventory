using System.Collections.Generic;
using System.Text.RegularExpressions;
using Strawhenge.Inventory.Unity.Items.ItemData;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Inventory.Unity.Tests
{
    public class ItemScriptableObjectCustomData
    {
        readonly List<Object> _created = new();
        ItemScriptableObject _item;

        [SetUp]
        public void SetUp()
        {
            _item = Create<ItemScriptableObject>("Sword");
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var obj in _created)
            {
                if (obj != null)
                    Object.DestroyImmediate(obj);
            }

            _created.Clear();
        }

        [Test]
        public void Should_return_custom_data_by_its_own_type_and_interface()
        {
            var stats = Create<WeaponStats>("Sword Stats");
            SetCustomData(stats);

            var item = _item.ToItem();

            item.Get<WeaponStats>().VerifyIsSome(stats);
            item.Get<IWeaponStats>().VerifyIsSome(stats);
            item.Get<ShieldStats>().VerifyIsNone();
        }

        [Test]
        public void Should_ignore_empty_and_destroyed_entries()
        {
            var destroyed = Create<WeaponStats>("Destroyed Stats");
            var stats = Create<ShieldStats>("Shield Stats");
            SetCustomData(null, destroyed, stats);
            Object.DestroyImmediate(destroyed);

            // A destroyed entry still looks like an object to C#; only Unity's == null catches it.
            _item.ToItem().Get<IWeaponStats>().VerifyIsSome(stats);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void Should_return_first_match_and_warn_once_when_more_than_one_matches()
        {
            var first = Create<WeaponStats>("Sword Stats");
            var second = Create<ShieldStats>("Shield Stats");
            SetCustomData(first, second);

            LogAssert.Expect(LogType.Warning, new Regex("Item 'Sword': 2 data values are a 'IWeaponStats'"));

            var item = _item.ToItem();
            item.Get<IWeaponStats>().VerifyIsSome(first);
            item.Get<IWeaponStats>(); // A second lookup shouldn't warn again.

            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void Should_not_replace_item_data()
        {
            SetCustomData(Create<FakeItemData>("Fake Item Data"));

            _item.ToItem().Get<IItemData>().VerifyIsSome(_item);
        }

        T Create<T>(string name) where T : ScriptableObject
        {
            var obj = ScriptableObject.CreateInstance<T>();
            obj.name = name;
            _created.Add(obj);
            return obj;
        }

        void SetCustomData(params ScriptableObject[] data)
        {
            var serializedItem = new SerializedObject(_item);
            var customData = serializedItem.FindProperty(ItemScriptableObject.CustomDataFieldName);

            customData.arraySize = data.Length;
            for (var i = 0; i < data.Length; i++)
                customData.GetArrayElementAtIndex(i).objectReferenceValue = data[i];

            serializedItem.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
