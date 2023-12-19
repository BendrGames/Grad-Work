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
            PieceView targetChip = enemyPieces.OrderBy(p => p.health).FirstOrDefault();
    
            // Sort playerPieces by attack in descending order
            playerPieces = playerPieces.OrderByDescending(p => p.attack).ToList();
    
            int targetHealth = targetChip.health;
    
            // Find the attacking chip with attack greater than or equal to targetHealth
            PieceView attackingChip = playerPieces.FirstOrDefault(p => p.attack >= targetHealth);

            return new Tuple<PieceView, PieceView>(attackingChip, targetChip);
        }
    }
}
