using Game.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
namespace DefaultNamespace.AI.Algorythms
{
    public class oldMCTS
    {
        private int simulationsPerMove;

        public oldMCTS(int simulationsPerMove)
        {
            this.simulationsPerMove = simulationsPerMove;
        }

        public Tuple<PieceSimulation, PieceSimulation> FindBestMove(BoardSimulation board)
        {
            oldMCTSNode root = new(board);

            for (int i = 0; i < simulationsPerMove; i++)
            {
                oldMCTSNode selectedNode = Selection(root);
                oldMCTSNode expandedNode = Expansion(selectedNode);

                if (expandedNode == null)
                {
                    // Log a message or throw an exception if expandedNode is null
                    Debug.Log("Expanded node is null in FindBestMove loop.");
                }

                int simulationResult = Simulation(expandedNode);
                Backpropagation(expandedNode, simulationResult);
            }

            // Select the best move based on the highest number of visits
            oldMCTSNode bestChild = root.Children.OrderByDescending(c => c.Visits).FirstOrDefault();

            if (bestChild == null)
            {
                // Log a message or throw an exception if bestChild is null
                Debug.Log("Best child is null in FindBestMove method.");
                return null;
            }

            return bestChild != null ? new Tuple<PieceSimulation, PieceSimulation>(bestChild.Move.Item1, bestChild.Move.Item2) : null;
        }

        private oldMCTSNode Selection(oldMCTSNode node)
        {
            while (!node.IsTerminal && node.IsFullyExpanded)
            {
                node = UCTSelectChild(node);
                Debug.Log($"Selected node: Visits - {node.Visits}, TotalScore - {node.TotalScore}");
            }

            return node;
        }

        private oldMCTSNode Expansion(oldMCTSNode node)
        {
            if (!node.IsTerminal && !node.IsFullyExpanded)
            {
                Tuple<PieceSimulation, PieceSimulation> possibleMove = GetUntriedMove(node);

                if (possibleMove != null)
                {
                    node.Expand(possibleMove);

                    // Log the number of children after expansion
                    Debug.Log($"Expanded node. Children count: {node.Children.Count}");
                }
                else
                {
                    oldMCTSNode bestChild = node.Children.OrderByDescending(c => Evaluate(c.Board)).FirstOrDefault();
                    return bestChild;
                }
            }

            // Select a child node for simulation
            oldMCTSNode expandedNode = UCTSelectChild(node);

            if (expandedNode == null)
            {
                // Log a message or throw an exception if expandedNode is null
                Debug.Log("Expanded node is null in Expansion method.");
            }

            return expandedNode;
        }
        
        

        private int Simulation(oldMCTSNode node)
        {
            // Perform alternating turn simulation from the current state
            BoardSimulation simulatedBoard = new(node.Board);
            AlternatingSimulation(simulatedBoard);
            int result = Evaluate(simulatedBoard);

            Debug.Log($"Simulation result: {result}");
            
            return result;
            // Evaluate the simulated board and return a result
        }

        private Random random = new();

        private void AlternatingSimulation(BoardSimulation board)
        {
            while (!board.IsGameOver())
            {
                List<PieceSimulation> activePieces = board.IsPlayerTurn() ? board.PlayerPieces : board.AIPieces;

                if (activePieces.Count > 0)
                {
                    PieceSimulation randomAttacker = activePieces[random.Next(0, activePieces.Count)];
                    List<PieceSimulation> targetPieces = board.IsPlayerTurn() ? board.AIPieces : board.PlayerPieces;
                    PieceSimulation randomTarget = targetPieces[random.Next(0, targetPieces.Count)];

                    board.SimulateMove(randomAttacker, randomTarget);
                    board.SwitchTurn();
                }
            }
        }

        private void Backpropagation(oldMCTSNode node, int result)
        {
            while (node != null)
            {
                Debug.Log($"Backpropagation: Visits - {node.Visits}, TotalScore - {node.TotalScore}");
                node.Visits += 1;  // Increment by the total number of visits made during simulation
                node.TotalScore += result;
                node = node.Parent;
            }
        }

        private oldMCTSNode UCTSelectChild(oldMCTSNode node)
        {
            if (node.Children.Count == 0)
            {
                // Handle the case when there are no children (e.g., return a default node)
                return null;
            }

            // Select a child node using the UCT formula
            return node.Children.OrderByDescending(c => UCTValue(node, c)).FirstOrDefault();
        }

        private double UCTValue(oldMCTSNode parent, oldMCTSNode child)
        {
            const double explorationWeight = 1.4f;
            return (child.TotalScore / child.Visits) +
                explorationWeight * Math.Sqrt(Math.Log(parent.Visits) / child.Visits);
        }

        private Tuple<PieceSimulation, PieceSimulation> GetUntriedMove(oldMCTSNode node)
        {
            bool isPlayerTurn = node.Board.IsPlayerTurn();

            // Filter possible moves based on the current player's turn
            List<Tuple<PieceSimulation, PieceSimulation>> filteredMoves = new();
            foreach (var move in node.PossibleMoves)
            {
                bool isValidMove = (move.Item1.PieceType == PieceType.AIPiece && move.Item2.PieceType == PieceType.Player && !isPlayerTurn) ||
                    (move.Item1.PieceType == PieceType.Player && move.Item2.PieceType == PieceType.AIPiece && isPlayerTurn);

                if (isValidMove && !MoveExistsInChildren(move, node.Children))
                {
                    filteredMoves.Add(move);
                }
            }

            // Return the first untried move, or null if there are none
            return filteredMoves.FirstOrDefault();
        }

// Helper method to check if a move already exists in the list of children
        private static bool MoveExistsInChildren(Tuple<PieceSimulation, PieceSimulation> move, List<oldMCTSNode> children)
        {
            foreach (var child in children)
            {
                if (TupleEquals(child.Move, move))
                {
                    return true;
                }
            }
            return false;
        }

// Helper method to check Tuple equality
        private static bool TupleEquals(Tuple<PieceSimulation, PieceSimulation> tuple1, Tuple<PieceSimulation, PieceSimulation> tuple2)
        {
            return tuple1?.Item1 == tuple2?.Item1 && tuple1?.Item2 == tuple2?.Item2;
        }



        private int Evaluate(BoardSimulation board)
        {
            // int playerScore = SimulationScoringAlgorythms.CalculatePlayerScore(board.PlayerPieces);
            // int aiScore = SimulationScoringAlgorythms.CalculateAIScore(board.AIPieces);
            //
            // int boardAdvantageFactor = SimulationScoringAlgorythms.CalculateBoardAdvantage(board);
            // int threatAssessmentFactor = SimulationScoringAlgorythms.CalculateThreatAssessment(board);


            // return (playerScore - aiScore) + boardAdvantageFactor + threatAssessmentFactor;
            // return Evaluate(board);
            return UnityEngine.Random.Range(-3, 3);
        }
    }
}
// Add a TupleComparer class to compare Tuples for equality
public class TupleComparer : IEqualityComparer<Tuple<PieceSimulation, PieceSimulation>>
{
    public bool Equals(Tuple<PieceSimulation, PieceSimulation> x, Tuple<PieceSimulation, PieceSimulation> y)
    {
        return x?.Item1 == y?.Item1 && x?.Item2 == y?.Item2;
    }

    public int GetHashCode(Tuple<PieceSimulation, PieceSimulation> obj)
    {
        return obj?.Item1.GetHashCode() ?? 0 ^ obj?.Item2.GetHashCode() ?? 0;
    }

    // Singleton instance of the comparer
    public static TupleComparer Default{ get; } = new();
}
