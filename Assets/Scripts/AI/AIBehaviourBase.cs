using Game;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI
{
    public class AIBehaviourBase
    {
        public virtual Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            return null;
        }
    }
}
