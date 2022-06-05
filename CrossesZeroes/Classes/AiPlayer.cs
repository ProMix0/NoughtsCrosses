using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossesZeroes.Classes
{
    /// <summary>
    /// Игрок-компьютер
    /// </summary>
    public class AiPlayer : IAiPlayer
    {
        private readonly AiPlayerBehaviour behaviour;
        private readonly ILogger<AiPlayer> logger;
        private ICrossesZeroesField? field;

        public AiPlayer(IOptions<AiPlayerBehaviour> behaviour, ILogger<AiPlayer> logger)
        {
            this.behaviour = behaviour.Value;
            this.logger = logger;
        }

        public void ReportEnd(bool victory)
        {
            logger.LogInformation("ReportEnd()", victory);
        }

        public void Init(CellState mark, ICrossesZeroesField field)
        {
            logger.LogDebug("Init()", mark);

            this.field = field;
        }

        public Task<Point> Turn()
        {
            logger.LogInformation("Turn()");

            //Перебор всех клеток в поиске свободных
            for (int i = 0; i < field!.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (field[i, j] == CellState.Empty)
                    {
                        Point point = new(i, j);

                        logger.LogDebug("Turn() result", point);
                        return Task.FromResult(point);
                    }

            Exception exception = new InvalidProgramException("No one empty cells");

            logger.LogError(exception, "No one empty cells");
            throw exception;
        }

        public Task<bool> IsRepeatWanted()
        {
            logger.LogInformation("IsRepeatWanted()", behaviour.wantRepeat);
            return Task.FromResult(behaviour.wantRepeat);
        }

        public void NotifyFieldChange(Point point)
        {
            logger.LogInformation("NotifyFieldChange()");
        }

        public class AiPlayerBehaviour
        {
            public bool wantRepeat;
        }
    }
}
