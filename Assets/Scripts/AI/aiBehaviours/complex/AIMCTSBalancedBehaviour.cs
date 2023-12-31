using Game;
using GameAI.Algorithms;
using GameAI.Algorithms.MonteCarlo;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours.complex
{
    public class AIMCTSBalancedBehaviour: AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {

            ChipCombatGameWinnerOrDraw ChipCombatsimulation = new(board);

            Tuple<string, string> bestmove =
                RandomSimulation<ChipCombatGameWinnerOrDraw, Tuple<string, string>, ChipCombatGameWinnerOrDraw.ChipPlayer>
                    .ParallelSearch(ChipCombatsimulation, 10 * (enemyPieces.Count + playerPieces.Count));


            PieceView attackingPiece = null;
            PieceView targetPiece = null;

            foreach (PieceView epiece in enemyPieces)
            {
                if (epiece.Id == bestmove.Item1)
                {
                    attackingPiece = epiece;
                }
            }

            foreach (PieceView ppiece in playerPieces)
            {
                if (ppiece.Id == bestmove.Item2)
                {
                    targetPiece = ppiece;
                }
            }

            return new Tuple<PieceView, PieceView>(attackingPiece, targetPiece);
        }
    }
}