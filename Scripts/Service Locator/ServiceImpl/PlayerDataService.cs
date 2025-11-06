using RepositorySystem;

namespace ServiceSystem;

public class PlayerDataService : IService {
    private PlayerDataRepository _playerDataRepository;

    public PlayerDataService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }

    public void AddShrimp(ShrimpType type, int amount) {
        _playerDataRepository.IncreaseShrimp(type, amount);
    }

    public void RemoveShrimp(ShrimpType type, int amount) {
        _playerDataRepository.DecreaseShrimp(type, amount);
    }

    public int GetShrimp(ShrimpType type) {
        return _playerDataRepository.GetShrimp(type);
    }
}