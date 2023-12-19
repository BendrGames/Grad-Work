using DefaultNamespace.AI.Algorythms.MCTStry3;
using Game;
using GameAI.Algorithms.MonteCarlo;
using GameAI.GameInterfaces;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameAI.Algorithms
{
    public class ChipCombatGameWinnerOrDraw : RandomSimulation<ChipCombatGameWinnerOrDraw, Tuple<string, string>, ChipCombatGameWinnerOrDraw.ChipPlayer>.IGame
    {
        private List<Piece> Pieces = new List<Piece>();
        private ChipPlayer currentPlayer;

        public ChipCombatGameWinnerOrDraw(Board board)
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

        public ChipCombatGameWinnerOrDraw(ChipCombatGameWinnerOrDraw other)
        {
            currentPlayer = other.currentPlayer;
            Pieces = new List<Piece>();
            foreach (Piece p in other.Pieces)
            {
                Pieces.Add(new Piece(p.Id, p.Attack, p.Health, p.IsPlayerPiece));
            }
        }

        public ChipCombatGameWinnerOrDraw DeepCopy()
        {
            return new ChipCombatGameWinnerOrDraw(this);
        }

        public void DoMove(Tuple<string, string> move)
        {
            if (IsLegalMove(move))
            {
                // Debug.Log($"Before Move - Player: {currentPlayer}, Piece1 Health: {GetPieceById(move.Item1).Health}, Piece2 Health: {GetPieceById(move.Item2).Health}");

                GetPieceById(move.Item1).Health -= GetPieceById(move.Item2).Attack;
                GetPieceById(move.Item2).Health -= GetPieceById(move.Item1).Attack;

                // Debug.Log($"After Move - Player: {currentPlayer}, Piece1 Health: {GetPieceById(move.Item1).Health}, Piece2 Health: {GetPieceById(move.Item2).Health}");

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

        // public bool IsWinner(ChipPlayer player)
        // {
        //     if (player == ChipPlayer.player)
        //     {
        //         return !Pieces.Exists(piece => piece.IsPlayerPiece);
        //     }
        //     else
        //     {
        //         return !Pieces.Exists(piece => !piece.IsPlayerPiece);
        //     }
        // }
        
        public bool IsWinner(ChipPlayer player)
        {
            bool playerHasPieces = false;
        
            foreach (Piece piece in Pieces)
            {
                if ((player == ChipPlayer.player && piece.IsPlayerPiece) ||
                    (player == ChipPlayer.enemy && !piece.IsPlayerPiece))
                {
                    playerHasPieces = true;
                    // Continue searching, as we want to find the last player with pieces
                }
            }
        
            return !playerHasPieces;
        }
        
        // public bool IsWinner(ChipPlayer player)
        // {
        //     int playerPieceCount = 0;
        //     int enemyPieceCount = 0;
        //
        //     foreach (Piece piece in Pieces)
        //     {
        //         if (piece.IsPlayerPiece)
        //         {
        //             playerPieceCount++;
        //         }
        //         else
        //         {
        //             enemyPieceCount++;
        //         }
        //     }
        //
        //     if (player == ChipPlayer.player)
        //     {
        //         // Player wins if player has at least 1 piece and enemy has 0 pieces
        //         return playerPieceCount > 0 && enemyPieceCount == 0;
        //     }
        //     else
        //     {
        //         // Enemy wins if player has 0 pieces and enemy has at least 1 piece
        //         return playerPieceCount == 0 && enemyPieceCount > 0;
        //     }
        // }

        public enum ChipPlayer
        {
            None,
            player,
            enemy
        }
    }
}
