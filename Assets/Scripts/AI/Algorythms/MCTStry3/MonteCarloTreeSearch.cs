using System.Linq;
namespace DefaultNamespace.AI.Algorythms.MCTStry3
{
    public class MonteCarloTreeSearch
    {
        public MonteCarloTreeNode Root { get; }

        public MonteCarloTreeSearch(MonteCarloTreeNode root)
        {
            Root = root;
        }

        public void Simulate()
        {
            Root.SimulateMove();

            if (Root.Board.IsGameOver())
            {
                double score = CalculateGameOutcome();
                Root.Backpropagate(score);
            }
            else
            {
                double score = CalculateScore();
                Root.Backpropagate(score);
            }
        }

        private double CalculateGameOutcome()
        {
            // Implement your game outcome scoring logic
            // Return a positive value if active player wins, negative if opponent wins, and zero for a draw
            return 0.0;
        }

        public void RunIterations(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                MonteCarloTreeNode selectedNode = Select();
                MonteCarloTreeNode expandedNode = selectedNode.Expand();
                Simulate();
                expandedNode.Backpropagate(0); // Use the appropriate score from the simulation
            }
        }

        private MonteCarloTreeNode Select()
        {
            MonteCarloTreeNode currentNode = Root;

            while (currentNode.Children.Any())
            {
                currentNode = currentNode.SelectChild();
            }

            return currentNode;
        }

        private double CalculateScore()
        {
            return 0.0; // Placeholder for score calculation logic
        }
    }
}
