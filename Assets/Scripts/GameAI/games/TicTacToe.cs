using GameAI.Algorithms.MonteCarlo;
using GameAI.GameInterfaces;
using System;
using System.Collections.Generic;
namespace GameAI.Algorithms
{
    public class TicTacToe : RandomSimulation<TicTacToe, int, TicTacToe.Player>.IGame
    {
        private int[] board;
        private Player currentPlayer;

        public TicTacToe()
        {
            board = new int[9];
            currentPlayer = Player.X;
        }

        private TicTacToe(TicTacToe other)
        {
            board = (int[])other.board.Clone();
            currentPlayer = other.currentPlayer;
        }

        public TicTacToe DeepCopy()
        {
            return new TicTacToe(this);
        }

        public void DoMove(int move)
        {
            if (IsLegalMove(move))
            {
                board[move] = (int)currentPlayer;
                currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        public bool IsGameOver()
        {
            return GetLegalMoves().Count == 0 || GetWinner() != Player.None;
        }

        public Player CurrentPlayer => currentPlayer;

        public List<int> GetLegalMoves()
        {
            var legalMoves = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == 0)
                {
                    legalMoves.Add(i);
                }
            }
            return legalMoves;
        }

        public bool IsWinner(Player player)
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i * 3] == (int)player && board[i * 3 + 1] == (int)player && board[i * 3 + 2] == (int)player)
                    return true; // Row
                if (board[i] == (int)player && board[i + 3] == (int)player && board[i + 6] == (int)player)
                    return true; // Column
            }
            if (board[0] == (int)player && board[4] == (int)player && board[8] == (int)player)
                return true; // Diagonal
            if (board[2] == (int)player && board[4] == (int)player && board[6] == (int)player)
                return true; // Diagonal
            return false;
        }

        public Player GetWinner()
        {
            if (IsWinner(Player.X)) return Player.X;
            if (IsWinner(Player.O)) return Player.O;
            return Player.None;
        }

        private bool IsLegalMove(int move)
        {
            return move >= 0 && move < 9 && board[move] == 0;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{Symbol(board[i * 3])} | {Symbol(board[i * 3 + 1])} | {Symbol(board[i * 3 + 2])}");
                if (i < 2)
                    Console.WriteLine("---------");
            }
            Console.WriteLine();
        }

        private char Symbol(int cell)
        {
            return cell switch
            {
                0 => ' ',
                (int)Player.X => 'X',
                (int)Player.O => 'O',
                _ => throw new InvalidOperationException("Invalid cell value"),
            };
        }

        public enum Player
        {
            None,
            X,
            O
        }
        
    }
}
