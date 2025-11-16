namespace RepositorySystem;

public class PlayerDataRepository : IRepository {
    private int _freshShrimps = 0;
    private int _staleShrimps = 0;
    private int _expiringShrimps = 0;
    private int _money = 0;

    public void IncreaseShrimpAmount(ShrimpType shrimpType, int amount) {
        switch (shrimpType) {
            case ShrimpType.Fresh:
                _freshShrimps += amount;
                break;
            case ShrimpType.Stale:
                _staleShrimps += amount;
                break;
            case ShrimpType.Gross:
                _expiringShrimps += amount;
                break;
        }
    }

    public void DecreaseShrimpAmount(ShrimpType shrimpType, int amount) {
        switch (shrimpType) {
            case ShrimpType.Fresh:
                _freshShrimps -= amount;
                break;
            case ShrimpType.Stale:
                _staleShrimps -= amount;
                break;
            case ShrimpType.Gross:
                _expiringShrimps -= amount;
                break;
        }
    }

    public int GetShrimpAmount(ShrimpType shrimpType) {
        return shrimpType switch {
            ShrimpType.Fresh => _freshShrimps,
            ShrimpType.Stale => _staleShrimps,
            ShrimpType.Gross => _expiringShrimps,
        };
    }

    public void SetShrimpAmount(ShrimpType shrimpType, int amount) {
        switch (shrimpType) {
            case ShrimpType.Fresh:
                _freshShrimps = amount;
                break;
            case ShrimpType.Stale:
                _staleShrimps = amount;
                break;
            case ShrimpType.Gross:
                _expiringShrimps = amount;
                break;
        }
    }

    public int GetMoney() {
        return _money;
    }

    public void SetMoney(int money) {
        _money = money;
    }
}