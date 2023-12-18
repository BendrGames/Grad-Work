using Game.Simulation;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.Algorythms
{
    public static class SimulationScoringAlgorythms
    {
         public static int CalculateBoardAdvantage(BoardSimulation board)
        {
            int playerBoardValue = CalculateBoardValue(board.PlayerPieces);
            int aiBoardValue = CalculateBoardValue(board.AIPieces);

            // Weighted difference in board advantage
            return (playerBoardValue - aiBoardValue) * 2;
        }

         public static int CalculateBoardValue(List<PieceSimulation> pieces)
        {
            // Calculate the value of the board based on the sum of health and attack of all pieces
            return pieces.Sum(p => (p.Health + p.Attack));
        }

         public static int CalculateThreatAssessment(BoardSimulation board)
        {
            int playerThreatValue = CalculateThreatValue(board.PlayerPieces);
            int aiThreatValue = CalculateThreatValue(board.AIPieces);

            // Weighted difference in threat assessment
            return (playerThreatValue - aiThreatValue);
        }

         public static int CalculateThreatValue(List<PieceSimulation> pieces)
        {
            // Calculate the threat value based on the sum of attack values of all pieces
            return pieces.Sum(p => p.Attack);
        }

         public static int CalculatePlayerScore(List<PieceSimulation> pieces)
        {
            return pieces.Sum(p => (p.Health - p.Attack) * WeightedValueForPlayer(p.PieceType));
        }

         public static int CalculateAIScore(List<PieceSimulation> pieces)
        {
            return pieces.Sum(p => (p.Health - p.Attack) * WeightedValueForAI(p.PieceType));
        }

         public static int WeightedValueForPlayer(PieceType pieceType)
        {
            // You can adjust these weights based on the importance of health, attack, and other factors
            return pieceType == PieceType.Player ? 1 : 1;
        }

         public static int WeightedValueForAI(PieceType pieceType)
        {
            // You can adjust these weights based on the importance of health, attack, and other factors
            return pieceType == PieceType.AIPiece ? 1 : 1;
        }
    }
}
