using System;

namespace Utilities.DependencyInjection
{
    /// <summary>
    /// Describes the properties needed to resolve a dependency.
    /// </summary>
    internal sealed class DependencyDescriptor
    {
        /// <summary>
        /// The concrete implementation
        /// </summary>
        internal Type ConcreteType { get; private set; }

        /// <summary>
        /// The object lifespan
        /// </summary>
        internal DependencyLifespan Lifespan { get; private set; }

        public DependencyDescriptor(Type type, DependencyLifespan lifespan)
        {
            ConcreteType = type;
            Lifespan = lifespan;
        }
    }
}
