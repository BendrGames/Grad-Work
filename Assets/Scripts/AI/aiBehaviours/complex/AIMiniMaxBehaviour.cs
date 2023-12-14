using DefaultNamespace.AI.Algorythms;
using Game;
using Game.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace DefaultNamespace.AI.aiBehaviours.complex
{
    public class AiMiniMaxBehaviour : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            // Convert the board to a simulated board
            BoardSimulation simulatedBoard = new BoardSimulation(board);

            // Call the minimax function to find the best move
            int depth = 8; // Adjust the depth based on your game's complexity
            Tuple<PieceSimulation, PieceSimulation> bestMove = MiniMax.Minimax(simulatedBoard, depth, true);

            PieceView attackingPiece = null;
            PieceView targetPiece = null;

            foreach (PieceView epiece in enemyPieces)
            {
                if (epiece.Id == bestMove.Item1.Id)
                {
                    attackingPiece = epiece;
                }
            }

            foreach (PieceView ppiece in playerPieces)
            {
                if (ppiece.Id == bestMove.Item2.Id)
                {
                    targetPiece = ppiece;
                }
            }

            return new Tuple<PieceView, PieceView>(attackingPiece, targetPiece);
        }

    }
}


