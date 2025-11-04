using Godot;
using Godot.Collections;

namespace RepositorySystem;

public partial class PackedSceneRepository : Node, IAutoload, IRepository {
    // Probably not the best way.
    // Figure out a way to make this an export
    private Dictionary<PackedSceneId, PackedScene> _packedScenes = new() {
        {PackedSceneId.Shrimp,  ResourceLoader.Load<PackedScene>("res://Scenes/Game Object/Shrimp.tscn")}
    };

    public PackedScene GetPackedScene(PackedSceneId packedSceneId) {
        return _packedScenes[packedSceneId];
    }
}

public enum PackedSceneId {
    Shrimp = 0
}