using Game.DI;
using Game.Gameplay;
using UnityEngine;

namespace Game.Battlefield.Core
{
    public class BattllefieldInstaller : MonoInstaller
    {
        [SerializeField] private TilemapReaderData tilemapReaderData;
        [SerializeField] private PawnfieldData pawnfieldData;

        public override void Bind(DIContainer container)
        {
            container.RegisterInstance(tilemapReaderData);
            container.RegisterType<Gamefield>();
            container.RegisterInstance(pawnfieldData);
            container.RegisterType<PawnFactory>();
            container.RegisterType<Pawnfield>();
            container.RegisterTypeAndInterfaces<BattleStageCyclic>();
        }
    }
}