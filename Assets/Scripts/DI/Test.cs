using UnityEngine;

namespace Game.DI
{
    public class Test : IInitializable, IStartable, IUpdatable
    {
        public void Initialise()
        {
            Debug.Log("I am Initialised");
        }

        public void Start()
        {
            Debug.Log("I am Started");
        }

        public void Update()
        {
            Debug.Log("I am Updated");
        }
    }
}