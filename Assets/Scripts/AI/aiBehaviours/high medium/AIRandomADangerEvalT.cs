using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.high_medium
{
    public class AIRandomADangerEvalT : AIBehaviourBase
    {
        
            public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
            {
                PieceView targetChip = enemyPieces
                    .OrderByDescending(p => p.health * 0.8 + p.attack * 0.2) // Consider both health and attack for target selection
                    .ThenBy(p => p.attack) // Add a secondary sorting by attack (lower attack is considered less dangerous)
                    .FirstOrDefault();
                
                playerPieces.Shuffle();
                PieceView attackingChip = playerPieces.FirstOrDefault();

                return new Tuple<PieceView, PieceView>(targetChip, attackingChip);
            }
        
    }
}
