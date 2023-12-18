using Game.Simulation;
using System;
using System.Collections.Generic;
namespace DefaultNamespace.AI.Algorythms
{
    public class oldMCTSNode
    {
        public BoardSimulation Board{ get; }
        public Tuple<PieceSimulation, PieceSimulation> Move{ get; private set; }
        public int Visits{ get; set; }
        public double TotalScore{ get; set; }

        public List<oldMCTSNode> Children{ get; }
        public List<Tuple<PieceSimulation, PieceSimulation>> PossibleMoves { get; }
        public oldMCTSNode Parent{ get; }

        public oldMCTSNode(BoardSimulation board,  Tuple<PieceSimulation, PieceSimulation> move = null, oldMCTSNode parent = null)
        {
            Board = board;
            Move = move;
            Parent = parent;
            Children = new List<oldMCTSNode>();
            PossibleMoves = GeneratePossibleMoves();
        }

        public bool IsTerminal => Board.IsGameOver();
        public bool IsFullyExpanded => PossibleMoves.Count == Children.Count;

        public void Expand(Tuple<PieceSimulation, PieceSimulation> move)
        {
            Move = move;

            // // Simulate the move on a copy of the board to avoid modifying the original board
            // BoardSimulation newBoard = new BoardSimulation(Board);
            // newBoard.SimulateMove(move.Item1, move.Item2);

            // Create a new child node representing the new board state
            oldMCTSNode childNode = new oldMCTSNode(new BoardSimulation(Board), move, this);
            Children.Add(childNode);
        }

        
        private List<Tuple<PieceSimulation, PieceSimulation>> GeneratePossibleMoves()
        {
            // Implement your logic to generate possible moves for the current board state
            // This could involve iterating over available pieces and possible targets
            List<Tuple<PieceSimulation, PieceSimulation>> possibleMoves = new List<Tuple<PieceSimulation, PieceSimulation>>();

            // Placeholder logic; replace with your actual move generation
            foreach (var playerPiece in Board.PlayerPieces)
            {
                foreach (var enemyPiece in Board.AIPieces)
                {
                    possibleMoves.Add(new Tuple<PieceSimulation, PieceSimulation>(playerPiece, enemyPiece));
                    possibleMoves.Add(new Tuple<PieceSimulation, PieceSimulation>(enemyPiece, playerPiece));
                }
            }

            return possibleMoves;
        }
        
        public override string ToString()
        {
            return $"Move: {Move}, Visits: {Visits}, TotalScore: {TotalScore}";
        }
    }
}
