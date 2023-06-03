using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utilities.DependencyInjection
{
    public enum DependencyLifespan
    {
        Transient = 0,
        Singleton = 1
    }

    public class DependencyDescriptor
    {
        public Type ConcreteType { get; private set; }
        public DependencyLifespan Lifespan { get; private set; }

        public DependencyDescriptor(Type type, DependencyLifespan lifespan)
        {
            ConcreteType = type;
            Lifespan = lifespan;
        }
    }

    public class DependencyContainer : IDisposable
    {
        private IReadOnlyDictionary<Type, DependencyDescriptor> _container;

        private ConcurrentDictionary<Type, object> _singletonCache =
            new ConcurrentDictionary<Type, object>();

        internal DependencyContainer(ContainerBuilder containerBuilder)
        {
            _container = containerBuilder.Container;
        }

        public TInterface Resolve<TInterface>()
            where TInterface : class
        {
            var descriptor = GetDescriptor(typeof(TInterface));

            if (descriptor.Lifespan == DependencyLifespan.Singleton)
            {
                object existingInstance = _singletonCache.GetOrAdd(
                    descriptor.ConcreteType,
                    Activate<TInterface>(descriptor.ConcreteType, descriptor.Lifespan));

                return existingInstance as TInterface;
            }

            return Activate<TInterface>(descriptor.ConcreteType, descriptor.Lifespan);
        }

        public object Resolve(Type type)
        {
            var descriptor = GetDescriptor(type);

            if (descriptor.Lifespan == DependencyLifespan.Singleton)
            {
                object existingInstance = _singletonCache.GetOrAdd(
                    descriptor.ConcreteType,
                    Activate<object>(descriptor.ConcreteType, descriptor.Lifespan));

                return existingInstance;
            }

            return Activate<object>(descriptor.ConcreteType, descriptor.Lifespan);
        }

        private DependencyDescriptor GetDescriptor(Type type)
        {
            return _container[type];
        }

        private TInterface Activate<TInterface>(Type concreteType, DependencyLifespan upstreamLifespan)
            where TInterface : class
        {
            var descriptor = GetDescriptor(typeof(TInterface));

            if (descriptor == null)
                throw new Exception($"No dependency description exists for {concreteType}");

            if (upstreamLifespan == DependencyLifespan.Singleton && descriptor.Lifespan == DependencyLifespan.Transient)
                throw new Exception($"Cannot activate a transient instance as a dependency of a singleton object");

            var constructors = concreteType
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (constructors.Length == 0)
                throw new Exception($"Unable to activate {concreteType} due to missing constructors");

            var constructor = constructors[0];
            var constructorParameters = constructor.GetParameters();

            object[] constructorInstances = new object[constructorParameters.Length];

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var parameterType = constructorParameters[i].ParameterType;
                constructorInstances[i] = Resolve(parameterType);
            }

            return constructor.Invoke(constructorInstances) as TInterface;
        }

        public void Dispose()
        {
            foreach (var instance in _singletonCache.Values)
            {
                if (instance is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }

    public class ContainerBuilder
    {
        private ConcurrentDictionary<Type, DependencyDescriptor> _container =
            new ConcurrentDictionary<Type, DependencyDescriptor>();

        public IReadOnlyDictionary<Type, DependencyDescriptor> Container => _container;

        public DependencyContainer Build()
        {
            return new DependencyContainer(this);
        }

        public ContainerBuilder RegisterTransient<TInterface, TConcrete>()
            where TInterface : class
            where TConcrete : TInterface
        {
            Register(
                typeof(TInterface),
                new DependencyDescriptor(typeof(TConcrete), DependencyLifespan.Transient));

            return this;
        }

        public ContainerBuilder RegisterSingleton<TInterface, TConcrete>()
            where TInterface : class
            where TConcrete : TInterface
        {
            Register(
                typeof(TInterface),
                new DependencyDescriptor(typeof(TConcrete), DependencyLifespan.Singleton));

            return this;
        }

        private void Register(Type interfaceType, DependencyDescriptor descriptor)
        {
            _container[interfaceType] = descriptor;
        }
    }
}
