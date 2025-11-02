using EvenetSystem;

namespace EventSystem {
    public record ExampleGameEvent : GameEvent {
        public GameEventId Id { get; }
        public string Message { get; }

        public ExampleGameEvent(GameEventId id, string message) {
            Id = id;
            Message = message;
        }
    }
}