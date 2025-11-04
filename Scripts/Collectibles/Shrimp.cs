using Godot;

public partial class Shrimp : Node2D, ICollectable {
    private ulong _id;

    public void Initialize(ulong id) {
        _id = id;
    }

    public ulong Id() {
        return _id;
    }

    public void Collect() {
        throw new System.NotImplementedException();
    }
}