public class PersistentData {
    private static int _freshShrimps = 0;
    private static int _staleShrimps = 0;
    private static int _expiringShrimps = 0;
    private static int _money = 0;

    public static int GetFreshShrimps() {
        return _freshShrimps;
    }

    public static void IncreaseFreshShrimps(int amount) {
        _freshShrimps += amount;
    }

    public static void DecreaseFreshShrimps(int amount) {
        _freshShrimps -= amount;
    }

    public static int GetStaleShrimps() {
        return _staleShrimps;
    }

    public static void IncreaseStaleShrimps(int amount) {
        _staleShrimps += amount;
    }

    public static void DecreaseStaleShrimps(int amount) {
        _staleShrimps -= amount;
    }

    public static int GetExpiringShrimps() {
        return _expiringShrimps;
    }

    public static void IncreaseExpiringShrimps(int amount) {
        _expiringShrimps += amount;
    }

    public static void DecreaseExpiringShrimps(int amount) {
        _expiringShrimps -= amount;
    }

    public static int GetMoney() {
        return _money;
    }

    public static void IncreaseMoney(int amount) {
        _money += amount;
    }

    public static void DecreaseMoney(int amount) {
        _money -= amount;
    }
}