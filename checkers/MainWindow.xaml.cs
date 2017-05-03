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

namespace checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            buildBoard();

        }

        private void buildBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    StackPanel boardField = new StackPanel();

                    if ((j % 2 == 1 && i % 2 == 1) || (j % 2 == 0 && i % 2 == 0))
                    {
                        boardField.Background = Brushes.White;
                    }
                    else
                    {
                        boardField.Background = Brushes.Gray;
                    }

                    board.Children.Add(boardField);
                    Grid.SetRow(boardField, i);
                    Grid.SetColumn(boardField, j);
                }
            }

            insertPiecesOnBoard();
        }

        private StackPanel getBoardField(Grid board, int row, int col)
        {
            for (int i = 0; i < board.Children.Count; i++)
            {
                StackPanel field = (StackPanel)board.Children[i];
                if (Grid.GetRow(field) == row && Grid.GetColumn(field) == col)
                    return field;
            }
            return null;
        }

        private void insertPiecesOnBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    StackPanel stackPanel = getBoardField(board, row, col);
                    Button button = new Button();
                    button.Click += new RoutedEventHandler(piece_click);
                    button.Height = 50;
                    button.Width = 50;
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;
                    var redBrush = new ImageBrush();
                    redBrush.ImageSource = new BitmapImage(new Uri("images/red.png", UriKind.Relative));
                    var blackBrush = new ImageBrush();
                    blackBrush.ImageSource = new BitmapImage(new Uri("images/black.png", UriKind.Relative));

                    switch (row)
                    {
                        case 0:
                            if (col % 2 == 1)
                            {

                                button.Background = redBrush;
                                button.Name = "buttonRed" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 1:
                            if (col % 2 == 0)
                            {
                                button.Background = redBrush;
                                button.Name = "buttonRed" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 2:
                            if (col % 2 == 1)
                            {
                                button.Background = redBrush;
                                button.Name = "buttonRed" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 3:
                            if (col % 2 == 0)
                            {
                                button.Background = Brushes.Gray;
                                button.Name = "button" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 4:
                            if (col % 2 == 1)
                            {
                                button.Background = Brushes.Gray;
                                button.Name = "button" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 5:
                            if (col % 2 == 0)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 6:
                            if (col % 2 == 1)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 7:
                            if (col % 2 == 0)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + row + col;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Board getCurrentBoard()
        {
            Board gameBoard = new Board();
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    StackPanel boardField = getBoardField(board,row, col);
                    if(boardField.Children.Count > 0)
                    {
                        Button button = (Button)boardField.Children[0];
                        if(button.Name.Contains("Red"))
                        {
                            if (button.Name.Contains("King"))
                                gameBoard.AssignPiece(row, col, Board.FieldState.RED_KING);
                            else
                                gameBoard.AssignPiece(row, col, Board.FieldState.RED);
                        }
                        else if (button.Name.Contains("Black"))
                        {
                            if (button.Name.Contains("King"))
                                gameBoard.AssignPiece(row, col, Board.FieldState.BLACK_KING);
                            else
                                gameBoard.AssignPiece(row, col, Board.FieldState.BLACK);
                        }
                        else
                        {
                            gameBoard.AssignPiece(row, col, Board.FieldState.EMPTY);
                        }
                    }
                    else
                    {
                        gameBoard.AssignPiece(row, col, Board.FieldState.INVALID);
                    }
                }
            }
            return gameBoard;
        }

        public void piece_click(Object sender, RoutedEventArgs e)
        {
        }
    }
}
