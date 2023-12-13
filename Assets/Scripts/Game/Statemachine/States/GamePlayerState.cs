using UnityEngine;
namespace Game.Statemachine.States
{
    public class GamePlayerState : MonoBehaviour, IState
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
            gameloop.popup.ShowPopup("Player turn!");
            gameloop.EnableDragAndDrop();
        }



        public void PlayerAttack(PieceView attacker, PieceView target)
        {
            gameloop.DisableDragAndDrop();

            attacker.PieceAttackAnimation(target, AttackCompleted);
        }

        private void AttackCompleted()
        {
            board.CheckForDeadPieces();

            AttackAndDeathCompleted();

        }

        private void AttackAndDeathCompleted()
        {
            if (board.DidAnyPlayerLose())
            {
                gameloop.stateMachine.SetState(gameloop.stateMachine.GameDestructionState);
            }
            else
            {
                gameloop.stateMachine.SetState(gameloop.stateMachine.GameEnemyAIState);
            }
        }

        public void OnExitState()
        {

        }
        public void UpdateState()
        {

        }

        public void ContinueButtonClocked()
        {

        }
    }

}
