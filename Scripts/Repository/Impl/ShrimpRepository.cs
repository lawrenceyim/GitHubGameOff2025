using System.Collections.Generic;

namespace RepositorySystem;

public class ShrimpRepository : IRepository {
    private Dictionary<ulong, Shrimp> _shrimps = new Dictionary<ulong, Shrimp>();

    public void AddShrimp(ulong id, Shrimp shrimp) {
        _shrimps.Add(id, shrimp);
    }

    public void RemoveShrimp(ulong id) {
        Shrimp shrimp = _shrimps[id];
        _shrimps.Remove(id);
        shrimp.QueueFree();
    }

    public void RemoveAllShrimps() {
        foreach (Shrimp shrimp in _shrimps.Values) {
            shrimp.QueueFree();
        }

        _shrimps.Clear();
    }

    public Dictionary<ulong, Shrimp>.ValueCollection GetShrimps() {
        return _shrimps.Values;
    }

    public Shrimp GetShrimp(ulong id) {
        return _shrimps[id];
    }
}