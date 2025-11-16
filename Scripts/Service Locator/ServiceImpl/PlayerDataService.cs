using RepositorySystem;
using ServiceSystem;

public class PlayerDataService : IService {
    private readonly PlayerDataRepository _playerDataRepository;

    public PlayerDataService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }

    public void AddShrimpAmount(ShrimpType type, int amount) {
        switch (type) {
            case ShrimpType.Fresh:
                _playerDataRepository.FreshShrimps = _playerDataRepository.FreshShrimps + amount;
                break;
            case ShrimpType.Stale:
                _playerDataRepository.StaleShrimps = _playerDataRepository.StaleShrimps + amount;
                break;
            case ShrimpType.Gross:
                _playerDataRepository.GrossShrimps = _playerDataRepository.GrossShrimps + amount;
                break;
        }
    }

    public int GetShrimpAmount(ShrimpType type) {
        return type switch {
            ShrimpType.Fresh => _playerDataRepository.FreshShrimps,
            ShrimpType.Stale => _playerDataRepository.StaleShrimps,
            ShrimpType.Gross => _playerDataRepository.GrossShrimps,
        };
    }

    public void SetShrimpAmount(ShrimpType type, int amount) {
        switch (type) {
            case ShrimpType.Fresh:
                _playerDataRepository.FreshShrimps = amount;
                break;
            case ShrimpType.Stale:
                _playerDataRepository.StaleShrimps = amount;
                break;
            case ShrimpType.Gross:
                _playerDataRepository.GrossShrimps = amount;
                break;
        }
    }

    public void SetMoney(int money) {
        _playerDataRepository.Money = money;
    }

    public int GetMoney() {
        return _playerDataRepository.Money;
    }
}