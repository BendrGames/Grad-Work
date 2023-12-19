using Game.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.Algorythms
{
    public static class MiniMax
    {
        public static Tuple<PieceSimulation, PieceSimulation> Minimax(BoardSimulation board, int depth, bool isMaximizing)
        {
            // Check for terminal conditions or reach the maximum depth
            if (depth == 0 || board.IsGameOver())
            {
                // Evaluate the simulated board and return the utility value
                int evaluation = Evaluate(board);
                return new Tuple<PieceSimulation, PieceSimulation>(null, null);
            }

            List<Tuple<PieceSimulation, PieceSimulation>> possibleMoves = GeneratePossibleMoves(board);

            Tuple<PieceSimulation, PieceSimulation> bestMove = null;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                foreach (var move in possibleMoves)
                {
                    // Make the move on the simulated board
                    board.SimulateMove(move.Item1, move.Item2);

                    // Recursively call minimax with the new simulated board state
                    Tuple<PieceSimulation, PieceSimulation> result = Minimax(board, depth - 1, false);

                    // Undo the move on the simulated board
                    board.UndoMove(move.Item1, move.Item2);

                    // Update the best move and score
                    if (result != null)
                    {
                        int score = Evaluate(board);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = move;
                        }
                    }
                }
            }
            else
            {
                int bestScore = int.MaxValue;
                foreach (var move in possibleMoves)
                {
                    // Make the move on the simulated board
                    board.SimulateMove(move.Item1, move.Item2);

                    // Recursively call minimax with the new simulated board state
                    Tuple<PieceSimulation, PieceSimulation> result = Minimax(board, depth - 1, true);

                    // Undo the move on the simulated board
                    board.UndoMove(move.Item1, move.Item2);

                    // Update the best move and score
                    if (result != null)
                    {
                        int score = Evaluate(board);
                        if (score < bestScore)
                        {
                            bestScore = score;
                            bestMove = move;
                        }
                    }
                }
            }

            return bestMove;
        }
        private static List<Tuple<PieceSimulation, PieceSimulation>> GeneratePossibleMoves(BoardSimulation board)
        {
            List<Tuple<PieceSimulation, PieceSimulation>> possiblemoves = new();

            foreach (PieceSimulation Epiecesim in board.AIPieces)
            {
                foreach (var playerPieceSim in board.PlayerPieces)
                {
                    possiblemoves.Add(new Tuple<PieceSimulation, PieceSimulation>(Epiecesim, playerPieceSim));
                }
            }
            // Implement logic to generate all possible moves on the simulated board
            // Return a list of tuples representing each possible move
            return possiblemoves; // Placeholder value, replace with your actual move generation logic
        }
        
        private static int Evaluate(BoardSimulation board)
        {
            int playerScore = SimulationScoringAlgorythms.CalculatePlayerScore(board.PlayerPieces);
            int aiScore = SimulationScoringAlgorythms.CalculateAIScore(board.AIPieces);

            int boardAdvantageFactor = SimulationScoringAlgorythms.CalculateBoardAdvantage(board);
            int threatAssessmentFactor =SimulationScoringAlgorythms. CalculateThreatAssessment(board);

            // Adjust weights based on the importance of different factors in your game
            return (playerScore - aiScore) + boardAdvantageFactor + threatAssessmentFactor;
        }
        
       
    }
    
}
