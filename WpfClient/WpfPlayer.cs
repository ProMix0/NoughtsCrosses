using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CrossesZeroes.Abstractions;
using CrossesZeroes.Common;

namespace WpfClient
{
    public class WpfPlayer : IRealPlayer
    {
        private IWpfView? client;
        Thread windowThread;

        protected Button[,]? btnMatr = null;
        protected TaskCompletionSource<Button>? btnCompletionSource = null;

        public WpfPlayer()
        {
            // Create a thread
            windowThread = new(new ThreadStart(() =>
            {
                // Create and show the Window
                WpfView view = new();
                client = view;

                Window tempWindow = view;

                tempWindow.Show();

                tempWindow.Closed += (_, _) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                Dispatcher.Run();
            }));
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.IsBackground = true;
            windowThread.Start();


            while (client == null) Thread.Sleep(10);

            Console.WriteLine("Client isn't null");
        }

        public void ReportEnd(bool victory, ICrossesZeroesField field)
        {
            Dispatcher.FromThread(windowThread).Invoke(() =>
                client!.StateLine.Text = $"You {(victory ? "win" : "lose")}!");
            PrintField(field);
        }

        public void Init(CellState mark)
        {
            string markStr = mark switch
            {
                CellState.Cross => "cross",
                CellState.Zero => "zero",
                _ => ""
            };

            Dispatcher.FromThread(windowThread).Invoke(() =>
            client!.StateLine.Text = $"You are {markStr}");

            if (btnMatr != null)
                Dispatcher.FromThread(windowThread).Invoke(() =>
                {
                    for (int i = 0; i < btnMatr!.GetLength(0); i++)
                        for (int j = 0; j < btnMatr!.GetLength(1); j++)
                            btnMatr![i, j].Content = ' ';
                });
        }

        public async Task<CrossesZeroes.Common.Point> Turn(ICrossesZeroesField field)
        {
            EnsureGridSize(field.Height, field.Width);
            PrintField(field);

            btnCompletionSource = new();
            Button button = await btnCompletionSource.Task;
            btnCompletionSource = null;

            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (btnMatr![i, j] == button)
                        return new(i, j);

            throw new Exception("Button outside field");
        }

        private void PrintField(ICrossesZeroesField field)
        {
            Dispatcher.FromThread(windowThread).Invoke(() =>
            {
                for (int i = 0; i < field.Height; i++)
                    for (int j = 0; j < field.Width; j++)
                        btnMatr![i, j].Content = field[i, j] switch
                        {
                            CellState.Cross => 'X',
                            CellState.Zero => '0',
                            _ => ' '
                        };
            });
        }

        protected void EnsureGridSize(int height, int width)
        {
            if (btnMatr != null && height == btnMatr!.GetLength(0) && width == btnMatr!.GetLength(1)) return;

            Dispatcher.FromThread(windowThread).Invoke(() =>
            {
                Grid grid = client!.Field;
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
            });
        }

        protected void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (btnCompletionSource == null) return;

            btnCompletionSource.TrySetResult((sender as Button)!);
        }

        public Task<bool> IsRepeatWanted()
        {
            TaskCompletionSource<bool> completion = new();

            Dispatcher.FromThread(windowThread).InvokeAsync(() =>
            {
                Window dialog = new RepeatDialog();
                dialog.Owner = client as Window;

                bool value = dialog.ShowDialog()!.Value;
                completion.SetResult(value);
            });

            return completion.Task;
        }
    }
}
