using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.high_medium
{
    public class AIRandomAAttackAndHealthT : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            // Sort opponent's chips by a combination of attack and health (you can adjust the weights as needed)
            PieceView targetChip = playerPieces.OrderByDescending(p => p.Attack * 0.2 + p.Health * 0.8)
                .ThenBy(p => p.Health) // Add a secondary sorting by attack (lower attack is considered less dangerous)
                .FirstOrDefault();
        
            // Select attacking chip based on some strategy (e.g., high attack, high health, or a combination)
            playerPieces.Shuffle();
            PieceView attackingChip = enemyPieces.FirstOrDefault();

            return new Tuple<PieceView, PieceView>(attackingChip, targetChip);
        }
    }
}
