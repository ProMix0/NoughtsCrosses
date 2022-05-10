namespace CrossesZeroes.Abstractions
{
    public interface ICrossesZeroesGame
    {
        void Restart();
        Task<bool> Turn();
        Task<bool> IsRestartWanted();
    }
}