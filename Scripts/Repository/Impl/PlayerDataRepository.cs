namespace RepositorySystem;

public class PlayerDataRepository : IRepository {
    private int _freshShrimps = 0;
    private int _staleShrimps = 0;
    private int _expiringShrimps = 0;
    private int _money = 0;

    public void IncreaseShrimp(ShrimpType shrimpType, int amount) {
        switch (shrimpType) {
            case ShrimpType.Fresh:
                _freshShrimps += amount;
                break;
            case ShrimpType.Stale:
                _staleShrimps += amount;
                break;
            case ShrimpType.Expiring:
                _expiringShrimps += amount;
                break;
        }
    }

    public void DecreaseShrimp(ShrimpType shrimpType, int amount) {
        switch (shrimpType) {
            case ShrimpType.Fresh:
                _freshShrimps -= amount;
                break;
            case ShrimpType.Stale:
                _staleShrimps -= amount;
                break;
            case ShrimpType.Expiring:
                _expiringShrimps -= amount;
                break;
        }
    }

    public int GetShrimp(ShrimpType shrimpType) {
        return shrimpType switch {
            ShrimpType.Fresh => _freshShrimps,
            ShrimpType.Stale => _staleShrimps,
            ShrimpType.Expiring => _expiringShrimps,
        };
    }

}