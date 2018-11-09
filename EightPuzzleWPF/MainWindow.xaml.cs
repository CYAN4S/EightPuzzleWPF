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
        string minsec = string.Empty;
        string milli = string.Empty;

        public static int movedTime = 0;

        public MainWindow()
        {
            InitializeComponent();

            boardGame = new BoardGame(3, 3);
            InitBoard();
            ShowBoard();

            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan timeSpan = stopwatch.Elapsed;
                minsec = String.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
                milli = String.Format("{0:000}", timeSpan.Milliseconds);
                MinuteSecondText.Document.Blocks.Clear();
                MinuteSecondText.Document.Blocks.Add(new Paragraph(new Run(minsec)));
                MillisecondText.Document.Blocks.Clear();
                MillisecondText.Document.Blocks.Add(new Paragraph(new Run(milli)));
            }
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
                        FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "/EightPuzzleWPF;component/Fonts/#Roboto Mono Light"),
                        Foreground = Brushes.Gray,
                        Background = Brushes.White,
                        BorderThickness = new Thickness(0),
                        Tag = i * colSize + j
                    };
                    btn.Click += Button_Click_Tile;

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
            {
                ShowBoard();
                MovedTimeText.Document.Blocks.Clear();
                MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
            }
        }

        private void Button_Click_Tile(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            boardGame.MoveTileWithMouse(tag);
            if (Board.IsSolved(boardGame) && stopwatch.IsRunning)
                stopwatch.Stop();
            ShowBoard();
            MovedTimeText.Document.Blocks.Clear();
            MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));

        }

        private void Button_Click_Center(object sender, RoutedEventArgs e)
        {
            if (Board.IsSolved(boardGame))
            {
                for (int i = 0; i < 250; i++)
                {
                    boardGame.MoveOnceRandom();
                    ShowBoard();
                    Delay(3);
                }
                movedTime = 0;
                MovedTimeText.Document.Blocks.Clear();
                MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
                stopwatch.Reset();
                stopwatch.Start();
                dispatcherTimer.Start();
            }
            else
            {
                List<Key> paths = boardGame.Solve();
                foreach (var i in paths)
                {
                    boardGame.MoveTile(i);
                    ShowBoard();
                    MovedTimeText.Document.Blocks.Clear();
                    MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
                    Delay(100);
                }
                stopwatch.Stop();
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
        
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = (MenuGrid.Visibility == Visibility.Hidden) ? Visibility.Visible : Visibility.Hidden;
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            boardGame.ResetTiles();
            stopwatch.Stop();
            movedTime = 0;
            MovedTimeText.Document.Blocks.Clear();
            MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
            ShowBoard();
        }

        private void ResizeButton(object sender, RoutedEventArgs e)
        {
            RowCombo.SelectedIndex = 1;
            ColCombo.SelectedIndex = 1;
            MenuGrid.Visibility = Visibility.Hidden;
            BoardSpace.Visibility = Visibility.Hidden;
            ResizeGrid.Visibility = Visibility.Visible;
        }

        private void ShuffleButton(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            for (int i = 0; i < 250; i++)
            {
                boardGame.MoveOnceRandom();
                ShowBoard();
                Delay(3);
            }
            movedTime = 0;
            MovedTimeText.Document.Blocks.Clear();
            MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
            stopwatch.Reset();
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        private void ResetButton(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            boardGame.ResetTiles();
            stopwatch.Stop();
            movedTime = 0;
            MovedTimeText.Document.Blocks.Clear();
            MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
            ShowBoard();
        }

        private void SolveButton(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            List<Key> paths = boardGame.Solve();
            foreach (var i in paths)
            {
                boardGame.MoveTile(i);
                ShowBoard();
                movedTime++;
                MovedTimeText.Document.Blocks.Clear();
                MovedTimeText.Document.Blocks.Add(new Paragraph(new Run(Convert.ToString(movedTime))));
                Delay(100);
            }
            stopwatch.Stop();
            ShowBoard();
        }

        private void ResizeCancel(object sender, RoutedEventArgs e)
        {
            ResizeGrid.Visibility = Visibility.Hidden;
            BoardSpace.Visibility = Visibility.Visible;
        }

        private void ResizeOK(object sender, RoutedEventArgs e)
        {
            ResizeBoard(RowCombo.SelectedIndex + 2, ColCombo.SelectedIndex + 2);
            ResizeGrid.Visibility = Visibility.Hidden;
            BoardSpace.Visibility = Visibility.Visible;
        }
    }
}
