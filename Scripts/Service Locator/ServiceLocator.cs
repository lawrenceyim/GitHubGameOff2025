using System.Collections.Generic;
using EventSystem;
using Godot;
using InputSystem;

namespace ServiceLocatorSystem {
	public partial class ServiceLocator : Node, IAutoload {
		Dictionary<ServiceName, object> _services = new Dictionary<ServiceName, object>();

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
			GD.Print("Instantiating services...");
			InputStateMachine inputStateMachine = new InputStateMachine();
			_services[ServiceName.InputStateMachine] = inputStateMachine;
			AddChild(inputStateMachine);
		}
	}
}
