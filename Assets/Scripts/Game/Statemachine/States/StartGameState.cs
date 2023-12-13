using UnityEngine;
namespace Game.Statemachine.States
{
    public class StartGameState : MonoBehaviour, IState
    {
        private Gameloop gameloop;
        private SimpleStateMachine stateMachine;

        private GameObject questionScreen;

        private void Awake()
        {
           gameloop = Gameloop.Instance;
            stateMachine = gameloop.stateMachine;
            questionScreen = gameloop.QuestionScreen;
        }

        public void OnEnterState()
        {Gameloop gameloop = Gameloop.Instance;
            gameloop.StartScreen.SetActive(true);
        }
        public void OnExitState()
        {
            gameloop.StartScreen.SetActive(false);
        }
        public void UpdateState()
        {
         
        }
        public void ContinueButtonClocked()
        {
            stateMachine.SetState(stateMachine.GameGenerationState);
        }
    }
}