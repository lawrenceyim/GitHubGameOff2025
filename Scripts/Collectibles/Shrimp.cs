using Godot;

public partial class Shrimp : Node2D, ICollectable {
    private ulong _id;
    private int _amount;

    public void Initialize(ulong id, int amount) {
        _id = id;
        _amount = amount;
    }

    public ulong GetId() {
        return _id;
    }

    public int GetAmount() {
        return _amount;
    }

    public void Collect() {
        throw new System.NotImplementedException();
    }
}