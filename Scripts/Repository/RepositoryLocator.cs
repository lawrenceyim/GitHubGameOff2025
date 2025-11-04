using System.Collections.Generic;

namespace RepositorySystem;

public class RepositoryLocator {
    private Dictionary<RepositoryName, IRepository> _repositories = new() {
        { RepositoryName.PackedSceneRepository, new PackedSceneRepository() }
    };

    public void AddRepository(RepositoryName repositoryName, IRepository repository) {
        _repositories.Add(repositoryName, repository);
    }
    
    public T GetRepository<T>(RepositoryName repositoryName) {
        return (T)_repositories[repositoryName];
    }    
}