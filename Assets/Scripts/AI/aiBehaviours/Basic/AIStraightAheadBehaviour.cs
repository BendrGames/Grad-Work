using Game;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.aiBehaviours
{
    public class AIStraightAheadBehaviour : AIBehaviourBase
    {
        public override Tuple<PieceView, PieceView> FindTarget(Board board, List<PieceView> enemyPieces, List<PieceView> playerPieces)
        {
            List<PieceView> enemyPiecesShuffled = enemyPieces;
            enemyPiecesShuffled.Shuffle();

            foreach (PieceView epiece in enemyPiecesShuffled)
            {
                if (board.isPlayerPieceInFrontOfMe(epiece, out var foundPlayerPiece))
                {
                    return new Tuple<PieceView, PieceView>(epiece, foundPlayerPiece);
                }
            }
            
            List<PieceView> tempenemyPieces = new(enemyPieces);
            List<PieceView> tempPlayerPieces = new(playerPieces);
            
            tempenemyPieces.Shuffle();
            tempPlayerPieces.Shuffle();

            return new Tuple<PieceView, PieceView>(tempenemyPieces[0], tempPlayerPieces[0]);
        }
    }
}
