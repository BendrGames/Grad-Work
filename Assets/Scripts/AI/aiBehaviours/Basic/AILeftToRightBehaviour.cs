using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours
{
    public class AILeftToRightBehaviour: AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            List<PieceView> tempenemyPieces = new(enemyPieces);
            List<PieceView> tempPlayerPieces = new(playerPieces);
        
            return new Tuple<PieceView, PieceView>(tempenemyPieces[0], tempPlayerPieces[tempPlayerPieces.Count - 1]);
        }
        
    }
}
