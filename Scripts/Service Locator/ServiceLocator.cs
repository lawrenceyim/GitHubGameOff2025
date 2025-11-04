using System.Collections.Generic;
using Godot;
using InputSystem;
using RepositorySystem;

namespace ServiceSystem;

public partial class ServiceLocator : Node, IAutoload {
    public static string AutoloadPath => "/root/ServiceLocator";
    private readonly RepositoryLocator _repositoryLocator = new RepositoryLocator();
    private readonly Dictionary<ServiceName, object> _services = new Dictionary<ServiceName, object>();

    public override void _EnterTree() {
        _InstantiateServices();
    }

    public void AddService(ServiceName serviceName, object service) {
        _services.Add(serviceName, service);
    }

    public void RemoveService(ServiceName serviceName) {
        _services.Remove(serviceName);
    }

    public T GetService<T>(ServiceName serviceName) {
        return (T)_services[serviceName];
    }

    private void _InstantiateServices() {
        _services[ServiceName.RepositoryLocator] = _repositoryLocator;

        InputStateMachine inputStateMachine = new();
        _services[ServiceName.InputStateMachine] = inputStateMachine;
        AddChild(inputStateMachine);
        
        CollectibleManager collectibleManager = new();
        _services[ServiceName.CollectibleManager] = collectibleManager;
        AddChild(collectibleManager);
    }
}