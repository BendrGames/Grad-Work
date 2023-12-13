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
            Gameloop gameloop = Gameloop.Instance;
            board = gameloop.Board;
        stateMachine = gameloop.stateMachine;
        
        }

        public void OnEnterState()
        {
            board.DestroyBoard();
            stateMachine.SetState(stateMachine.QuestionaireState);
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
