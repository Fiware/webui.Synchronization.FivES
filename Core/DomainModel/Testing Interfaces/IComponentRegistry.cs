﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIVES
{
    /// <summary>
    /// This interface is used internally to mock ComponentRegistry in unit tests. Please do not use it.
    /// </summary>
    public interface IComponentRegistry
    {
        ReadOnlyCollection<ReadOnlyComponentDefinition> RegisteredComponents { get; }
        void Register(ComponentDefinition definition);
        void Upgrade(ComponentDefinition newDefinition, ComponentUpgrader upgrader);
        ReadOnlyComponentDefinition FindComponentDefinition(string componentName);
        event EventHandler<ComponentEventArgs> UpgradedComponent;
    }
}