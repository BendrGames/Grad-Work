using DG.Tweening;
using Game.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
namespace DefaultNamespace.AI.Algorythms.MCTS
{
    public class MCTS
    {
        private Node root; // The root of the Monte Carlo Tree

        // private List<MCTSBoardSimulation> moveSequence;

        bool EnemyWinner = false;


        // private IEnumerator RunForDuration(MCTSBoardSimulation board, float howLong)
        // {
        //     float startTime = Time.realtimeSinceStartup;
        //
        //     while (true)
        //     {
        //         float currentTime = Time.realtimeSinceStartup;
        //         float elapsedTime = currentTime - startTime;
        //
        //         if (elapsedTime >= howLong)
        //         {
        //             break;
        //         }
        //
        //         // Your coroutine logic goes here
        //
        //         Debug.Log($"Elapsed Time: {elapsedTime:F2} seconds");
        //
        //         yield return null; // Yielding null makes the coroutine wait for the next frame
        //     }
        //
        //     Debug.Log("Coroutine finished");
        // }

        public Tuple<PieceSimulation, PieceSimulation> RunMCTSRoutine(MCTSBoardSimulation board)
        {
            for (int i = 0; i < 25; i++)
            {
                MCTSiteration(board);
            }

            return board.SelectMove(0);
        }


        public void MCTSiteration(MCTSBoardSimulation board)
        {

            //selection
            MCTSBoardSimulation current = board;
            List<MCTSBoardSimulation> moveSequence = new();
            moveSequence.Add(current);
            while (current.FullyExplored())
            {
                Tuple<PieceSimulation, PieceSimulation> move = current.SelectMove(1.4f);
                current.MokeMoveNoSim(move);
                moveSequence.Add(current);
            }

            //expansion
            Tuple<PieceSimulation, PieceSimulation> move2 = current.ChooseUnexploredMove();
            current.MokeMoveNoSim(move2);
            moveSequence.Add(current);

            //simulation
            EnemyWinner = PlayOut(current);

            //backpropagation
            foreach (MCTSBoardSimulation sim in moveSequence)
            {
                if (current == sim)
                {
                    if (EnemyWinner == current.IsenemyTurn())
                    {
                        current.wins++;
                    }
                    current.playouts++;
                }
            }
        }

        // private void Selection(MCTSBoardSimulation board)
        // {
        //     current = board;
        //     moveSequence = new List<MCTSBoardSimulation>();
        //     moveSequence.Add(current);
        //
        //
        //     while (current.FullyExplored())
        //     {
        //         Tuple<PieceSimulation, PieceSimulation> move = current.SelectMove(1.4f);
        //         current = current.MokeMove(move);
        //         moveSequence.Add(current);
        //     }
        // }
        //
        // public void Expansion()
        // {
        //     Tuple<PieceSimulation, PieceSimulation> move = current.ChooseUnexploredMove();
        //     current = current.MokeMove(move);
        //     moveSequence.Add(current);
        // }


        // private bool PlayOut(MCTSBoardSimulation mctsBoardSimulation)
        // {
        //     
        //     MCTSBoardSimulation board = new(mctsBoardSimulation, mctsBoardSimulation.IsenemyTurn());
        //     
        //     
        //     while (!board.IsGameOver())
        //     {
        //         Tuple<PieceSimulation, PieceSimulation> move = mctsBoardSimulation.SelectMove(1.4f);
        //
        //         board = board.MokeMove(move);
        //
        //         board.SwitchTurn();
        //         Debug.Log(board.IsenemyTurn());
        //     }
        //
        //     return board.IsEnemyWinner();
        // }

        private bool PlayOut(MCTSBoardSimulation mctsBoardSimulation)
        {
            MCTSBoardSimulation board = new(mctsBoardSimulation, mctsBoardSimulation.IsenemyTurn());

            bool IsGameOver = !board.IsGameOver();

            while (IsGameOver)
            {
                Tuple<PieceSimulation, PieceSimulation> move = board.SelectMove(1.4f);

                board.MokeMoveNoSim(move);

                board.SwitchTurn();
                Debug.Log(board.IsenemyTurn());
            }

            return board.IsEnemyWinner();
        }

        // void Backpropagation()
        // {
        //     foreach (MCTSBoardSimulation sim in moveSequence)
        //     {
        //         if (current == sim)
        //         {
        //             if (EnemyWinner == current.IsenemyTurn())
        //             {
        //                 current.wins++;
        //             }
        //             current.playouts++;
        //         }
        //     }
        // }

    }
}
