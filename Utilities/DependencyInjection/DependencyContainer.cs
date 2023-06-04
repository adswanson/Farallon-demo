using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.DependencyInjection
{
    /// <summary>
    /// A lightweight container for resolving dependencies at runtime.
    /// </summary>
    public sealed class DependencyContainer : IDisposable
    {
        private IReadOnlyDictionary<Type, DependencyDescriptor> _container;

        private ConcurrentDictionary<Type, object> _singletonCache =
            new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Instantiates a DI container based on the provided registrations
        /// </summary>
        internal DependencyContainer(ContainerBuilder containerBuilder)
        {
            _container = containerBuilder.Container;

            foreach(var prebuiltInstance in containerBuilder.PrebuiltInstances)
            {
                _singletonCache[prebuiltInstance.Key] = prebuiltInstance.Value;
            }
        }

        /// <summary>
        /// Resolves the given interface type into a registered concretion
        /// </summary>
        /// <typeparam name="TInterface">Interface type to resolve</typeparam>
        public TInterface Resolve<TInterface>()
            where TInterface : class
        {
            return Resolve(typeof(TInterface)) as TInterface;
        }

        private object Resolve(Type type)
        {
            var descriptor = GetDescriptor(type);

            // Singletons must only be instantiated once- check
            // the singleton cache for an existing instance.
            if (descriptor.Lifespan == DependencyLifespan.Singleton)
            {
                if(!_singletonCache.TryGetValue(type, out _))
                {
                    _singletonCache[type] = Activate(descriptor.ConcreteType);                  
                }

                return _singletonCache[type];
            }

            return Activate(descriptor.ConcreteType);
        }

        private DependencyDescriptor GetDescriptor(Type type)
        {
            return _container[type];
        }

        private object Activate(Type concreteType)
        {
            // Include non-public constructors in case a class restricts access to its constructor
            // i.e. if it's using a static singleton pattern.
            var constructors = concreteType
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (constructors.Length == 0)
                throw new Exception($"Unable to activate {concreteType} due to missing constructors");

            // A limitation of this implementation is that it only works for one constructor, so
            // take the first.
            var constructor = constructors[0];
            var constructorParameters = constructor.GetParameters();
            var constructorInstances = new object[constructorParameters.Length];

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var parameterType = constructorParameters[i].ParameterType;
                constructorInstances[i] = Resolve(parameterType);
            }

            return constructor.Invoke(constructorInstances);
        }

        /// <summary>
        /// Ensures singletons managed by the DI container are properly disposed.
        /// Transient dependencies that implement IDispoable are better managed
        /// through something like a factory pattern rather than resolved through DI.
        /// </summary>
        public void Dispose()
        {
            foreach (var instance in _singletonCache.Values)
            {
                if (instance is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
