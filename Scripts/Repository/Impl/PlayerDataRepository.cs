namespace RepositorySystem;

public class PlayerDataRepository : IRepository {
    public int FreshShrimps { get; set; } = 0;
    public int StaleShrimps { get; set; } = 0;
    public int GrossShrimps { get; set; } = 0;
    public int Money { get; set; } = 0;
    public int HorseHealthUpgradeLevel { get; set; } = 1;
    public int CatchUpgradeLevel { get; set; } = 1;
    public int HorseSpeedUpgradeLevel { get; set; } = 1;
}