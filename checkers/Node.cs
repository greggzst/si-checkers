using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Node
    {
        private Board board;
        private Move move;
        private int score;
        private List<Node> children;

        public Node(Board board, Move move, int score, params Node[] children)
        {
            this.board = board;
            this.move = move;
            this.score = score;
            this.children = children.ToList();
        }

        public Board getBoard()
        {
            return board;
        }

        public Move getMove()
        {
            return move;
        }

        public int getScore()
        {
            return score;
        }

        public void setScore(int value)
        {
            score = value;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public int getNumChildren()
        {
            return children.Count;
        }

        public void addChild(Node child)
        {
            children.Add(child);
        }

        public Node getChild(int index)
        {
            return children.ElementAt(index);
        }
    }
}
