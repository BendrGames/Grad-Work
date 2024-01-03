using Game;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.aiBehaviours
{
    public class AIAlphabeticalBehaviour: AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            List<PieceView> tempenemyPieces = new(enemyPieces);
            List<PieceView> tempPlayerPieces = new(playerPieces);
            
            tempenemyPieces = tempenemyPieces.OrderBy(x => x.name).ToList();
            tempPlayerPieces = tempPlayerPieces.OrderByDescending(x => x.name).ToList();

            return new Tuple<PieceView, PieceView>(tempenemyPieces[0], tempPlayerPieces[0]);
        }
        
    }
}
