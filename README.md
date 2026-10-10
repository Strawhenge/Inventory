# Strawhenge Inventory

A character inventory system for Unity. Characters hold items in their left and right hands, equip them to holsters (hip, back, …), store them by weight, wear apparel in slots, and consume items to apply effects. Every action is played out through the character's Animator: drawing a sword waits for the hand to reach the hilt, and putting it away waits for it to reach the sheath.

The rules live in a plain .NET library (`Strawhenge.Inventory`) with no Unity dependency. The Unity package (`com.strawhenge.inventory`) adds components, ScriptableObject data, animation tooling and sample menus on top.

## Features

- **Hands**: one- and two-handed items, holding in either hand, swapping hands.
- **Holsters**: equip items to named holsters, then draw and put away with animations.
- **Storage**: a weight-limited bag for storable items.
- **Temporary items**: hold items that aren't stored anywhere (e.g. picked up off the ground).
- **Apparel**: pieces worn in named slots, with effects applied while worn.
- **Consumables**: consume from either hand to apply effects.
- **Effects**: plug in your own game logic (health, armour, buffs, …).
- **Procedure queue**: actions run in order and wait for animations; `Interrupt()` cancels pending ones.
- **Loot**: loot containers and other characters' inventories.
- **Save/load**: export and import inventory state.
- **Editor tooling**: one-click character setup and wizards that build Animator states for you.

## Requirements

- Unity **2021.3** or newer.
- Two other Strawhenge packages: `com.strawhenge.common` and `com.strawhenge.functionalutilities` (see below).

## Installation

Unity can't resolve Git dependencies of a Git package by itself, so add all three packages to your project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.strawhenge.functionalutilities": "https://github.com/Strawhenge/FunctionalUtilities.git?path=/Package#release",
    "com.strawhenge.common": "https://github.com/Strawhenge/Common.git?path=/Strawhenge.Common.Unity/Assets/Package#release",
    "com.strawhenge.inventory": "https://github.com/Strawhenge/Inventory.git?path=/Strawhenge.Inventory.Unity/Assets/Package#release",
    ...
  }
}
```

You can also add them one at a time from **Window > Package Manager > + > Add package from git URL…**, in the order shown above.

The `#release` suffix tracks the latest release. To pin a version, replace it with a specific commit hash.

## Quick start

### 1. Set up a character

1. Put your character in a scene. It needs an **Animator** with an **Animator Controller**; a humanoid rig is recommended.
2. Select the character and choose **Strawhenge > Inventory > Set Up Character…** (or right-click it in the Hierarchy: **Strawhenge > Inventory > Set Up Character…**).
3. Click **Set Up**.

The tool:

- adds an `InventoryScript` to the character
- creates **Left Hand Item Point** and **Right Hand Item Point** objects on the humanoid hand bones, with `LeftHandScript` / `RightHandScript`
- links the hands and Animator on `InventoryScript`
- adds the inventory parameters and an **Inventory** layer to the Animator Controller
- sets a default stored-items weight capacity

Anything already set up is kept, and the whole operation is a single undo step. A report lists what was done and what's left.

### 2. Add holsters (optional)

1. Create a holster asset per holster type: **Assets > Create > Strawhenge > Inventory > Holster** (e.g. `Left Hip`, `Back`). The asset's name is the holster's name.
2. Add an empty child under the matching bone (e.g. the hips or spine), add a `HolsterScript`, and assign the holster asset.
3. Add the `HolsterScript` to the **Holsters** list on `InventoryScript`.

### 3. Create an item

1. Make a prefab for the item as it appears in the hand and holster, and add an `ItemScript` to it.
2. Optionally, make a pickup prefab (the item lying in the world) and add an `ItemPickupScript` to it.
3. **Assets > Create > Strawhenge > Inventory > Item**, then fill in:
   - **Prefab** / **Pickup Prefab**
   - **Size**: one- or two-handed
   - **Is Storable** / **Weight**
   - **Left/Right Hand Hold Item Data**: position and rotation offsets in each hand, plus an optional hold animation
   - **Holster Item Data**: one entry per holster the item fits, with offsets and optional draw/put-away animations
   - **Consumable**: optional effects and consume animation

The asset's name is the item's name.

### 4. Use it from code

```csharp
using Strawhenge.Inventory.Unity;
using Strawhenge.Inventory.Unity.Items.ItemData;
using UnityEngine;

public class GiveSword : MonoBehaviour
{
    [SerializeField] InventoryScript _inventory;
    [SerializeField] ItemScriptableObject _sword;

    void Start()
    {
        var inventory = _inventory.Inventory;
        var sword = inventory.CreateItem(_sword);

        // Equip to a holster, then draw it into the right hand.
        sword.Holsters["Left Hip"].Do(holster => holster.Equip());
        sword.HoldRightHand(() => Debug.Log("Sword drawn."));
    }
}
```

Actions are queued, so the call above draws the sword *after* it has been holstered. The optional callback runs when the action (including its animation) finishes.

Common operations on an `InventoryItem`:

| Call | What it does |
|---|---|
| `HoldLeftHand()` / `HoldRightHand()` | Take the item into a hand, drawing from a holster or storage if needed |
| `SwapHands()` | Move the item to the other hand |
| `PutAway()` | Return the item to its holster or storage |
| `ClearFromHands()` | Empty the hand (puts away when possible) |
| `Holsters[name].Equip()` / `.Unequip()` | Assign the item to, or remove it from, a holster |
| `Storable.Do(s => s.AddToStorage())` | Put the item in storage (subject to weight capacity) |
| `Consumable.Do(c => c.ConsumeRightHand())` | Consume the item and apply its effects |
| `Drop()` | Drop the item into the world (spawns its pickup prefab) |
| `Discard()` | Remove the item without dropping it |

On `Inventory`:

| Call | What it does |
|---|---|
| `CreateItem(...)` | Add an item (from an `ItemScriptableObject`, an `ItemPickupScript` or an `Item`) |
| `GetItemOrCreateTemporary(item)` | Find an item already in the inventory, or create a temporary one |
| `CreateApparelPiece(...)` then `.Equip()` | Wear apparel |
| `Interrupt()` | Cancel all queued actions |
| `ExportState()` / `ImportState(state)` | Save and restore |
| `Loot()` | Expose the inventory as a loot source |

Picking an item up from the world:

```csharp
var item = inventory.CreateItem(pickupScript); // destroys the pickup by default
item.HoldRightHand();
```

## Animation

Inventory actions talk to the Animator through these parameters (all created for you by **Set Up Character…** or **Inventory Animator Layer**):

| Parameter | Type | Used for |
|---|---|---|
| `Draw Item` / `Draw Item ID` | Trigger / Int | Drawing an item into a hand |
| `Put Away Item` / `Put Away Item ID` | Trigger / Int | Putting an item away |
| `Consume Item` / `Consume Item ID` | Trigger / Int | Consuming an item |
| `Hold Item Left ID` / `Hold Item Right ID` | Int | Holding poses (0 = none) |
| `Interrupt Inventory` | Trigger | Cancelling the current inventory animation |

Use these wizards to add animations. Each one adds the Animator states and transitions and creates an animation asset that you assign on an item:

- **Assets > Create > Strawhenge > Inventory > Draw Item Animation…**
- **Assets > Create > Strawhenge > Inventory > Put Away Item Animation…**
- **Assets > Create > Strawhenge > Inventory > Consume Item Animation…**
- **Assets > Create > Strawhenge > Inventory > Hold Item Animation…**
- **Strawhenge > Inventory > Inventory Animator Layer** adds another inventory layer, e.g. with an avatar mask.

**Animation events.** Draw and put-away clips must call these functions at the moment the hand touches the item:

- `OnGrabItem`: in **draw** clips, when the hand grabs the item from the holster
- `OnReleaseItem`: in **put-away** clips, when the hand lets go of the item

Add them in the clip's import settings under **Animation > Events**. An `AnimationEventReceiverScript` is added to the Animator's GameObject automatically at runtime to receive them.

Without draw/put-away animations configured, items move between hand and holster instantly.

## Apparel

1. **Assets > Create > Strawhenge > Inventory > Apparel > Apparel Slot** (e.g. `Head`, `Torso`).
2. Add an `ApparelSlotScript` under the matching bone, assign the slot, and add it to **Apparel Slots** on `InventoryScript`.
3. **Assets > Create > Strawhenge > Inventory > Apparel > Apparel Piece**: assign a prefab (with `ApparelPieceScript`), the slot, an offset and optional effects.
4. In code: `inventory.CreateApparelPiece(pieceAsset).Equip();`

Unequipped apparel is dropped in a loot container, so assign a **Loot Drop Prefab** (a prefab with `LootCollectionScript`) on `InventoryScript` if apparel can be removed.

## Effects

Effects are how items and apparel change your game state. You need three things:

```csharp
// 1. Data: a ScriptableObject you can attach to items and apparel.
[CreateAssetMenu(menuName = "My Game/Increase Health")]
public class IncreaseHealthData : EffectScriptableObject
{
    [SerializeField] int _amount;
    public int Amount => _amount;
    public override EffectData Data => EffectData.From(this);
}

// 2. The effect itself.
public class IncreaseHealthEffect : Effect
{
    readonly int _amount;
    public IncreaseHealthEffect(int amount) => _amount = amount;
    protected override void PerformApply() { /* add _amount health */ }
    protected override void PerformRevert() { /* remove it again */ }
}

// 3. A locator that maps data types to factories. Add it to the scene
//    and assign it to "Effect Factory Locator" on InventoryScript.
public class MyEffectFactoryLocator : BaseEffectFactoryLocatorScript, IEffectFactory<IncreaseHealthData>
{
    public Effect Create(IncreaseHealthData data) => new IncreaseHealthEffect(data.Amount);

    public override Maybe<IEffectFactory<TData>> Find<TData>() =>
        this is IEffectFactory<TData> factory
            ? Maybe.Some(factory)
            : Maybe.None<IEffectFactory<TData>>();
}
```

Consumables apply their effects once. Apparel applies effects when equipped and reverts them when removed.

## Menus and loot

The package defines abstract `InventoryMenuScript` and `LootMenuScript` components. Put one of each in a scene and the inventory finds it automatically; for example, apparel drops wait until the menus close. Ready-made UI Toolkit versions are in `Runtime/Menu/*/Sample` (**Sample Inventory Menu** and **Sample Loot Menu** prefabs).

Open a loot menu on a container or another character:

```csharp
lootMenu.Open(lootCollectionScript.Source);
lootMenu.Open(otherCharacter.Inventory.Loot());
```

## Other extension points

- **`ItemContextHandlerScript`**: attach per-instance data (ammo count, durability, …) to an item. The `Context` follows the item from pickup, to hand, to dropped pickup.
- **Subclass `ItemScriptableObject`** and override `Set(IDataSetter)` to attach your own data to items; read it back with `item.Get<T>()`.
- **`ImportInventoryStateScript`**: give a character a starting loadout from the Inspector.
- **`ResourcesItemRepository` / `ResourcesApparelPieceRepository`**: look up item and apparel assets by name from `Resources` folders (useful for save files). Names must be unique.
- **UnityEvents** on `InventoryScript`, `HandScript` and `HolsterScript` fire when item and apparel prefabs are spawned, set and unset.

## Repository layout

```
Strawhenge.Inventory/                 .NET solution (no Unity dependency)
  Strawhenge.Inventory/               Core library, netstandard2.0
  Strawhenge.Inventory.Tests/         xUnit tests
Strawhenge.Inventory.Unity/           Unity 2021.3 project
  Assets/Package/                     The distributed package (com.strawhenge.inventory)
    Runtime/                          Components, data, procedures, menus
    Editor/                           Inspectors and wizards
  Assets/Development/                 Development scene, Y Bot character and test items
```
