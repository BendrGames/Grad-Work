using Data;
using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Simulation
{
    public class BoardSimulation
    {
        public List<PieceSimulation> PlayerPieces;
        public List<PieceSimulation> AIPieces;

        private bool enemyTurn;

        public BoardSimulation(Board board)
        {
            PlayerPieces = new List<PieceSimulation>();
            AIPieces = new List<PieceSimulation>();
            isPlayerTurn = false; // Start with player's turn

            CreateSimulatedCopy(board);
        }

        public BoardSimulation(BoardSimulation board)
        {
            PlayerPieces = new List<PieceSimulation>();
            AIPieces = new List<PieceSimulation>();
            isPlayerTurn = board.isPlayerTurn; // Copy the turn indicator

            CreateSimulatedCopy(board);
        }

        public void CreateSimulatedCopy(Board board)
        {
            // Copy player pieces
            foreach (var playerPiece in board.PlayerPieces)
            {
                PieceSimulation copyPiece = new(playerPiece.pieceType, playerPiece.Attack, playerPiece.Health,playerPiece.Id);
                PlayerPieces.Add(copyPiece);
            }

            // Copy AI pieces
            foreach (var aiPiece in board.EnemyPieces)
            {
                PieceSimulation copyPiece = new(aiPiece.pieceType, aiPiece.Attack, aiPiece.Health, aiPiece.Id);
                AIPieces.Add(copyPiece);
            }
        }
        
        public void CreateSimulatedCopy(BoardSimulation board)
        {
            // Copy player pieces
            foreach (var playerPiece in board.PlayerPieces)
            {
                PieceSimulation copyPiece = new(playerPiece.PieceType, playerPiece.Attack, playerPiece.Health, playerPiece.Id);
                PlayerPieces.Add(copyPiece);
            }

            // Copy AI pieces
            foreach (var aiPiece in board.AIPieces)
            {
                PieceSimulation copyPiece = new(aiPiece.PieceType, aiPiece.Attack, aiPiece.Health, aiPiece.Id);
                AIPieces.Add(copyPiece);
            }
        }

        public void RemovePieceFromAlivePieces(PieceSimulation currentarget)
        {
            if (currentarget.PieceType == PieceType.Player)
            {
                PlayerPieces.Remove(currentarget);
            }
            else if (currentarget.PieceType == PieceType.AIPiece)
            {
                AIPieces.Remove(currentarget);
            }
        }

        public void CheckForDeadPieces()
        {
            for (int index = PlayerPieces.Count - 1; index >= 0; index--)
            {
                PieceSimulation piece = PlayerPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                }
            }
            for (int index = AIPieces.Count - 1; index >= 0; index--)
            {
                PieceSimulation piece = AIPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                }
            }
        }
        public bool IsGameOver()
        {
            return PlayerPieces.Count == 0 || AIPieces.Count == 0;
        }
        
        public void SimulateMove(PieceSimulation attacker, PieceSimulation target)
        {
            // Simulate the attack
            if (attacker != null && target != null)
            {
                target.SimulateAttack(attacker);
            }

            // Check for dead pieces after the attack
            CheckForDeadPieces();
        }
        
        public void UndoMove(PieceSimulation attacker, PieceSimulation target)
        {
            // Undo the attack
            if (attacker != null && target != null)
            {
                // Revert the attacked piece to its previous health
                target.UndoAttack();
            }

            // If pieces were removed during the attack, add them back to the appropriate list
            if (attacker != null && target != null && target.IsDead())
            {
                // Assuming you have a method like AddPieceToAlivePieces that adds a piece back to the list
                AddPieceToAlivePieces(target);
            }

            // You may need additional logic here to properly undo the move depending on your game's rules

            // Check for dead pieces after undoing the move
            CheckForDeadPieces();
        }

        private void AddPieceToAlivePieces(PieceSimulation piece)
        {
            if (piece.PieceType == PieceType.Player)
            {
                PlayerPieces.Add(piece);
            }
            else if (piece.PieceType == PieceType.AIPiece)
            {
                AIPieces.Add(piece);
            }
        }
        public object GetCurrentPlayerTurn()
        {
            throw new NotImplementedException();
        }
        
        private bool isPlayerTurn; // true for player's turn, false for AI's turn

        // ... other members and methods ...

        public bool IsPlayerTurn()
        {
            return isPlayerTurn;
        }

        public void SwitchTurn()
        {
            isPlayerTurn = !isPlayerTurn;
        }
    }
}
