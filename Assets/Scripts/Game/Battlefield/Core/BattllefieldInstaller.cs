using Game.DI;
using Game.Gameplay;
using UnityEngine;

namespace Game.Battlefield.Core
{
    public class BattllefieldInstaller : MonoInstaller
    {
        [SerializeField] private Camera currentCamera;
        [Space(8f)]
        [SerializeField] private TilemapReaderData tilemapReaderData;
        [SerializeField] private PawnfieldData pawnfieldData;

        public override void Bind(DIContainer container)
        {
            container.RegisterInstance(currentCamera);
            container.RegisterInstance(tilemapReaderData);
            container.RegisterInstance(pawnfieldData);

            container.RegisterType<Gamefield>();
            container.RegisterType<PawnFactory>();
            container.RegisterType<Pawnfield>();

            container.RegisterTypeAndInterfaces<BattleStageCyclic>();
        }
    }
}