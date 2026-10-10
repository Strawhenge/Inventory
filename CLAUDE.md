# CLAUDE.md

Guidance for AI coding agents working in this repository.

## Project

Strawhenge Inventory is a character inventory system for Unity, with two parts:

- `Strawhenge.Inventory/`: a .NET solution containing the core library (`netstandard2.0`, no Unity dependency) and xUnit tests (`net8.0`).
- `Strawhenge.Inventory.Unity/`: a Unity **2021.3** project. `Assets/Package/` is the distributed package `com.strawhenge.inventory`, installed by users via Git URL. `Assets/Development/` is a development scene (Y Bot character, test items) and is not shipped.

`README.md` is for **package users only**. Don't add build, contributing or other development content to it.

## Build and test

```bash
dotnet build Strawhenge.Inventory
dotnet test Strawhenge.Inventory
```

- Requires the .NET 8 SDK and the Strawhenge GitHub Packages NuGet feed (`strawhenge.common`, `functionalutilities`). The feed needs a token with `read:packages`.
- Building the library copies `Strawhenge.Inventory.dll` into `Assets/Package/Runtime/` (post-build `xcopy`, Windows only). Never edit or hand-copy that DLL.
- CI (`.github/workflows/ci.yml`) builds and tests the .NET solution on pushes and PRs to master. **Unity code is not compiled by CI.** If Unity isn't available to you, say that Unity changes are uncompiled and give the user a test checklist.
- Headless Unity compile check, if Unity is installed: `Unity.exe -batchmode -nographics -quit -projectPath Strawhenge.Inventory.Unity -logFile -`

## Code conventions

- Private members have no access modifier (`readonly ILogger _logger;`, `void Foo()`). Fields are `_camelCase`. Serialized fields are private, with `[SerializeField]`.
- Use `Maybe<T>` (FunctionalUtilities) for optional values rather than null, in the core library and the package.
- Renaming a serialized field needs `[FormerlySerializedAs("oldName")]`.
- **Never look up serialized fields with string literals.** Expose the name from the owning class as `internal static string XFieldName => nameof(_x);` in a partial `<Class>.Internals.cs` file (see `ItemScriptableObject.Internals.cs`). The Editor assembly can see Runtime internals via `Runtime/Assembly.cs`.
- Tests are behaviour-style: one class per scenario, named like `Hold_when_another_item_in_hand`, grouped by action folder (`ItemTests/HoldRightHand/`, …). Fakes live in `Fakes/`.

## Unity package rules

- **Every new file and folder under `Assets/Package/` needs a `.meta` file with a new unique GUID.** The package is installed from Git as an immutable package, so Unity ignores any asset without a `.meta`. Never change an existing GUID.
- Code in `Assets/Package/` must compile on Unity 2021.3 (C# 9). The core library targets `netstandard2.0` (C# 7.3 by default).
- Editor tools live in `Editor/Tools/<Tool>/`, with menu items under `Strawhenge/Inventory/`. Put the logic in a static class separate from the window or wizard (e.g. `SetupCharacter`, `CreateItem`, `ItemOffsets`), so it can be called and tested without UI.
- Edit assets and components through `SerializedObject` / `Undo` so changes are undoable.
- IMGUI: after changing state that the rest of the current `OnGUI` pass depends on, or after showing a modal dialog, call `GUIUtility.ExitGUI()`. See `IfConfirmed` in `ItemWizardWindow.Pose.cs`.
- Inside `namespace Strawhenge.Inventory.Unity.Editor.Tools…`, `Tools` resolves to that namespace. Write `UnityEditor.Tools` explicitly.

## Repository and git

- Files use **CRLF** line endings, and the repo is used with `core.autocrlf=true`. Keep new files CRLF.
- Commits are authored as `Strawhenge <strawhengestudios@gmail.com>`.
- Stage files by explicit path; never `git add -A` / `commit -a`. The user tests in Unity in the same working copy, which modifies `Assets/Development/` assets that must not be committed unless asked.

## Workflow

- Plan first for anything non-trivial, and wait for the user's go-ahead before changing code when they ask for a plan.
- Work happens on a feature branch. Each piece of work gets its own branch and a **pull request into the feature branch**. The user reviews, tests in Unity and merges; don't merge PRs yourself. The feature branch then goes to master through its own PR.
- Keep PRs small. Each PR description says what changed, why, what's untested, and gives a test checklist.
- Fixes from review go as new commits on the same PR branch, with a PR comment explaining them. When fixing a reported bug, check for the same class of bug elsewhere.
