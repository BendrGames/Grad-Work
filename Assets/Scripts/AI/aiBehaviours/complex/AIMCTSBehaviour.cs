using DefaultNamespace.AI.Algorythms;
using Game;
using Game.Simulation;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours.complex
{
    public class AIMCTSBehaviour : AIBehaviourBase
    {

        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            BoardSimulation simulatedBoard = new BoardSimulation(board);
            int simulationsPerMove = 200;
            oldMCTS oldMcts = new oldMCTS(simulationsPerMove);
            Tuple<PieceSimulation, PieceSimulation> result = oldMcts.FindBestMove(simulatedBoard);

            PieceView attackingPiece = null;
            PieceView targetPiece = null;

            foreach (PieceView epiece in enemyPieces)
            {
                if (epiece.Id == result.Item1.Id)
                {
                    attackingPiece = epiece;
                }
            }

            foreach (PieceView ppiece in playerPieces)
            {
                if (ppiece.Id == result.Item2.Id)
                {
                    targetPiece = ppiece;
                }
            }

            return new Tuple<PieceView, PieceView>(attackingPiece, targetPiece);
        }
    }
}
