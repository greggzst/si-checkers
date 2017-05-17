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
        private string me;
        private string opponent;

        public CheckersAI(string colour)
        {
            this.me = colour;
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
            buildTree(root, 4, true);
            return root;
        }

        private void buildTree(Node root, int depth, bool player)
        {
            if(depth == 0)
            {
                return;
            }

            string colour = player ? me : opponent; 

            List<Move> moves;
            if (root.getBoard().isJumped())
            {
                var lastMove = root.getMove();
                moves = root.getBoard().getJumpsFromLocation(colour,lastMove.RowToBeMovedTo,lastMove.ColToBeMovedTo);
            }
            else if(root.getBoard().getJumps(colour).Count > 0)
            {
                moves = root.getBoard().getJumps(colour);
            }
            else
            {
                moves = root.getBoard().getAllLegalMovesForColor(colour);
            }

            foreach (var move in moves)
            {
                Board copy = copyBoard(root.getBoard());
                copy.makeMove(move, colour);
                copy.handleJump(move, colour);
                Node child = new Node(copy, move, score(copy));
                buildTree(child, depth - 1, !player);
                root.addChild(child);
            }


        }

        private Move pickMove()
        {
            minmax(decisionTree, 4, true);

            int max = int.MinValue;
            int index = 0;
            for(int i = 0; i < decisionTree.getNumChildren(); i++)
            {
                Node child = decisionTree.getChild(i);
                if(child.getScore() >= max)
                {
                    max = child.getScore();
                    index = i;
                }
            }

            return decisionTree.getChild(index).getMove();
        }

        private int minmax(Node root, int depth, bool maxPlayer)
        {
            if (depth == 0 || root.getNumChildren() == 0)
                return root.getScore();

            if(maxPlayer)
            {
                int bestValue = int.MinValue;
                foreach (var child in root.getChildren())
                {
                    int val = minmax(child, depth - 1, false);
                    bestValue = Math.Max(bestValue, val);
                }
                root.setScore(bestValue);
                return bestValue;
            }
            else
            {
                int bestValue = int.MaxValue;
                foreach (var child in root.getChildren())
                {
                    int val = minmax(child, depth - 1, true);
                    bestValue = Math.Min(bestValue, val);
                }
                root.setScore(bestValue);
                return bestValue;
            }
        }

        private int score(Board board)
        {
            if (me.Equals("red"))
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

            copy.setBlackCount(board.getBlackCount());
            copy.setBlackLadyCount(board.getBlackLadyCount());
            copy.setRedCount(board.getRedCount());
            copy.setRedLadyCount(board.getRedLadyCount());

            return copy;
        }


    }
}
