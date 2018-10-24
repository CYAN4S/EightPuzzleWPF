using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace EightPuzzleWPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardGame boardGame;
        List<List<Button>> buttons;
        Stopwatch stopwatch  = new Stopwatch();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        string currentTime = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            boardGame = new BoardGame(3, 3);
            InitBoard();
            // ShowBoard(boardGame);
            ResizeBoard(3, 3);

            
        }

        private void ResizeBoard(int r, int c)
        {
            boardGame = new BoardGame(r, c);
            buttons.Clear();
            BoardSpace.Children.Clear();
            BoardSpace.RowDefinitions.Clear();
            BoardSpace.ColumnDefinitions.Clear();
            InitBoard();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                clocktxt.Text = currentTime;
            }
        }

        // 처음 생성, 판 크기 변경 시
        private void InitBoard()
        {
            int rowSize = boardGame.Status.Count;
            int colSize = boardGame.Status[0].Count;
            buttons = new List<List<Button>>();

            for (int i = 0; i < rowSize; i++)
                BoardSpace.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < colSize; i++)
                BoardSpace.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < rowSize; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < colSize; j++)
                {
                    Button btn = new Button
                    {
                        Content = boardGame.Status[i][j].ToString(),
                        FontSize = 40,
                        FontFamily = new FontFamily("Roboto Mono Light"),
                        Foreground = Brushes.Gray,
                        Background = Brushes.White,
                        BorderThickness = new Thickness(0),
                        Tag = i * colSize + j
                    };
                    btn.Click += Button_Click;

                    BoardSpace.Children.Add(btn);
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    buttons[i].Add(btn);
                }
            }
            buttons[boardGame.HoleRow][boardGame.HoleCol].Visibility = Visibility.Hidden;
        }

        private void ShowBoard()
        {
            int rowSize = boardGame.Status.Count;
            int colSize = boardGame.Status[0].Count;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    buttons[i][j].Content = boardGame.Status[i][j].ToString();
                    buttons[i][j].Visibility = Visibility.Visible;
                }
            }
            buttons[boardGame.HoleRow][boardGame.HoleCol].Visibility = Visibility.Hidden;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (boardGame.MoveTile(e.Key))
                ShowBoard();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            //buttons[0][0].Content = tag.ToString();
            boardGame.MoveTileWithMouse(tag);
            if (boardGame.IsSolved() && stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
            ShowBoard();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (boardGame.IsSolved())
            {
                for (int i = 0; i < 250; i++)
                {
                    boardGame.MoveOnceRandom();
                    ShowBoard();
                    Delay(3);
                }

                stopwatch.Start();
            }
            else
            {
                // boardGame.ResetTiles();
                List<Key> paths = boardGame.Solve();
                foreach (var i in paths)
                {
                    boardGame.MoveTile(i);
                    ShowBoard();
                    Delay(100);
                }
                ShowBoard();
            }
            
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;

        }
        
    }
}
