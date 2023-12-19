using DefaultNamespace.AI.Algorythms.MCTStry3;
using Game;
using GameAI.Algorithms.MonteCarlo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GameAI.GameInterfaces;

namespace GameAI.Algorithms
{
    public class ChipCombatGameUCB1 : UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.IGame

    {
        private List<Piece> Pieces = new List<Piece>();
        private ChipPlayer currentPlayer;

        public ChipCombatGameUCB1(Board board)
        {
            foreach (PieceView pp in board.PlayerPieces)
            {
                Pieces.Add(new Piece(pp.Id, pp.attack, pp.health, true));
            }

            foreach (PieceView pp in board.EnemyPieces)
            {
                Pieces.Add(new Piece(pp.Id, pp.attack, pp.health, false));
            }

            currentPlayer = ChipPlayer.enemy;
        }

        public ChipCombatGameUCB1(ChipCombatGameUCB1 other)
        {
            currentPlayer = other.currentPlayer;
            Pieces = new List<Piece>();
            foreach (Piece p in other.Pieces)
            {
                Pieces.Add(new Piece(p.Id, p.Attack, p.Health, p.IsPlayerPiece));
            }
        }

        public ChipCombatGameUCB1 DeepCopy()
        {
            return new ChipCombatGameUCB1(this);
        }

        public void DoMove(Tuple<string, string> move)
        {
            if (IsLegalMove(move))
            {
                GetPieceById(move.Item1).Health -= GetPieceById(move.Item2).Attack;
                GetPieceById(move.Item2).Health -= GetPieceById(move.Item1).Attack;

                RemoveDeadPieces();

                currentPlayer = (currentPlayer == ChipPlayer.player) ? ChipPlayer.enemy : ChipPlayer.player;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        private void RemoveDeadPieces()
        {
            Pieces.RemoveAll(piece => piece.Health <= 0);
        }

        private Piece GetPieceById(string pieceId)
        {
            return Pieces.Find(piece => piece.Id == pieceId);
        }

        private bool IsLegalMove(Tuple<string, string> move)
        {
            Piece piece1 = GetPieceById(move.Item1);
            Piece piece2 = GetPieceById(move.Item2);

            if (currentPlayer == ChipPlayer.player)
            {
                return piece1.IsPlayerPiece && !piece2.IsPlayerPiece;
            }
            else if (currentPlayer == ChipPlayer.enemy)
            {
                return !piece1.IsPlayerPiece && piece2.IsPlayerPiece;
            }

            return false;
        }

        public bool IsGameOver()
        {
            return GetLegalMoves().Count == 0 || IsWinner(ChipPlayer.player) || IsWinner(ChipPlayer.enemy);
        }

        public ChipPlayer CurrentPlayer => currentPlayer;

        public List<Tuple<string, string>> GetLegalMoves()
        {
            List<Tuple<string, string>> legalMoves = new List<Tuple<string, string>>();

            foreach (Piece attackingPiece in Pieces)
            {
                foreach (Piece defendingPiece in Pieces)
                {
                    if (currentPlayer == ChipPlayer.player)
                    {
                        if (attackingPiece.IsPlayerPiece && !defendingPiece.IsPlayerPiece)
                        {
                            legalMoves.Add(new Tuple<string, string>(attackingPiece.Id, defendingPiece.Id));
                        }
                    }
                    else if (currentPlayer == ChipPlayer.enemy)
                    {
                        if (!attackingPiece.IsPlayerPiece && defendingPiece.IsPlayerPiece)
                        {
                            legalMoves.Add(new Tuple<string, string>(attackingPiece.Id, defendingPiece.Id));
                        }
                    }
                }
            }

            return legalMoves;
        }

        public bool IsWinner(ChipPlayer player)
        {
            int playerPieceCount = 0;
            int enemyPieceCount = 0;

            foreach (Piece piece in Pieces)
            {
                if (piece.IsPlayerPiece)
                {
                    playerPieceCount++;
                }
                else
                {
                    enemyPieceCount++;
                }
            }

            if (player == ChipPlayer.player)
            {
                // Player wins if player has at least 1 piece and enemy has 0 pieces
                return playerPieceCount > 0 && enemyPieceCount == 0;
            }
            else
            {
                // Enemy wins if player has 0 pieces and enemy has at least 1 piece
                return playerPieceCount == 0 && enemyPieceCount > 0;
            }
        }

        public enum ChipPlayer
        {
            None,
            player,
            enemy
        }
        public long Hash{
            get {
                // Replace the following line with a proper hash function based on your game state
                return GetHashCode();
            }
        }

        public void Transition(UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.Transition t)
        {
            // Apply the given transition to the game state
            DoMove(t.Move);
        }

        public List<UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.Transition> GetLegalTransitions()
        {
            List<UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.Transition> legalTransitions =
                new List<UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.Transition>();

            foreach (var move in GetLegalMoves())
            {
                ChipCombatGameUCB1 nextState = DeepCopy();
                nextState.DoMove(move);
                legalTransitions.Add(new UCB1Tree<ChipCombatGameUCB1, Tuple<string, string>, ChipCombatGameUCB1.ChipPlayer>.Transition
                (
                    move,
                    // Replace 'NextState' with the actual property name in your 'Transition' class
                    nextState.Hash
                ));
            }

            return legalTransitions;
        }

        public void Rollout()
        {
            // Implement a rollout policy to simulate random moves until a terminal state is reached
            Random random = new Random();

            while (!IsGameOver())
            {
                List<Tuple<string, string>> legalMoves = GetLegalMoves();
                if (legalMoves.Count > 0)
                {
                    int randomMoveIndex = random.Next(legalMoves.Count);
                    DoMove(legalMoves[randomMoveIndex]);
                }
            }
        }
        // ... (remaining code)
    }
}

