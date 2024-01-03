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
                PieceView targetChip = playerPieces
                    .OrderByDescending(p => p.Health * 0.8 + p.Attack * 0.2) // Consider both health and attack for target selection
                    .ThenBy(p => p.Attack) // Add a secondary sorting by attack (lower attack is considered less dangerous)
                    .FirstOrDefault();
                
                playerPieces.Shuffle();
                PieceView attackingChip = enemyPieces.FirstOrDefault();

                return new Tuple<PieceView, PieceView>(attackingChip, targetChip);
            }
        
    }
}
