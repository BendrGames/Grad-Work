using DefaultNamespace.AI;
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

        if (gameloop.IsFirstCombat)
        {
            gameloop.enemyAiManager.SetRandomBehaviour();
        }
        else
        {
            gameloop.enemyAiManager.SetRandomBehaviourAndRemove();
        }


        // gameloop.enemyAiManager.SetSpecificBehaviour(AItypes.AIMCTSWin);

        stateMachine.SetState(stateMachine.GameEnemyAIState);
        // stateMachine.SetState(stateMachine.GamePlayerState);
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
