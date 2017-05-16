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
            List<Move> jumpMoves = root.getBoard().getJumps(colour);
            if (jumpMoves.Count > 0)
            {
                moves = jumpMoves;
            }
            else
            {
                moves = root.getBoard().getAllLegalMovesForColor(colour);
            }

            foreach (var move in moves)
            {
                Board copy = copyBoard(root.getBoard());
                copy.makeMove(move, colour);
                Node child = new Node(copy, move, score(copy));
                buildTree(child, depth - 1, !player);
                root.addChild(child);
            }


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

            return copy;
        }


    }
}
