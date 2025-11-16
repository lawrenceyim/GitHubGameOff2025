using RepositorySystem;

namespace ServiceSystem;

public class PlayerDataService : IService {
    private PlayerDataRepository _playerDataRepository;

    public PlayerDataService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }

    public void AddShrimpAmount(ShrimpType type, int amount) {
        _playerDataRepository.IncreaseShrimpAmount(type, amount);
    }

    public void RemoveShrimpAmount(ShrimpType type, int amount) {
        _playerDataRepository.DecreaseShrimpAmount(type, amount);
    }

    public int GetShrimpAmount(ShrimpType type) {
        return _playerDataRepository.GetShrimpAmount(type);
    }

    public void SetShrimpAmount(ShrimpType type, int amount) {
        _playerDataRepository.SetShrimpAmount(type, amount);
    }

    public void SetMoney(int money) {
        _playerDataRepository.SetMoney(money);
    }

    public int GetMoney() {
        return _playerDataRepository.GetMoney();
    }
}