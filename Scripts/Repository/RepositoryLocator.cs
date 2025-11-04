using System.Collections.Generic;
using ServiceSystem;

namespace RepositorySystem;

public class RepositoryLocator : IService {
    private Dictionary<RepositoryName, IRepository> _repositories = new() {
        { RepositoryName.PackedScene, new PackedSceneRepository() },
        { RepositoryName.Shrimp, new ShrimpRepository() },
        { RepositoryName.PlayerData, new PlayerDataRepository() }
    };

    public void AddRepository(RepositoryName repositoryName, IRepository repository) {
        _repositories.Add(repositoryName, repository);
    }

    public T GetRepository<T>(RepositoryName repositoryName) {
        return (T)_repositories[repositoryName];
    }
}