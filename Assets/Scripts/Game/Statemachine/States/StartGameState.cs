using DefaultNamespace.AI.Algorythms.MCTStry3;
using GameAI.Algorithms.MonteCarlo;
using System;
using System.Linq;
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
        {
            Gameloop gameloop = Gameloop.Instance;
            
            gameloop.StartGameSequence.StartSequence();
            
            gameloop.StartGameSequence.TutorialCompleted += UiSequenceDone;
            
            // gameloop.StartScreen.SetActive(true);
        }
        private void UiSequenceDone(object sender, EventArgs e)
        {
            stateMachine.SetState(stateMachine.GameGenerationState);
            gameloop.StartGameSequence.TutorialCompleted -= UiSequenceDone;
            
           
            
          
        }
        public void OnExitState()
        {
            gameloop.StartGameSequence.gameObject.SetActive(false);
            
            
            // gameloop.StartScreen.SetActive(false);
        }
        public void UpdateState()
        {

        }
        public void ContinueButtonClocked()
        {
            // stateMachine.SetState(stateMachine.GameGenerationState);
        }
    }
}
