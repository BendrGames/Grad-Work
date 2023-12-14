using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours.Medium
{
    public class AiRandomALowestHealthT : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            PieceView targetChip = enemyPieces.OrderBy(p => p.health).FirstOrDefault();
            playerPieces.Shuffle();
            PieceView attackingChip = playerPieces.FirstOrDefault();

            return new Tuple<PieceView, PieceView>(targetChip, attackingChip);
        }
    }
}
