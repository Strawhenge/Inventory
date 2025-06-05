using Autofac;
using Autofac.Unity;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Procedures;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Menu;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

public static class DependencyInjection
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Configure()
    {
        AutofacUnity.Configure(builder =>
        {
            builder.RegisterModule(new UnityComponentsModule());

            builder
                .RegisterType<UnityLogger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            RegisterSingletons(builder);
            RegisterScoped(builder);
        });
    }

    static void RegisterSingletons(ContainerBuilder builder)
    {
        builder
            .RegisterType<ResourcesItemRepository>()
            .As<IItemRepository>()
            .SingleInstance();

        builder
            .RegisterType<ResourcesApparelPieceRepository>()
            .As<IApparelPieceRepository>()
            .SingleInstance();

        builder
            .RegisterType<InventoryMenuScriptContainer>()
            .WithParameter(new TypedParameter(typeof(ILogger), new UnityLogger(null)))
            .AsSelf()
            .As<IInventoryMenu>()
            .SingleInstance();

        builder
            .RegisterType<LootMenuScriptContainer>()
            .WithParameter(new TypedParameter(typeof(ILogger), new UnityLogger(null)))
            .AsSelf()
            .As<ILootMenu>()
            .SingleInstance();
    }

    static void RegisterScoped(ContainerBuilder builder)
    {
        builder
            .RegisterType<DropPoint>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HandScriptsContainer>()
            .AsSelf()
            .As<HandScriptsContainer>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HoldItemAnimationHandler>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ProduceItemAnimationHandler>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ConsumeItemAnimationHandler>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HolsterScriptsContainer>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ApparelSlotScriptsContainer>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<Inventory>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<InventoryLootSource>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<LootCollectionSource>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<LootDrop>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<EffectFactory>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<IncreaseHealthEffectFactory>()
            .As<IEffectFactory<IncreaseHealthEffectScriptableObject>>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<IncreaseArmourEffectFactory>()
            .As<IEffectFactory<IncreaseArmourEffectScriptableObject>>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ApparelViewFactory>()
            .As<IApparelViewFactory>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ItemProceduresFactory>()
            .As<IItemProceduresFactory>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<PrefabInstantiatedEvents>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}