using Game.Simulation;
using System.Collections.Generic;

namespace DefaultNamespace.AI.Algorythms.MCTS
{
    public class Node
    {
        public int visits;
        public float wins;
        public List<Node> children;
        public BoardSimulation Board;
        public Node parent;

        public Node(BoardSimulation Board)
        {
            this.Board = Board;
            this.visits = 0;
            this.wins = 0;
            this.children = new List<Node>();
        }
    }
}
