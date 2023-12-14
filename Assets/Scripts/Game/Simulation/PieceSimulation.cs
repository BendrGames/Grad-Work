namespace Game.Simulation
{
    public class PieceSimulation
    {
        
            
            public PieceType PieceType { get; private set; }
            public int Attack { get; private set; }
            public int Health { get; private set; }
            public string Id { get; private set; }

            public PieceSimulation( PieceType pieceType , int attack, int health, string id)
            {
                PieceType = pieceType;
                Attack = attack;
                Health = health;
                Id = id;
            }

            // public void SimulateAttack(PieceSimulation attackedPiece)
            // {
            //     // Implement logic to simulate an attack
            //     attackedPiece.TakeDamage(Attack);
            // }
            //
            // public bool IsDead()
            // {
            //     return Health <= 0;
            // }
            //
            // private void TakeDamage(int damage)
            // {
            //     Health -= damage;
            //     if (Health <= 0)
            //     {
            //         Health = 0;
            //     }
            // }
            
            private int previousHealth; // Store the previous health before an attack
            
            public void SimulateAttack(PieceSimulation attackedPiece)
            {
                // Store the previous health before the attack
                attackedPiece.StorePreviousHealth();

                // Implement logic to simulate an attack
                attackedPiece.TakeDamage(Attack);
            }

            public void UndoAttack()
            {
                // Restore the previous health to undo the attack
                Health = previousHealth;
            }

            private void StorePreviousHealth()
            {
                // Store the current health before the attack
                previousHealth = Health;
            }

            public bool IsDead()
            {
                return Health <= 0;
            }

            private void TakeDamage(int damage)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    Health = 0;
                }
            }
        }
    
}
