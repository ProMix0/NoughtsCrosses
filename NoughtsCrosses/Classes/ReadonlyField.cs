using Microsoft.Extensions.Logging;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Read-only reference to <see cref="IGameField"/>
    /// </summary>
    public sealed class ReadonlyField : IGameField
    {
        private readonly ILogger<IGameField> logger;
        private readonly IGameField proxied;

        /// <summary>
        /// Create new reference
        /// </summary>
        /// <param name="proxied">Proxied instance</param>
        /// <param name="logger">Logger</param>
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
            throw logger.LogExceptionMessage(
                new InvalidOperationException($"You can't call {nameof(Set)} in ReadonlyField"));
        }

        public ReadonlyField AsReadonly() => this;

        public void Clear()
        {
            throw logger.LogExceptionMessage(
                new InvalidOperationException($"You can't call {nameof(Clear)} in ReadonlyField"));
        }

        public bool IsEndGame(out CellState winner) => proxied.IsEndGame(out winner);
    }
}