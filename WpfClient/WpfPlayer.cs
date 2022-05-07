using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;

namespace WpfClient
{
    public class WpfPlayer : IRealPlayer
    {
        private readonly WpfClient client;

        protected int width = 0, height = 0;
        protected Button[,]? btnMatr = null;
        protected TaskCompletionSource<Button>? btnCompletionSource = null;

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

            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    btnMatr![i, j].Content = field[i, j] switch
                    {
                        CellState.Cross => 'X',
                        CellState.Zero => '0',
                        _ => ' '
                    };

            btnCompletionSource = new();
            Button button = btnCompletionSource.Task.Result;

            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (btnMatr![i, j] == button)
                        return new(i, j);

            throw new Exception("Button outside field");
        }

        protected void EnsureGridSize(int height, int width)
        {
            if (height == this.height && width == this.width) return;

            Grid grid = new();
            client.field = grid;
            btnMatr = new Button[height, width];

            for (int i = 0; i < height; i++)
                grid.RowDefinitions.Add(new());
            for (int i = 0; i < width; i++)
                grid.ColumnDefinitions.Add(new());

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    Button button = new();

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    button.Click += ButtonClick;

                    btnMatr[i, j] = button;
                    grid.Children.Add(button);
                }
        }

        protected void ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (btnCompletionSource == null) return;

            btnCompletionSource.SetResult((sender as Button)!);
        }
    }
}
