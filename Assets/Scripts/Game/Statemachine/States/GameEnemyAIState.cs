using System;
using System.Collections;
using UnityEngine;
namespace Game.Statemachine.States
{
    public class GameEnemyAIState : MonoBehaviour, IState
    {
        private Gameloop gameloop;
        private Board board;
        private SimpleStateMachine stateMachine;

        private void Awake()
        {
            gameloop = Gameloop.Instance;
            board = gameloop.Board;

            stateMachine = gameloop.stateMachine;
        }

        public void OnEnterState()
        {
            gameloop.popup.ShowPopup("Enemy turn! AI Is Thinking", 2f);
            StartCoroutine(EnemyTurnRoutine());
            gameloop.turnBasedTimer.SwitchPlayerTimer();
        }

        private PieceView attacker;
        private PieceView target;

        public IEnumerator EnemyTurnRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            
            Tuple<PieceView, PieceView> result = gameloop.enemyAiManager.GetCurrentBehaviour().FindTarget(board, board.EnemyPieces, board.PlayerPieces);

            yield return new WaitForSeconds(1.5f);

            attacker = result.Item1;
            target = result.Item2;

            gameloop.LR.positionCount = 2;
            gameloop.LR.SetPosition(0, attacker.transform.position);
            gameloop.LR.SetPosition(1, target.transform.position);

            gameloop.enemyAIButton.gameObject.SetActive(true);

            // attacker.PieceAttackAnimation(target, AttackCompleted);
        }

        public void ContinueButtonClocked()
        {
            gameloop.enemyAIButton.gameObject.SetActive(false);

            gameloop.LR.positionCount = 0;

            attacker.PieceAttackAnimation(target, AttackCompleted);
        }

        private void AttackCompleted()
        {
            board.CheckForDeadPieces();
            AttackAndDeathCompleted();

        }

        private void AttackAndDeathCompleted()
        {
            StartCoroutine(EndEncounterRoutine());
        }

        IEnumerator EndEncounterRoutine()
        {
            if (board.DidAnyPlayerLose())
            {
                yield return new WaitForSeconds(0.5f);
                switch (board.CheckWhoLost())
                {
                    case GameOutCome.PlayerWon:
                        gameloop.popup.ShowPopup("Game Over, you Win this round!");
                        break;
                    case GameOutCome.AIWon:
                        gameloop.popup.ShowPopup("Game Over, you Lose this round!");
                        break;
                    case GameOutCome.Draw:
                        gameloop.popup.ShowPopup("Game Over, it's a draw!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return new WaitForSeconds(1.5f);
                stateMachine.SetState(stateMachine.GameDestructionState);
            }
            else
            {
                gameloop.stateMachine.SetState(gameloop.stateMachine.GamePlayerState);
            }
        }

        public void OnExitState()
        {
            attacker = null;
            target = null;
        }
        public void UpdateState()
        {

        }


    }

}
