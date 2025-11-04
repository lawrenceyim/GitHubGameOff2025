using System.Collections.Generic;
using ServiceSystem;

namespace RepositorySystem;

public class RepositoryLocator : IService {
    private Dictionary<RepositoryName, IRepository> _repositories = new() {
        { RepositoryName.PackedSceneRepository, new PackedSceneRepository() },
        { RepositoryName.ShrimpRepository, new ShrimpRepository() }
    };

    public void AddRepository(RepositoryName repositoryName, IRepository repository) {
        _repositories.Add(repositoryName, repository);
    }
    
    public T GetRepository<T>(RepositoryName repositoryName) {
        return (T)_repositories[repositoryName];
    }    
}