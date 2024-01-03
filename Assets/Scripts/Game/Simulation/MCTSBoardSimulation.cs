using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Simulation
{
    public class MCTSBoardSimulation
    {
        public List<PieceSimulation> PlayerPieces;
        public List<PieceSimulation> AIPieces;

        public int wins;
        public int playouts;

        private bool IsEnemyTurn;

        public MCTSBoardSimulation(Board board, bool IsEnemyTurn)
        {
            PlayerPieces = new List<PieceSimulation>();
            AIPieces = new List<PieceSimulation>();
            this.IsEnemyTurn = IsEnemyTurn;
            wins = 0;
            playouts = 0;

            CreateSimulatedCopy(board);
        }

        public MCTSBoardSimulation(MCTSBoardSimulation board, bool IsEnemyTurn)
        {
            PlayerPieces = new List<PieceSimulation>();
            AIPieces = new List<PieceSimulation>();
            this.IsEnemyTurn = IsEnemyTurn;
            
           

            wins = board.wins;
            playouts = board.playouts;

            CreateSimulatedCopy(board);
        }

        public void CreateSimulatedCopy(Board board)
        {
            // Copy player pieces
            foreach (var playerPiece in board.PlayerPieces)
            {
                PieceSimulation copyPiece = new(playerPiece.pieceType, playerPiece.Attack, playerPiece.Health, playerPiece.Id);
                PlayerPieces.Add(copyPiece);
            }

            // Copy AI pieces
            foreach (var aiPiece in board.EnemyPieces)
            {
                PieceSimulation copyPiece = new(aiPiece.pieceType, aiPiece.Attack, aiPiece.Health, aiPiece.Id);
                AIPieces.Add(copyPiece);
            }
        }

        public void CreateSimulatedCopy(MCTSBoardSimulation board)
        {
            // Copy player pieces
            foreach (var playerPiece in board.PlayerPieces)
            {
                PieceSimulation copyPiece = new(playerPiece.PieceType, playerPiece.Attack, playerPiece.Health, playerPiece.Id);
                PlayerPieces.Add(copyPiece);
            }

            // Copy AI pieces
            foreach (var aiPiece in board.AIPieces)
            {
                PieceSimulation copyPiece = new(aiPiece.PieceType, aiPiece.Attack, aiPiece.Health, aiPiece.Id);
                AIPieces.Add(copyPiece);
            }
        }

        public bool IsenemyTurn()
        {
            return IsEnemyTurn;
        }

        public void SwitchTurn()
        {
            IsEnemyTurn = !IsEnemyTurn;
        }

        public bool FullyExplored()
        {
            List<Tuple<PieceSimulation, PieceSimulation>> possibleMoves = GetMoves();
            foreach (var move in possibleMoves)
            {
                MCTSBoardSimulation next = MokeMove(move);

                if (next.playouts == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public Tuple<PieceSimulation, PieceSimulation> SelectMove(float explorecoefficent)
        {
            float lnN = Mathf.Log(playouts);

            float bestUCT = 0;

            Tuple<PieceSimulation, PieceSimulation> bestmove = null;

            List<Tuple<PieceSimulation, PieceSimulation>> possibleMoves = GetMoves();
            foreach (Tuple<PieceSimulation, PieceSimulation> move in possibleMoves)
            {
                MCTSBoardSimulation next = MokeMove(move);
                float uct = next.wins / next.playouts + explorecoefficent + Mathf.Sqrt(2 * lnN / next.wins);

                if (uct > bestUCT)
                {
                    bestUCT = uct;
                    bestmove = move;
                }
            }

            return bestmove;
        }
        
   

        public Tuple<PieceSimulation, PieceSimulation> ChooseUnexploredMove()
        {
            List<Tuple<PieceSimulation, PieceSimulation>> unexploredMoves = new();
            
            List<Tuple<PieceSimulation, PieceSimulation>> possibleMoves = GetMoves();
            foreach (Tuple<PieceSimulation, PieceSimulation> move in possibleMoves)
            {
                MCTSBoardSimulation next = MokeMove(move);
                if (next.playouts == 0)
                {
                    unexploredMoves.Add(move);
                }
            }

            return RandomChoice(unexploredMoves);
        }
        private Tuple<PieceSimulation, PieceSimulation> RandomChoice(List<Tuple<PieceSimulation, PieceSimulation>> unexploredMoves)
        {
            int randomIndex = UnityEngine.Random.Range(0, unexploredMoves.Count);
            return unexploredMoves[randomIndex];
        }

        public List<Tuple<PieceSimulation, PieceSimulation>> GetMoves()
        {
            List<Tuple<PieceSimulation, PieceSimulation>> validMoves = new();

            if (IsEnemyTurn)
            {
                foreach (var playersim in PlayerPieces)
                {
                    foreach (var enemysim in AIPieces)
                    {
                        validMoves.Add(new Tuple<PieceSimulation, PieceSimulation>(playersim, enemysim));
                    }
                }
            }
            else
            {
                foreach (var enemysim in AIPieces)
                {
                    foreach (var playersim in PlayerPieces)
                    {
                        validMoves.Add(new Tuple<PieceSimulation, PieceSimulation>(enemysim, playersim));
                    }
                }
            }

            return validMoves;
        }

        public MCTSBoardSimulation MokeMove(Tuple<PieceSimulation, PieceSimulation> move)
        {
            Debug.Log(IsenemyTurn());
            
            MCTSBoardSimulation newBoard = new(this, IsEnemyTurn);

            var attacker = FindPieceInCorrectBoard(move.Item1, newBoard);
            var target = FindPieceInCorrectBoard(move.Item2, newBoard);
            
            // var attacker = move.Item1;
            // var target = move.Item2;

            if (attacker != null && target != null)
            {
                target.SimulateAttack(attacker);
            }

            newBoard.CheckForDeadPieces();

            return newBoard;
        }

        public PieceSimulation FindPieceInCorrectBoard(PieceSimulation pieceView, MCTSBoardSimulation TargetBoard)
        {
            foreach (PieceSimulation epiece in AIPieces)
            {
                if (epiece.Id == pieceView.Id)
                {
                    return epiece;
                }
            }
           
            foreach (PieceSimulation ppiece in PlayerPieces)
            {
                if (ppiece.Id == pieceView.Id)
                {
                    return ppiece;
                }
            }
            return null;
        }
        
        public void MokeMoveNoSim(Tuple<PieceSimulation, PieceSimulation> move)
        {
           
            var attacker = FindPieceInCorrectBoard(move.Item1, this);
            var target = FindPieceInCorrectBoard(move.Item2, this);

            if (attacker != null && target != null)
            {
                target.SimulateAttack(attacker);
            }

            CheckForDeadPieces();
        }

        //piece removal
        public void CheckForDeadPieces()
        {
            for (int index = PlayerPieces.Count - 1; index >= 0; index--)
            {
                PieceSimulation piece = PlayerPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                }
            }
            for (int index = AIPieces.Count - 1; index >= 0; index--)
            {
                PieceSimulation piece = AIPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                }
            }
        }

        public void RemovePieceFromAlivePieces(PieceSimulation currentarget)
        {
            if (currentarget.PieceType == PieceType.Player)
            {
                // Debug.Log("Player piece removed");
                PlayerPieces.Remove(currentarget);
            }
            else if (currentarget.PieceType == PieceType.AIPiece)
            {
                // Debug.Log("enemy piece removed");
                AIPieces.Remove(currentarget);
            }
        }
        public bool IsGameOver()
        {
            return PlayerPieces.Count == 0 || AIPieces.Count == 0;
        }

        public bool IsEnemyWinner()
        {
            return AIPieces.Count >= PlayerPieces.Count;

        }
        // public Tuple<PieceSimulation, PieceSimulation> SelectBestMove()
        // {
        //     Tuple<PieceSimulation, PieceSimulation> bestMove = null;
        //     float bestWinRate = 0;
        //
        //     foreach (Tuple<PieceSimulation, PieceSimulation> move in GetMoves())
        //     {
        //         MCTSBoardSimulation next = MokeMove(move);
        //         float winRate = next.wins / next.playouts;
        //
        //         if (winRate > bestWinRate)
        //         {
        //             bestWinRate = winRate;
        //             bestMove = move;
        //         }
        //     }
        //
        //     return bestMove;
        // }
       
    }
}
