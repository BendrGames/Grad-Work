using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.Algorythms.MCTStry3
{
    public class MCTSBoard
    {
        public List<Piece> PlayerPieces { get; set; }
        public List<Piece> EnemyPieces { get; set; }

        public MCTSBoard()
        {
            PlayerPieces = new List<Piece>();
            EnemyPieces = new List<Piece>();
        }

        public bool IsGameOver()
        {
            // Check if all pieces of any player have zero health
            bool all = true;
            foreach (Piece p in PlayerPieces)
            {
                if (p.Health > 0)
                {
                    all = false;
                    break;
                }
            }
            return all || EnemyPieces.All(p => p.Health <= 0);
        }
    }
}
