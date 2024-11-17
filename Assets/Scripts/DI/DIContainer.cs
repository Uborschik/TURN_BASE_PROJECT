using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.DI
{
    public class DIContainer
    {
        private readonly DIContainer parentContainer;
        private readonly Injector injector;
        private readonly Dictionary<Type, Registration> entriesMap = new();
        private readonly HashSet<Type> resolutionsCache = new();

        private readonly List<IInitializable> initializables = new();
        private readonly List<IStartable> startables = new();
        private readonly List<IUpdatable> updatables = new();

        public List<IInitializable> Initializables => initializables;
        public List<IStartable> Startables => startables;
        public List<IUpdatable> Updatables => updatables;

        public DIContainer(DIContainer parentContainer = null)
        {
            this.parentContainer = parentContainer;
            injector = new Injector(this);
        }

        public void RegisterInstanceAndInterfaces<T>(T instance)
        {
            RegisterInstance(instance);

            AddInterface(instance, initializables);
            AddInterface(instance, startables);
            AddInterface(instance, updatables);
        }

        public void RegisterTypeAndInterfaces<T>()
        {
            RegisterType(typeof(T));

            if (typeof(IInitializable).IsAssignableFrom(typeof(T)) || typeof(IStartable).IsAssignableFrom(typeof(T)))
            {
                var instance = Resolve(typeof(T));

                AddInterface(instance, initializables);
                AddInterface(instance, startables);
                AddInterface(instance, updatables);
            }
        }

        public void RegisterType<T>()
        {
            RegisterType(typeof(T));
        }

        public void RegisterType(Type type)
        {
            ThrowIfContainsRegistration(type);

            var registration = new ConstructorRegistration(injector, type);

            Register(type, registration);
        }

        public void RegisterInstance<T>(T instance)
        {
            var key = typeof(T);

            ThrowIfContainsRegistration(key);

            var registration = new SingleRegistration(injector, instance);

            Register(key, registration);
        }

        public bool TryResolve(Type parameterType, out object value)
        {
            value = Resolve(parameterType);

            if (value != null)
            {
                return true;
            }

            return false;
        }

        private object Resolve(Type type)
        {
            if (resolutionsCache.Contains(type))
            {
                throw new Exception($"DI: Cyclic dependency for type {type.FullName}");
            }

            resolutionsCache.Add(type);
            Debug.Log(entriesMap.Count);
            try
            {
                if (entriesMap.TryGetValue(type, out var registration))
                {
                    Debug.Log($"Success for {type}");

                    return registration.Resolve();
                }
                else
                {
                    Debug.Log($"Can`t find registration for {type}");
                }

                Debug.Log(parentContainer);

                if (parentContainer != null)
                {
                    return parentContainer.Resolve(type);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                resolutionsCache.Remove(type);
            }

            throw new Exception($"Couldn't find dependency for type {type.FullName}");
        }

        private void AddInterface<T>(object instance, List<T> interfaces) where T : class
        {
            if (instance is T instanceIsT)
                interfaces.Add(instanceIsT);
        }

        private void Register(Type type, Registration registration)
        {
            entriesMap.Add(type, registration);
            Debug.Log(type);
        }

        private void ThrowIfContainsRegistration(Type type)
        {
            if (entriesMap.ContainsKey(type))
            {
                throw new ArgumentException($"Type {type} is already registered ");
            }
        }
    }
}