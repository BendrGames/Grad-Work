using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace.AI.Algorythms.MCTStry3
{
   public class MonteCarloTreeNode
{
    public int Visits { get; set; }
    public double Score { get; set; }
    public List<MonteCarloTreeNode> Children { get; set; }
    public MonteCarloTreeNode Parent { get; set; }
    public MCTSBoard Board { get; set; }

    public MonteCarloTreeNode(MCTSBoard board)
    {
        Visits = 0;
        Score = 0.0;
        Children = new List<MonteCarloTreeNode>();
        Parent = null;
        Board = board;
    }

    public MonteCarloTreeNode SelectChild()
    {
        if (!Children.Any())
            return this;

        return Children.OrderByDescending(c => UCB1(c)).First();
    }

    private double UCB1(MonteCarloTreeNode child)
    {
        const double explorationWeight = 2000;

        if (child.Visits == 0)
            return double.MaxValue;

        return child.Score / child.Visits + explorationWeight * Math.Sqrt(Math.Log(Visits) / child.Visits);
    }

    public MonteCarloTreeNode Expand()
    {
        List<MCTSBoard> possibleMoves = GeneratePossibleMoves();

        foreach (var move in possibleMoves)
        {
            var newChild = new MonteCarloTreeNode(move)
            {
                Parent = this
            };
            Children.Add(newChild);
        }

        if (Children.Count == 0)
        {
            // If there are no possible moves, create a new child with the current board
            var newChild = new MonteCarloTreeNode(new MCTSBoard { PlayerPieces = new List<Piece>(Board.PlayerPieces), EnemyPieces = new List<Piece>(Board.EnemyPieces) })
            {
                Parent = this
            };
            Children.Add(newChild);
        }

        var randomChildIndex = new Random().Next(Children.Count);
        return Children[randomChildIndex];
    }
    
     private List<MCTSBoard> GeneratePossibleMoves()
    {
        List<MCTSBoard> possibleMoves = new List<MCTSBoard>();

        // Determine whose turn it is
        List<Piece> activePlayerPieces;
        List<Piece> opponentPieces;

        if (Board.PlayerPieces.Any() && Board.EnemyPieces.Any())
        {
            // If the number of visits is even, it's the active player's turn
            if (Visits % 2 == 0)
            {
                activePlayerPieces = Board.PlayerPieces;
                opponentPieces = Board.EnemyPieces;
            }
            else
            {
                // If the number of visits is odd, it's the opponent's turn
                activePlayerPieces = Board.EnemyPieces;
                opponentPieces = Board.PlayerPieces;
            }

            // Generate possible moves
            foreach (var activePiece in activePlayerPieces)
            {
                foreach (var targetPiece in opponentPieces.Where(p => p.Health > 0))
                {
                    // Clone the current board state
                    var newBoard = new MCTSBoard
                    {
                        PlayerPieces = new List<Piece>(Board.PlayerPieces),
                        EnemyPieces = new List<Piece>(Board.EnemyPieces)
                    };

                    // Find the active piece in the new board state
                    var newActivePiece = newBoard.PlayerPieces.Single(p => p.Id == activePiece.Id);

                    // Find the target piece in the new board state
                    var newTargetPiece = newBoard.EnemyPieces.Single(p => p.Id == targetPiece.Id);

                    // Simulate the attack
                    if (newActivePiece.Health > 0 && newTargetPiece.Health > 0)
                    {
                        newTargetPiece.Health -= newActivePiece.Attack;

                        if (newTargetPiece.Health <= 0)
                        {
                            newBoard.EnemyPieces.Remove(newTargetPiece);
                        }

                        newActivePiece.Health -= newTargetPiece.Attack;

                        if (newActivePiece.Health <= 0)
                        {
                            newBoard.PlayerPieces.Remove(newActivePiece);
                        }
                    }

                    possibleMoves.Add(newBoard);
                }
            }
        }

        return possibleMoves;
    }

     public void Backpropagate(double score)
    {
        Visits++;
        Score += score;

        if (Parent != null)
            Parent.Backpropagate(score);
    }

    public void SimulateMove()
    {
        List<Piece> activePlayerPieces = Board.PlayerPieces;
        List<Piece> opponentPieces = Board.EnemyPieces;

        if (activePlayerPieces.Any() && opponentPieces.Any())
        {
            Piece attackingPiece = activePlayerPieces[new Random().Next(activePlayerPieces.Count)];
            Piece targetPiece = opponentPieces[new Random().Next(opponentPieces.Count)];

            if (attackingPiece.Health > 0 && targetPiece.Health > 0)
            {
                targetPiece.Health -= attackingPiece.Attack;

                if (targetPiece.Health <= 0)
                {
                    opponentPieces.Remove(targetPiece);
                }

                attackingPiece.Health -= targetPiece.Attack;

                if (attackingPiece.Health <= 0)
                {
                    activePlayerPieces.Remove(attackingPiece);
                }
            }
        }
    }
}
}
