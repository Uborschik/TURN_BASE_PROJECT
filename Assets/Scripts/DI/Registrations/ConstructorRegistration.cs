using System;

namespace Game.DI
{
    public class ConstructorRegistration : Registration
    {
        private readonly Injector injector;
        private readonly Type type;

        private object _instance;


        public ConstructorRegistration(Injector injector, Type type)
        {
            this.injector = injector;
            this.type = type;
        }

        public override object Resolve()
        {
            return _instance ??= CreateInstance();
        }

        private object CreateInstance()
        {
            return injector.CreateInstance(type);
        }
    }
}