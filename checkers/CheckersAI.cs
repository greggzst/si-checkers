using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class CheckersAI
    {
        private Node decisionTree;
        private Move move;
        private string colour;
        private string opponent;

        public CheckersAI(string colour)
        {
            this.colour = colour;
            if (colour.Equals("red"))
            {
                opponent = "black";
            }
            else
            {
                opponent = "red";
            }
        }

        public Move getAiMove(Board board)
        {
            decisionTree = buildTree(board);
            move = pickMove();
            return move;
        }

        private Node buildTree(Board board)
        {
            Node root = new Node(board, null, score(board));

            List<Move> moves = board.getAllLegalMovesForColor(colour);

            foreach (var move in moves)
            {
                Board copy = copyBoard(board);
                copy.makeMove(move, colour);
                Node firstLayer = new Node(copy, move, score(copy));
                List<Move> opponentMoves = copy.getAllLegalMovesForColor(opponent);

                foreach (var omove in opponentMoves)
                {
                    Board copy2 = copyBoard(copy);
                    copy2.makeMove(omove, opponent);
                    firstLayer.addChild(new Node(copy2, omove, score(copy2)));
                }
                root.addChild(firstLayer);
            }

            return root;
        }

        private Move pickMove()
        {
            int max = int.MinValue;
            int index = 0;

            for(int i = 0; i < decisionTree.getNumChildren(); i++)
            {
                Node child = decisionTree.getChild(i);
                int smin = int.MaxValue;

                foreach (var sChild in child.getChildren())
                {
                    if(sChild.getScore() <= smin)
                    {
                        smin = sChild.getScore();
                    }
                }
                child.setScore(smin);

                if(child.getScore() >= max)
                {
                    max = child.getScore();
                    index = i;
                }
            }

            return decisionTree.getChild(index).getMove();

        }

        private int score(Board board)
        {
            if (colour.Equals("red"))
            {
                return board.getRedWeightedScore() - board.getBlackWeightedScore();
            }
            else
            {
                return board.getRedWeightedScore() - board.getRedWeightedScore();
            }
        }

        private Board copyBoard(Board board)
        {
            Board copy = new Board();

            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    copy.AssignPiece(row, col, board.GetPiece(row, col));
                }
            }

            return copy;
        }


    }
}
