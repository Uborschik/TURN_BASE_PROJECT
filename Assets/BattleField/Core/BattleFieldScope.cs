using Game.Battlefield.Core;
using Game.Battlefield;
using Game.Gameplay.Gamefields;
using Game.Gameplay.Pawnfields.Factories;
using Game.Gameplay;
using VContainer;
using VContainer.Unity;
using Game.Gameplay.Pawnfields;
using UnityEngine;
using Game.Services.InputSystem;

public class BattleFieldScope : LifetimeScope
{
    [SerializeField] private Camera currentCamera;
    [Space(8f)]
    [SerializeField] private SceneConfig sceneConfig;
    [SerializeField] private TilemapReaderConfig tilemapReaderData;
    [SerializeField] private CharacterFactoryConfig characterFactoryData;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(currentCamera);

        builder.RegisterInstance(sceneConfig);
        builder.RegisterInstance(tilemapReaderData);
        builder.RegisterInstance(characterFactoryData);

        builder.Register<Inputs>(Lifetime.Singleton);
        builder.Register<Gamefield>(Lifetime.Singleton);
        builder.Register<CharacterFactory>(Lifetime.Singleton);
        builder.Register<CharacterField>(Lifetime.Singleton);

        builder.Register<BattleStageBehaviour>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
    }
}
