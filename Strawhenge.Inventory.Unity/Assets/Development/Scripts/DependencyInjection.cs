using Autofac;
using Autofac.Unity;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Loader;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Menu;
using Strawhenge.Inventory.Unity.Procedures;
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

            builder
                .RegisterType<AutofacEffectFactoryLocator>()
                .As<IEffectFactoryLocator>()
                .InstancePerLifetimeScope();

            RegisterSingletons(builder);
            RegisterScoped(builder);
        });
    }

    static void RegisterSingletons(ContainerBuilder builder)
    {
        builder
            .RegisterType<Layers>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder
            .RegisterType<Settings>()
            .As<ISettings>()
            .SingleInstance();

        builder
            .RegisterType<ResourcesItemRepository>()
            .As<IItemRepository>()
            .SingleInstance();

        builder
            .RegisterType<ResourcesApparelRepository>()
            .As<IApparelRepository>()
            .SingleInstance();

        builder
            .RegisterType<InventoryMenuScriptContainer>()
            .WithParameter(new TypedParameter(typeof(ILogger), new UnityLogger(null)))
            .AsSelf()
            .As<IInventoryMenu>()
            .SingleInstance();

        builder
            .RegisterType<ItemContainerMenuScriptContainer>()
            .WithParameter(new TypedParameter(typeof(ILogger), new UnityLogger(null)))
            .AsSelf()
            .As<IItemContainerMenu>()
            .SingleInstance();
    }

    static void RegisterScoped(ContainerBuilder builder)
    {
        builder
            .RegisterType<DropPoint>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .Register(x => x.Resolve<Inventory>().Hands)
            .As<Hands>()
            .InstancePerLifetimeScope();

        builder
            .Register(x => x.Resolve<Inventory>().Holsters)
            .As<Holsters>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HandScriptsContainer>()
            .AsSelf()
            .As<HandScriptsContainer>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HoldItemAnimationHandler>()
            .As<IHoldItemAnimationHandler>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ProduceItemAnimationHandler>()
            .As<IProduceItemAnimationHandler>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ConsumeItemAnimationHandler>()
            .As<IConsumeItemAnimationHandler>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<HolsterScriptsContainer>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .Register(x => x.Resolve<Inventory>().StoredItems)
            .As<StoredItems>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ApparelSlotScripts>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .Register(x => x.Resolve<Inventory>().ApparelSlots)
            .As<ApparelSlots>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<Inventory>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<ApparelGameObjectInitializer>()
            .As<IApparelGameObjectInitializer>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<InventoryLoader>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<InventoryInfoGenerator>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<LoadInventoryDataFactory>()
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
            .RegisterType<StackableSetApparelDrop>()
            .As<ISetApparelContainerPrefab>()
            .As<IApparelDrop>()
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
    }
}