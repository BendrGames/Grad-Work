using Game;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours.Medium
{
    public class AIRevengeBot : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            return new Tuple<PieceView, PieceView>(null, null);
        }
    }
}
