using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.Medium
{
    public class AiLowestHealthTBestAttacker : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            
            enemyPieces = enemyPieces.OrderBy(p => p.Attack).ToList();
            
            playerPieces = playerPieces.OrderBy(p => p.Health).ToList();
            
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
