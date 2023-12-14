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
            List<Tuple<PieceSimulation, PieceSimulation>> possiblemoves = new List<Tuple<PieceSimulation, PieceSimulation>>();

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
            int playerScore = CalculatePlayerScore(board.PlayerPieces);
            int aiScore = CalculateAIScore(board.AIPieces);

            int boardAdvantageFactor = CalculateBoardAdvantage(board);
            int threatAssessmentFactor = CalculateThreatAssessment(board);

            // Adjust weights based on the importance of different factors in your game
            return (playerScore - aiScore) + boardAdvantageFactor + threatAssessmentFactor;
        }
        
        private static int CalculateBoardAdvantage(BoardSimulation board)
        {
            int playerBoardValue = CalculateBoardValue(board.PlayerPieces);
            int aiBoardValue = CalculateBoardValue(board.AIPieces);

            // Weighted difference in board advantage
            return (playerBoardValue - aiBoardValue) * 2;
        }

        private static int CalculateBoardValue(List<PieceSimulation> pieces)
        {
            // Calculate the value of the board based on the sum of health and attack of all pieces
            return pieces.Sum(p => (p.Health + p.Attack));
        }

        private static int CalculateThreatAssessment(BoardSimulation board)
        {
            int playerThreatValue = CalculateThreatValue(board.PlayerPieces);
            int aiThreatValue = CalculateThreatValue(board.AIPieces);

            // Weighted difference in threat assessment
            return (playerThreatValue - aiThreatValue);
        }

        private static int CalculateThreatValue(List<PieceSimulation> pieces)
        {
            // Calculate the threat value based on the sum of attack values of all pieces
            return pieces.Sum(p => p.Attack);
        }

        private static int CalculatePlayerScore(List<PieceSimulation> pieces)
        {
            return pieces.Sum(p => (p.Health - p.Attack) * WeightedValueForPlayer(p.PieceType));
        }

        private static int CalculateAIScore(List<PieceSimulation> pieces)
        {
            return pieces.Sum(p => (p.Health - p.Attack) * WeightedValueForAI(p.PieceType));
        }

        private static int WeightedValueForPlayer(PieceType pieceType)
        {
            // You can adjust these weights based on the importance of health, attack, and other factors
            return pieceType == PieceType.Player ? 1 : 1;
        }

        private static int WeightedValueForAI(PieceType pieceType)
        {
            // You can adjust these weights based on the importance of health, attack, and other factors
            return pieceType == PieceType.AIPiece ? 1 : 1;
        }
    }
    
}
