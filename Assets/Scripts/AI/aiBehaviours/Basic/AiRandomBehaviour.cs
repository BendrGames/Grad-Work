using Game;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours
{
    public class AiRandomBehaviour : AIBehaviourBase
    {

        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            List<PieceView> tempenemyPieces = new List<PieceView>(enemyPieces);
            List<PieceView> tempPlayerPieces = new List<PieceView>(playerPieces);
            
            tempenemyPieces.Shuffle();
            tempPlayerPieces.Shuffle();

            return new Tuple<PieceView, PieceView>(tempenemyPieces[0], tempPlayerPieces[0]);
        }
    }
}
