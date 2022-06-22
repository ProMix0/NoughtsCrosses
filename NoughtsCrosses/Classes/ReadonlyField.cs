using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Поле игры в крестики-нолики только для чтения
    /// </summary>
    public sealed class ReadonlyField : IGameField
    {
        private readonly IGameField proxied;
        private readonly ILogger<IGameField> logger;

        /// <summary>
        /// Создаёт новый экземпляр
        /// </summary>
        /// <param name="proxied">Замещаемый экземпляр</param>
        public ReadonlyField(IGameField proxied, ILogger<IGameField> logger)
        {
            this.proxied = proxied;
            this.logger = logger;
        }

        public CellState this[int i, int j] => proxied[i, j];

        public int Height => proxied.Height;
        public int Width => proxied.Width;

        public void Set(Point point, CellState markType)
        {
            throw logger.LogExceptionMessage(new InvalidOperationException("Attempts to Set() in ReadonlyField"));
        }

        public ReadonlyField AsReadonly() => this;

        public void Clear()
        {
            throw logger.LogExceptionMessage(new InvalidOperationException("Attempts to Clear() in ReadonlyField"));
        }

        public bool IsEndGame(out CellState winner) => proxied.IsEndGame(out winner);
    }
}