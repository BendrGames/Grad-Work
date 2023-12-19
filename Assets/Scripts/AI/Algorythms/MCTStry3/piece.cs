namespace DefaultNamespace.AI.Algorythms.MCTStry3
{
    public class Piece
    {
        public string Id { get; }
        public int Attack { get; }
        public int Health { get; set; }
        
        public bool IsPlayerPiece { get; set; }

        public Piece(string id, int attack, int health, bool isPlayerPiece)
        {
            Id = id;
            Attack = attack;
            Health = health;
            IsPlayerPiece = isPlayerPiece;
        }
    }
}
