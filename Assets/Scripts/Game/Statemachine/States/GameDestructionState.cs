using System.Collections;
using UnityEngine;
namespace Game.Statemachine.States
{
    public class GameDestructionState : MonoBehaviour, IState
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
            board.DestroyBoard();
            
            gameloop.turnBasedTimer.StopAllTimers();

            if (gameloop.IsFirstCombat)
            {
                gameloop.IsFirstCombat = false;
                StartCoroutine(FirstGamePauzeIntervall());
            }
            else
            {
                stateMachine.SetState(stateMachine.QuestionaireState);
            }
        }

        IEnumerator FirstGamePauzeIntervall()
        {
            gameloop.popup.ShowPopup("Okay that was it for your Warmup, now the real game begins!", 3f);
            yield return new WaitForSeconds(3f);
            stateMachine.SetState(stateMachine.GameGenerationState);
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
