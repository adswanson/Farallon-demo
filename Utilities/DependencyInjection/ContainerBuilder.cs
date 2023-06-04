using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Utilities.DependencyInjection
{
    /// <summary>
    /// Builder class for registering type-based dependencies using a fluent syntax.
    /// </summary>
    public sealed class ContainerBuilder
    {
        private ConcurrentDictionary<Type, DependencyDescriptor> _container =
            new ConcurrentDictionary<Type, DependencyDescriptor>();

        private ConcurrentDictionary<Type, object> _prebuiltInstances = 
            new ConcurrentDictionary<Type, object>();

        internal IReadOnlyDictionary<Type, DependencyDescriptor> Container => _container;
        internal IReadOnlyDictionary<Type, object> PrebuiltInstances => _prebuiltInstances;

        /// <summary>
        /// Constructs a <seealso cref="DependencyContainer"/> composed of all currently registered dependencies. 
        /// </summary>
        public DependencyContainer Build()
        {
            return new DependencyContainer(this);
        }

        /// <summary>
        /// Registers a dependency as transient, meaning a new instance will always be returned.
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TConcrete">Concretion type</typeparam>
        public ContainerBuilder RegisterTransient<TInterface, TConcrete>()
            where TInterface : class
            where TConcrete : TInterface
        {
            Register(
                typeof(TInterface),
                new DependencyDescriptor(typeof(TConcrete), DependencyLifespan.Transient));

            return this;
        }

        /// <summary>
        /// Registers a dependency as a singleton, meaning the object will only ever be instantiated once
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TConcrete">Concretion type</typeparam>
        public ContainerBuilder RegisterSingleton<TInterface, TConcrete>()
            where TInterface : class
            where TConcrete : TInterface
        {
            Register(
                typeof(TInterface),
                new DependencyDescriptor(typeof(TConcrete), DependencyLifespan.Singleton));

            return this;
        }

        public ContainerBuilder RegisterSingleton<TInterface>(TInterface instance)
            where TInterface : class
        {
            _prebuiltInstances[typeof(TInterface)] = instance;

            Register(typeof(TInterface),
                new DependencyDescriptor(typeof(TInterface), DependencyLifespan.Singleton));

            return this;
        }

        private void Register(Type interfaceType, DependencyDescriptor descriptor)
        {
            _container[interfaceType] = descriptor;
        }
    }
}
