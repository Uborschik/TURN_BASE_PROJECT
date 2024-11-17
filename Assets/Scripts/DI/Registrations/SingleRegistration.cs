namespace Game.DI
{
    public class SingleRegistration : Registration
    {
        private readonly object instance;

        public SingleRegistration(Injector injector, object instance)
        {
            this.instance = instance;

            injector.InjectMembers(instance);
        }

        public override object Resolve()
        {
            return instance;
        }
    }
}