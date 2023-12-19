using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.Medium
{
    public class AIBestHighHealthKillPotential: AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            PieceView bestPlayerPiece = null;
            PieceView bestEnemyPiece = null;
            int maxDamage = 0;

            foreach (PieceView enemyPiece in enemyPieces)
            {
                foreach (PieceView playerPiece in playerPieces)
                {
                    int damage = CalculateDamage(enemyPiece, playerPiece);

                    // Check if the enemy piece can kill the player piece in one attack
                    if (damage >= playerPiece.health && damage > maxDamage)
                    {
                        maxDamage = damage;
                        bestPlayerPiece = playerPiece;
                        bestEnemyPiece = enemyPiece;
                    }
                }
            }

            // Return the best pair of pieces for the most efficient kill with enemy as item 1
            return Tuple.Create(bestEnemyPiece, bestPlayerPiece);
        }

// Calculate the damage that an enemy piece can deal to a player piece
        private int CalculateDamage(PieceView enemyPiece, PieceView playerPiece)
        {
            // This is a simplistic example; you may have a more complex formula based on your game logic
            int damage = enemyPiece.attack - playerPiece.health;

            return damage > 0 ? damage : 0;
        }
    }
}
