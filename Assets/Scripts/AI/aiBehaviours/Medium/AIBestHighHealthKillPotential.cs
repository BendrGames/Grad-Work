using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.Medium
{
    public class AIBestHighHealthKillPotential : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            PieceView bestPlayerPiece = null;
            PieceView bestEnemyPiece = null;

            //sort the enemy pieces by damage, highest is first in the list
            enemyPieces = enemyPieces.OrderByDescending(p => p.Attack).ToList();

            //sort the player pieces by health, highest is first in the list
            playerPieces = playerPieces.OrderByDescending(p => p.Health).ToList();

            foreach (PieceView epiece in enemyPieces)
            {
                foreach (PieceView pPiece in playerPieces)
                {
                    if (epiece.Attack >= pPiece.Health)
                    {
                        return Tuple.Create(epiece, pPiece);
                    }
                }
            }

            return FallBackRandomAttack(enemyPieces, playerPieces);
        }
    }
}
