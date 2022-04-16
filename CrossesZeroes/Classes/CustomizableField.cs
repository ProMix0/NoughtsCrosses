using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Classes
{
    class CustomizableField : ICrossesZeroesField
    {

        public CustomizableField(IOptions<Configuration> config)
            :this(config.Value.Height,config.Value.Width,config.Value.WinSequenceLength)
        {}

        public CustomizableField(int height, int width, int winSequenceLength)
        {
            Width = width > 0 ? width : throw new ArgumentException("Must be more than zero", nameof(width));
            Height = height > 0 ? height : throw new ArgumentException("Must be more than zero", nameof(height));
            this.winSequenceLength = winSequenceLength > 0 ? winSequenceLength : throw new ArgumentException("Must be more than zero", nameof(winSequenceLength));

            field = new CellState[height, width];
            readonlyField = new(this);
        }

        public CellState this[int i, int j] => field[i,j];
        private CellState[,] field;

        public int Width { get; }

        public int Height { get; }

        protected int winSequenceLength;

        private ReadonlyField readonlyField;
        public ICrossesZeroesField AsReadonly() => readonlyField;

        public void Clear()
        {
            field = new CellState[Height, Width];
        }

        //TODO
        public bool IsEndGame(out CellState winner)
        {
            throw new NotImplementedException();
        }

        public void Set(Point point, CellState markType)
        {
            if (markType == CellState.Empty) throw new ArgumentException("", nameof(markType));
            if (field[point.x, point.y] != CellState.Empty) throw new ArgumentException("", nameof(point));

            field[point.x, point.y] = markType;
        }

        public class Configuration
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int WinSequenceLength { get; set; }
        }
    }
}
