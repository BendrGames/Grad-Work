using Game.Statemachine.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStateMachine : MonoBehaviour
{
    [HideInInspector]
    public GameGenerationState GameGenerationState;
    [HideInInspector]
    public GameDestructionState GameDestructionState;
    [HideInInspector]
    public GamePlayerState GamePlayerState;
    [HideInInspector]
    public GameEnemyAIState GameEnemyAIState;
    [HideInInspector]
    public QuestionaireState QuestionaireState;
    [HideInInspector]
    public GameOverState GameOverState;
    [HideInInspector]
    public StartGameState StartGameState;

    public string CurrentStateName;
    private IState currentState;

    private void Awake()
    {
        // Instantiate the states
        GameGenerationState = gameObject.AddComponent<GameGenerationState>();
        GameDestructionState = gameObject.AddComponent<GameDestructionState>();
        GamePlayerState = gameObject.AddComponent<GamePlayerState>();
        GameEnemyAIState = gameObject.AddComponent<GameEnemyAIState>();
        
        QuestionaireState = gameObject.AddComponent<QuestionaireState>();
        GameOverState = gameObject.AddComponent<GameOverState>();
        StartGameState = gameObject.AddComponent<StartGameState>();

        // Initialize the state machine
    }

    

    private void Update()
    {
        // Update logic for the current state
        currentState.UpdateState();
    }

    public void SetState(IState newState)
    {
        // Exit logic for the current state
        if (currentState != null)
        {
            currentState.OnExitState();
        }

        // Enter logic for the new state
        currentState = newState;
        CurrentStateName = newState.GetType().Name;
        currentState.OnEnterState();
    }
    
    public void ContinueButtonClicked()
    {
        currentState.ContinueButtonClocked();
    }
}
