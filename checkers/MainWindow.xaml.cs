﻿using System;
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
        private string turn;
        private CheckersAI ai;
        private Move currentMove;
        private string winner;
        private bool jumped;
        private bool tie;


        public MainWindow()
        {
            InitializeComponent();
            buildEmptyBoard();
        }

        public bool isJumped()
        {
            return jumped;
        }

        private void buildEmptyBoard()
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

        private void clearBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = getBoardField(board, r, c);
                    board.Children.Remove(stackPanel);
                }
            }
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
                            if (button.Name.Contains("Lady"))
                            { 
                                gameBoard.AssignPiece(row, col, Board.FieldState.RED_LADY);
                                gameBoard.increaseRedLadyCount();
                            }
                            else
                            {
                                gameBoard.AssignPiece(row, col, Board.FieldState.RED);
                                gameBoard.increaseRedCount();
                            }
                        }
                        else if (button.Name.Contains("Black"))
                        {
                            if (button.Name.Contains("Lady"))
                            {
                                gameBoard.AssignPiece(row, col, Board.FieldState.BLACK_LADY);
                                gameBoard.increaseBlackLadyCount();
                            }
                            else
                            {
                                gameBoard.AssignPiece(row, col, Board.FieldState.BLACK);
                                gameBoard.increaseBlackCount();
                            }
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
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Parent;
            int row = Grid.GetRow(stackPanel);
            int col = Grid.GetColumn(stackPanel);

            if (currentMove == null)
            {
                currentMove = new Move();
            }

            if(currentMove.CurrentCol == -1 && currentMove.CurrentRow == -1)
            {
                currentMove.CurrentRow = row;
                currentMove.CurrentCol = col;
                stackPanel.Background = Brushes.Green;
            }
            else
            {
                currentMove.RowToBeMovedTo = row;
                currentMove.ColToBeMovedTo = col;
                stackPanel.Background = Brushes.Green;
            }

            if((currentMove.CurrentCol != -1 && currentMove.CurrentRow != -1) && 
                (currentMove.ColToBeMovedTo != -1 && currentMove.RowToBeMovedTo != -1))
            {
                if(checkMove())
                {
                    makeMove();
                    aiMakeMove(ai);
                }
            }
        }

        private void twoAiPlay(CheckersAI p1, CheckersAI p2)
        {
            while(winner == null && !tie)
            {
                aiMakeMove(p1);
                aiMakeMove(p2);
            }

            gameSummary.Text = summary(p1, p2);
        }

        private string summary(CheckersAI p1, CheckersAI p2)
        {
            var summary = "Trees' depth = " + treeDepthBox.Text + "\n";

            if (winner.Equals(p1.getPlayer()))
            {
                summary += "Winner: " + p1.getPlayer() + " heuristic " + p1.getHeuristic() + "\n";

                if (p1.useMinMax())
                {
                    summary += " minmax made: ";
                }
                else
                {
                    summary += " alfa beta made: ";
                }

                summary += p1.getNumOfMoves() + "moves \n";
                summary += "Time " + p1.getTime() + "\n";

                summary += "Loser: " + p2.getPlayer() + " heuristic " + p2.getHeuristic() + "\n";

                if (p2.useMinMax())
                {
                    summary += " minmax made: ";
                }
                else
                {
                    summary += " alfa beta made: ";
                }

                summary += p2.getNumOfMoves() + "moves \n";
                summary += "Time " + p2.getTime() + "\n";


            }
            else
            {
                summary += "Winner: " + p2.getPlayer() + " heuristic " + p2.getHeuristic() + "\n";

                if (p2.useMinMax())
                {
                    summary += " minmax made: ";
                }
                else
                {
                    summary += " alfa beta made: ";
                }

                summary += p2.getNumOfMoves() + "moves \n";
                summary += "Time " + p2.getTime() + "\n";

                summary += "Loser: " + p1.getPlayer() + " heuristic " + p1.getHeuristic() + "\n";

                if (p1.useMinMax())
                {
                    summary += " minmax made: ";
                }
                else
                {
                    summary += " alfa beta made: ";
                }

                summary += p1.getNumOfMoves() + "moves \n";
                summary += "Time " + p1.getTime() + "\n";
            }

            return summary;
        }

        private void aiMakeMove(CheckersAI ai)
        {
            while(turn.Equals(ai.getPlayer()))
            {
                currentMove = ai.getAiMove(getCurrentBoard());
                if (currentMove != null)
                {
                    if (checkMove())
                    {
                        makeMove();
                    }
                }
            }
        }

        private bool checkMove()
        {
            StackPanel stackPanel1 = getBoardField(board, currentMove.CurrentRow, currentMove.CurrentCol);
            StackPanel stackPanel2 = getBoardField(board, currentMove.RowToBeMovedTo, currentMove.ColToBeMovedTo);
            Button button1 = (Button)stackPanel1.Children[0];
            Button button2 = (Button)stackPanel2.Children[0];
            stackPanel1.Background = Brushes.Gray;
            stackPanel2.Background = Brushes.Gray;

            if ((turn == "black") && (button1.Name.Contains("Red")))
            {
                currentMove.CurrentRow = -1;
                currentMove.CurrentCol = -1;
                currentMove.RowToBeMovedTo = -1;
                currentMove.ColToBeMovedTo = -1;
                displayError("It is blacks turn.");
                return false;
            }
            if ((turn == "red") && (button1.Name.Contains("Black")))
            {
                currentMove.CurrentRow = -1;
                currentMove.CurrentCol = -1;
                currentMove.RowToBeMovedTo = -1;
                currentMove.ColToBeMovedTo = -1;
                displayError("It is reds turn.");
                return false;
            }
            if (button1.Equals(button2))
            {
                currentMove.CurrentRow = -1;
                currentMove.CurrentCol = -1;
                currentMove.RowToBeMovedTo = -1;
                currentMove.ColToBeMovedTo = -1;
                return false;
            }
            if (button1.Name.Contains("Black"))
            {
                return checkMoveBlack(button1, button2);
            }
            else if (button1.Name.Contains("Red"))
            {
                return checkMoveRed(button1, button2);
            }
            else
            {
                currentMove.CurrentRow = -1;
                currentMove.CurrentCol = -1;
                currentMove.RowToBeMovedTo = -1;
                currentMove.ColToBeMovedTo = -1;
                return false;
            }
        }

        private bool checkMoveRed(Button button1, Button button2)
        {
            Board currentBoard = getCurrentBoard();
            List<Move> jumpMoves = currentBoard.getJumps("red");

            if (jumpMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in jumpMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    displayError("Jump must be taken");
                    currentMove.CurrentRow = -1;
                    currentMove.CurrentCol = -1;
                    currentMove.RowToBeMovedTo = -1;
                    currentMove.ColToBeMovedTo = -1;
                    return false;
                }
            }

            if (button1.Name.Contains("Red"))
            {
                if (button1.Name.Contains("Lady"))
                {
                    if ((currentMove.isAdjacent("lady")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        jumped = false;
                        return true;
                    }
                    Tuple<int,int> middlePiece = currentMove.checkJump("lady");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = getBoardField(board, middlePiece.Item1, middlePiece.Item2);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            board.Children.Remove(middleStackPanel);
                            addGrayButton(middlePiece);
                            jumped = true;
                            return true;
                        }
                    }
                }
                else
                {
                    if ((currentMove.isAdjacent("red")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        jumped = false;
                        return true;
                    }
                    Tuple<int, int> middlePiece = currentMove.checkJump("red");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = getBoardField(board, middlePiece.Item1, middlePiece.Item2);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            board.Children.Remove(middleStackPanel);
                            addGrayButton(middlePiece);
                            jumped = true;
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            displayError("Invalid Move. Try Again.");
            return false;
        }

        private bool checkMoveBlack(Button button1, Button button2)
        {
            Board currentBoard = getCurrentBoard();
            List<Move> jumpMoves = currentBoard.getJumps("black");

            if (jumpMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in jumpMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    displayError("Jump must be taken");
                    currentMove.CurrentRow = -1;
                    currentMove.CurrentCol = -1;
                    currentMove.RowToBeMovedTo = -1;
                    currentMove.ColToBeMovedTo = -1;
                    return false;
                }
            }

            if (button1.Name.Contains("Black"))
            {
                if (button1.Name.Contains("Lady"))
                {
                    if ((currentMove.isAdjacent("lady")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        jumped = false;
                        return true;
                    }
                    Tuple<int, int> middlePiece = currentMove.checkJump("lady");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = getBoardField(board, middlePiece.Item1, middlePiece.Item2);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Red"))
                        {
                            board.Children.Remove(middleStackPanel);
                            addGrayButton(middlePiece);
                            jumped = true;
                            return true;
                        }
                    }
                }
                else
                {
                    if ((currentMove.isAdjacent("black")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        jumped = false;
                        return true;
                    }
                    Tuple<int, int> middlePiece = currentMove.checkJump("black");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = getBoardField(board, middlePiece.Item1, middlePiece.Item2);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Red"))
                        {
                            board.Children.Remove(middleStackPanel);
                            addGrayButton(middlePiece);
                            jumped = true;
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            displayError("Invalid Move. Try Again.");
            return false;
        }

        private void addGrayButton(Tuple<int,int> middleMove)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = Brushes.Gray;
            Button button = new Button();
            button.Click += new RoutedEventHandler(piece_click);
            button.Height = 60;
            button.Width = 60;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = Brushes.Gray;
            button.Name = "button" + middleMove.Item1 + middleMove.Item2;
            stackPanel.Children.Add(button);
            Grid.SetColumn(stackPanel, middleMove.Item2);
            Grid.SetRow(stackPanel, middleMove.Item1);
            board.Children.Add(stackPanel);
        }

        private void makeMove()
        {
            if ((currentMove.CurrentCol != -1 && currentMove.CurrentRow != -1) &&
                (currentMove.ColToBeMovedTo != -1 && currentMove.RowToBeMovedTo != -1))
            {
                StackPanel stackPanel1 = getBoardField(board, currentMove.CurrentRow, currentMove.CurrentCol);
                StackPanel stackPanel2 = getBoardField(board, currentMove.RowToBeMovedTo, currentMove.ColToBeMovedTo);
                board.Children.Remove(stackPanel1);
                board.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, currentMove.RowToBeMovedTo);
                Grid.SetColumn(stackPanel1, currentMove.ColToBeMovedTo);
                board.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, currentMove.CurrentRow);
                Grid.SetColumn(stackPanel2, currentMove.CurrentCol);
                board.Children.Add(stackPanel2);
                checkLady(currentMove.RowToBeMovedTo, currentMove.ColToBeMovedTo);

                //handles multipe jumps
                if(jumped)
                {
                    var jumpMovesCount = getCurrentBoard().getJumpsFromLocation(turn, currentMove.RowToBeMovedTo, currentMove.ColToBeMovedTo).Count;
                    if (jumpMovesCount == 0)
                    {
                        jumped = false;
                        if (turn == "black")
                        {
                            this.Title = "Checkers! Reds turn!";
                            turn = "red";
                        }
                        else if (turn == "red")
                        {
                            this.Title = "Checkers! Blacks turn!";
                            turn = "black";
                        }
                    }
                }
                else
                {
                    jumped = false;
                    if (turn == "black")
                    {
                        this.Title = "Checkers! Reds turn!";
                        turn = "red";
                    }
                    else if (turn == "red")
                    {
                        this.Title = "Checkers! Blacks turn!";
                        turn = "black";
                    }
                }
                checkWin();
                
                if(winner == null)
                {
                    checkTie();
                }
                currentMove = null;
            }
        }


        private void checkWin()
        {
            int totalBlack = 0, totalRed = 0;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = getBoardField(board, r, c);
                    if (stackPanel.Children.Count > 0)
                    {
                        Button button = (Button)stackPanel.Children[0];
                        if (button.Name.Contains("Red"))
                            totalRed++;
                        if (button.Name.Contains("Black"))
                            totalBlack++;
                    }
                }
            }
            if (totalBlack == 0)
                winner = "red";
            if (totalRed == 0)
                winner = "black";

            if(winner != null)
            {
                this.Title = winner + " is the winner!";
                MessageBoxResult result = MessageBox.Show(winner + " is the winner!", "Winner", MessageBoxButton.OK);
                turn = "";
                if (result == MessageBoxResult.OK)
                {
                    clearBoard();
                    buildEmptyBoard();
                }
            }

        }

        private void checkTie()
        {
            if(winner == null)
            {
                Board gameBoard = getCurrentBoard();
                if (gameBoard.getAllLegalMovesForColor(turn).Count == 0)
                {
                    if (turn.Equals("red"))
                    {
                        turn = "black";
                    }
                    else
                    {
                        turn = "red";
                    }

                    if (gameBoard.getAllLegalMovesForColor(turn).Count == 0)
                    {
                        tie = true;
                    }
                }

                tie = false;

                if (tie)
                {
                    MessageBoxResult result = MessageBox.Show("Tie!", "Tie", MessageBoxButton.YesNo);
                }
            }
        }

        private void checkLady(int row, int col)
        {
            StackPanel stackPanel = getBoardField(board, row, col);
            if (stackPanel.Children.Count > 0)
            {
                Button button = (Button)stackPanel.Children[0];
                var redBrush = new ImageBrush();
                redBrush.ImageSource = new BitmapImage(new Uri("images/red_king.png", UriKind.Relative));
                var blackBrush = new ImageBrush();
                blackBrush.ImageSource = new BitmapImage(new Uri("images/black_king.png", UriKind.Relative));
                if ((button.Name.Contains("Black")) && (!button.Name.Contains("Lady")))
                {
                    if (row == 0)
                    {
                        button.Name = "button" + "Black" + "Lady" + row + col;
                        button.Background = blackBrush;
                    }
                }
                else if ((button.Name.Contains("Red")) && (!button.Name.Contains("Lady")))
                {
                    if (row == 7)
                    {
                        button.Name = "button" + "Red" + "Lady" + row + col;
                        button.Background = redBrush;
                    }
                }
            }
        }

        private void displayError(string error)
        {
            MessageBox.Show(error, "Invalid Move", MessageBoxButton.OK);
        }

        private void startNewGame(object sender, RoutedEventArgs e)
        {
            clearBoard();
            buildBoard();
            turn = "black";
            currentMove = null;
            winner = null;

            int treeDepth = int.Parse(treeDepthBox.Text);

            if(minmaxVSPlayer.IsChecked == true)
            {
                if(firstH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 1,true);
                }

                if(secondH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 2,true);
                }

                if(thirdH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 3,true);
                }
            }

            if(minmaxVSMinmax.IsChecked == true)
            {
                CheckersAI ai2 = null;

                if (firstH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 1, true);
                }

                if (secondH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 2, true);
                }

                if (thirdH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 3, true);
                }
                
                if(firstHeuristic.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 1,true);
                }

                if (secondHeuristic.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 2, true);
                }

                if (thirdHeuristic.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 3, true);
                }

                twoAiPlay(ai2, ai);
            }

            if(minmaxVSAlfabeta.IsChecked == true)
            {
                CheckersAI ai2 = null;
                CheckersAI alphabeta1 = null;

                if (firstH.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 1,true);
                }

                if (secondH.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 2, true);
                }

                if (thirdH.IsChecked == true)
                {
                    ai2 = new CheckersAI("black", treeDepth, 1, true);
                }

                if (firstHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 1,false);
                }

                if (secondHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 2, false);
                }

                if (thirdHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 3, false);
                }

                twoAiPlay(ai2, alphabeta1);
            }

            if(alfabetaVSPlayer.IsChecked == true)
            {
                if (firstH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 1,false);
                }

                if (secondH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 2, false);
                }

                if (thirdH.IsChecked == true)
                {
                    ai = new CheckersAI("red", treeDepth, 3, false);
                }

            }

            if(alfabetaVSAlfabeta.IsChecked == true)
            {
                CheckersAI alphabeta2 = null;
                CheckersAI alphabeta1 = null;

                if (firstH.IsChecked == true)
                {
                    alphabeta2 = new CheckersAI("black", treeDepth, 1, false);
                }

                if (secondH.IsChecked == true)
                {
                    alphabeta2 = new CheckersAI("black", treeDepth, 2, false);
                }

                if (thirdH.IsChecked == true)
                {
                    alphabeta2 = new CheckersAI("black", treeDepth, 1, false);
                }

                if (firstHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 1, false);
                }

                if (secondHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 2, false);
                }

                if (thirdHeuristic.IsChecked == true)
                {
                    alphabeta1 = new CheckersAI("red", treeDepth, 3, false);
                }


                twoAiPlay(alphabeta2, alphabeta1);
            }
        }
    }
}
