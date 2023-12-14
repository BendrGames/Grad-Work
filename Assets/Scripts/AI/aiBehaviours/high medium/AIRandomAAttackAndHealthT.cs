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
            PieceView targetChip = enemyPieces.OrderByDescending(p => p.attack * 0.8 + p.health * 0.2).FirstOrDefault();
        
            // Select attacking chip based on some strategy (e.g., high attack, high health, or a combination)
            playerPieces.Shuffle();
            PieceView attackingChip = playerPieces.FirstOrDefault();

            return new Tuple<PieceView, PieceView>(targetChip, attackingChip);
        }
    }
}
