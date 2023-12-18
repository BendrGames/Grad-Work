using Game.Simulation;
using System;
using System.Linq;
namespace DefaultNamespace.AI.Algorythms.MCTS
{
    public class MCTS
    {
        private Node root; // The root of the Monte Carlo Tree

        public /*Tuple<PieceSimulation, PieceSimulation> */ void FindBestMove(BoardSimulation board, int depth)
        {
            root = new Node(board);

            for (int i = 0; i < depth; i++)
            {
                Node selectedNode = Selection(root);

                // Expansion
                Node expandedNode = Expansion(selectedNode);

                // Simulation
                float simulationResult = Simulation(expandedNode);

                // Backpropagation
                Backpropagation(expandedNode, simulationResult);
            }
            
            var bestChild = root.children.OrderByDescending(c => c.visits).FirstOrDefault();

        }
        
        Node Selection(Node node)
        {
            // TODO: Implement selection logic to choose a node from the tree based on a selection policy
            // Common policies include UCT (Upper Confidence Bound for Trees) or others.
            return node;
        }

        Node Expansion(Node node)
        {
            // TODO: Implement expansion logic to add a child node to the given node
            // Expansion typically involves creating a new node representing a possible move and adding it to the tree.
            return node;
        }

        float Simulation(Node node)
        {
            // TODO: Implement simulation logic to estimate the value of the given node
            // Simulation is often a random playout or a simple heuristic evaluation.
            return 0.0f; // Placeholder value, replace with actual simulation result
        }

        void Backpropagation(Node node, float result)
        {
            // TODO: Implement backpropagation logic to update the statistics of nodes along the path
            // Update visit count and win count based on the simulation result
            while (node != null)
            {
                node.visits++;
                node.wins += result;
                node = node.parent; // Move up the tree
            }
        }
      
    }
}
