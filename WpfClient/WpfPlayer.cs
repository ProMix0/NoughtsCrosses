using System;
using System.Windows.Controls;
using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;

namespace WpfClient
{
    public class WpfPlayer : IRealPlayer
    {
        private readonly WpfClient client;

        protected int width=0, height=0;

        public WpfPlayer(WpfClient client)
        {
            this.client = client;
        }

        public void ReportEnd(bool victory)
        {
            client.stateLine.Text = $"You {(victory ? "win" : "lose")}!";
        }

        public void SetMark(CellState mark)
        {
            string markStr = mark switch
            {
                CellState.Cross => "cross",
                CellState.Zero => "zero",
                _ => ""
            };

            client.stateLine.Text = $"You are {markStr}";
        }

        public Point Turn(ICrossesZeroesField field)
        {
            EnsureGridSize(field.Height, field.Width);
        }

        protected void EnsureGridSize(int height,int width)
        {
            if (height == this.height && width == this.width) return;

            Grid grid = new();
            client.field = grid;

            for (int i = 0; i < height; i++)
                grid.RowDefinitions.Add(new());
            for (int i = 0; i < width; i++)
                grid.ColumnDefinitions.Add(new());


        }
    }
}
