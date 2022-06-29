using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NoughtsCrosses.Abstractions;
using NoughtsCrosses.Common;
using NoughtsCrosses.Utils;

namespace NoughtsCrosses.Classes
{
    /// <summary>
    /// Basic AI behavior. Just marks empty cells from left up to right down
    /// </summary>
    public class AiPlayer : IPlayer
    {
        private readonly AiPlayerBehaviour behaviour;
        private readonly ILogger<AiPlayer> logger;
        private IGameField? field;

        public AiPlayer(IOptions<AiPlayerBehaviour> behaviour, ILogger<AiPlayer> logger)
        {
            this.behaviour = behaviour.Value;
            this.logger = logger;
        }

        public void ReportEnd(bool victory)
        {
            logger.LogInformation("Reporting end, victory: {Victory}", victory);
        }

        public void Init(CellState mark, IGameField field)
        {
            logger.LogInformation("Initialization with mark: {Mark}", mark);

            this.field = field;
        }

        public Task<Point> Turn()
        {
            logger.LogInformation("Turning");

            //Enumerating all cells to find empty
            for (int i = 0; i < field!.Height; i++)
            for (int j = 0; j < field.Width; j++)
                if (field[i, j] == CellState.Empty)
                {
                    Point point = new(i, j);

                    logger.LogDebug("Turning result: {Point}", point);
                    return Task.FromResult(point);
                }

            throw logger.LogExceptionMessage(new InvalidOperationException("No one empty cells"));
        }

        public Task<bool> IsRepeatWanted()
        {
            logger.LogInformation("Repeat wanted: {RepeatWanted}", behaviour.WantRepeat);

            return Task.FromResult(behaviour.WantRepeat);
        }

        public void NotifyFieldChange(Point point)
        {
            logger.LogInformation("Field changed at {Point}", point);
        }

        /// <summary>
        /// Options class for <see cref="AiPlayer"/>
        /// </summary>
        public class AiPlayerBehaviour
        {
            /// <summary>
            /// Default section name in config
            /// </summary>
            public const string SectionName = nameof(AiPlayer);

            /// <summary>
            /// Does should <see cref="AiPlayer"/> want repeat or not
            /// </summary>
            public bool WantRepeat { get; set; }
        }
    }
}