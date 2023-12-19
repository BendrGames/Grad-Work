using DefaultNamespace.AI.Algorythms;
using DefaultNamespace.AI.Algorythms.MCTS;
using DefaultNamespace.AI.Algorythms.MCTStry3;
using Game;
using Game.Simulation;
using GameAI.Algorithms;
using GameAI.Algorithms.MonteCarlo;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours.complex
{
    public class AIMCTSDeepWinBehaviour : AIBehaviourBase
    {

        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
           
         ChipCombatGameWinner ChipCombatsimulation = new(board);

            Tuple<string, string> bestmove = 
                RandomSimulation<ChipCombatGameWinner, Tuple<string, string>, ChipCombatGameWinner.ChipPlayer>
                    .ParallelSearch(ChipCombatsimulation, EnemyAiManager.DeepSearchIterationMultiplyer * (enemyPieces.Count + playerPieces.Count));


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
