using DefaultNamespace.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionaireState : MonoBehaviour, IState
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
        questionScreen.SetActive(true);
    }

    public void OnExitState()
    {
        questionScreen.SetActive(false);
    }

    public void UpdateState()
    {
       
    }
    
    public void ContinueButtonClocked()
    {
        string currentAI = gameloop.enemyAiManager.GetCurrentBehaviourString();
        
        List <string> data = new List<string>();
        data.Add(currentAI);
        if (gameloop.wonLastCombat)
        {
            data.Add("1");
        }
        else
        {
            data.Add("0");
        }
      
        data.Add(gameloop.sliderViewDifficulty.GetNumber());
        data.Add(gameloop.sliderViewFun.GetNumber());
        data.Add(gameloop.sliderViewRealism.GetNumber());
        
        gameloop.questionnaireData.Add(data);

        if (gameloop.enemyAiManager.IsTestedAllBehaviours())
        {
            stateMachine.SetState(stateMachine.GameOverState);
        }
        else
        {
            stateMachine.SetState(stateMachine.GameGenerationState);
        }
    }
}
