namespace NoughtsCrosses.Abstractions
{
    public interface IGame
    {
        void Restart();
        Task<bool> Turn();
        Task<bool> IsRestartWanted();
    }
}