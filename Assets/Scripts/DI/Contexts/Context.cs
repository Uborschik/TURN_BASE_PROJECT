using Game.Services.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DI
{
    public interface IInitializable
    {
        void Initialise();
    }

    public interface IStartable
    {
        void Start();
    }

    public interface IUpdatable
    {
        void Update();
    }

    public abstract class Context : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> monoInstallers;

        private DIContainer sceneContainer;
        private Injector injector;

        internal DIContainer DIContainer => sceneContainer;
        internal Injector Injector => injector;

        protected virtual void Awake()
        {
            var container = new DIContainer();

            container.RegisterType<InputInstaller>();

            Run(container);

            sceneContainer.Initializables.ForEach(i => i.Initialise());
        }

        protected virtual void Start()
        {
            sceneContainer.Startables.ForEach(s => s.Start());
        }

        protected virtual void Update()
        {
            sceneContainer.Updatables.ForEach(s => s.Update());
        }

        public virtual void Run(DIContainer rootContainer)
        {
            sceneContainer = new DIContainer(rootContainer);
            injector = new Injector(sceneContainer);

            InstallMonoInstallers();
        }

        private void InstallMonoInstallers()
        {
            monoInstallers.ForEach(x => x.Bind(sceneContainer));
        }
    }
}