using System.Collections.Generic;
using EvenetSystem;

namespace EventSystem {
    public class EventManager {
        private Dictionary<GameEventId, EventProcessor> _events = new Dictionary<GameEventId, EventProcessor>() {
            { GameEventId.ExampleEventId, new ExampleEventProcessor() },
        };

        public void ProcessEvent(GameEvent gameEvent) {
            _events[gameEvent.Id].Process(gameEvent);
        }
    }
}