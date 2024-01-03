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
        
        internal static Tuple<PieceView, PieceView> FallBackRandomAttack(List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {

            List<PieceView> tempenemyPieces = new(enemyPieces);
            List<PieceView> tempPlayerPieces = new(playerPieces);

            tempenemyPieces.Shuffle();
            tempPlayerPieces.Shuffle();

            return new Tuple<PieceView, PieceView>(tempenemyPieces[0], tempPlayerPieces[0]);
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
