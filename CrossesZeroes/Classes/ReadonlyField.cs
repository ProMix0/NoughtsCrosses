using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CrossesZeroes.Utils;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Поле игры в крестики-нолики только для чтения
    /// </summary>
    public sealed class ReadonlyField : ICrossesZeroesField
    {
        private readonly ICrossesZeroesField proxied;
        private readonly ILogger<ReadonlyField> logger;

        /// <summary>
        /// Создаёт новый экземпляр
        /// </summary>
        /// <param name="proxied">Замещаемый экземпляр</param>
        public ReadonlyField(ICrossesZeroesField proxied,ILogger<ReadonlyField> logger)
        {
            this.proxied = proxied;
            this.logger = logger;
        }

        public CellState this[int i, int j] => proxied[i, j];

        public int Height => proxied.Height;
        public int Width => proxied.Width;

        public void Set(Point point, CellState markType)
        {
            logger.LogMessageAndThrow( new InvalidOperationException("Attempts to Set() in ReadonlyField"));
        }

        public ICrossesZeroesField AsReadonly() => this;

        public void Clear()
        {
            logger.LogMessageAndThrow(new InvalidOperationException("Attempts to Clear() in ReadonlyField"));
        }

        public bool IsEndGame(out CellState winner) => proxied.IsEndGame(out winner);
    }

    public class ReadonlyFieldBinder
    {
        private readonly IServiceProvider provider;

        public ReadonlyFieldBinder(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ReadonlyField Bind(ICrossesZeroesField proxied)
        {
            return new(proxied, provider.GetRequiredService<ILogger<ReadonlyField>>());
        }
    }
}