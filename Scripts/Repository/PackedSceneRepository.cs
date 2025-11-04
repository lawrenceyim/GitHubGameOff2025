using Godot;
using Godot.Collections;

public partial class PackedSceneRepository : Node, IAutoload {
    [Export] private Dictionary<PackedSceneId, PackedScene> _packedScenes;

    public PackedScene GetPackedScene(PackedSceneId packedSceneId) {
        return _packedScenes[packedSceneId];
    }
}

public enum PackedSceneId {
    Shrimp = 0
}