using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerationState : MonoBehaviour, IState
{
    private Board board;
    private Gameloop gameloop;
    private SimpleStateMachine stateMachine;
   
    private void Awake()
    {
         gameloop = Gameloop.Instance;
        board = gameloop.Board;
        stateMachine = gameloop.stateMachine;
    }

    public void OnEnterState()
    {
        board.GenerateBoard();
        gameloop.enemyAiManager.GetRandomBehaviour();
        stateMachine.SetState(stateMachine.GameEnemyAIState);
    }
    public void OnExitState()
    {
    
    }

    //enemyTurn
    
    public void UpdateState()
    {
       
    }
    
    public void ContinueButtonClocked()
    {
            
    }
}
