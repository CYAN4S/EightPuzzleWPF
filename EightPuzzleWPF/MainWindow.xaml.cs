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

namespace EightPuzzleWPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardGame boardGame;
        List<List<Button>> buttons;

        public MainWindow()
        {
            InitializeComponent();

            boardGame = new BoardGame(3, 3);

            InitBoard();
            // ShowBoard(boardGame);

            ResizeBoard(4, 4);
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

        // 처음 생성, 판 크기 변경 시
        private void InitBoard()
        {
            int rowSize = boardGame.Status.Count;
            int colSize = boardGame.Status[0].Count;
            buttons = new List<List<Button>>();

            for (int i = 0; i < rowSize; i++)
            {
                BoardSpace.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < colSize; i++)
            {
                BoardSpace.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowSize; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < colSize; j++)
                {
                    Button btn = new Button
                    {
                        Content = boardGame.Status[i][j].ToString(),
                        FontSize = 48,
                        FontFamily = new FontFamily("Roboto Mono Light"),
                        Foreground = Brushes.Gray,
                        Background = Brushes.White,
                        BorderBrush = Brushes.White,
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
            


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            boardGame.MoveTile(e.Key);

            //if (e.Key == Key.Up)
            //{

            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            buttons[0][0].Content = tag.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            boardGame.ShuffleTiles();
        }
    }
}
