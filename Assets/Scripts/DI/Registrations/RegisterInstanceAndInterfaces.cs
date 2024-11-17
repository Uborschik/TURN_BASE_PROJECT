namespace Game.DI
{
    public class RegisterInstanceAndInterfaces : Registration
    {
        private readonly object instance;

        public RegisterInstanceAndInterfaces(object instance)
        {
            this.instance = instance;
        }

        public override object Resolve()
        {
            var type = instance.GetType();

            if (typeof(IInitializable).IsAssignableFrom(type))
            {
                var i = instance as IInitializable;
            }

            return instance;
        }
    }
}