using DefaultNamespace.AI;
using DefaultNamespace.ExcelWriting;
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
        gameloop.sliderViewDifficulty.SetNumber(5);
        gameloop.sliderViewFun.SetNumber(5);
        gameloop.sliderViewRealism.SetNumber(5);
        gameloop.sliderViewAiComplexity.SetNumber(5);
        // gameloop.stringGuess.SetInputFieldText("What kind of AI am I?");
        gameloop.stringGuess.setFunnyAiGuess();
        gameloop.stringGuess.resetValueChanged();
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
        DataCollection data = new DataCollection();
        // List <string> data = new List<string>();
        data.AddData(currentAI);
        if (gameloop.wonLastCombat)
        {
            data.AddData("1");
        }
        else
        {
            data.AddData("0");
        }
      
        data.AddData(gameloop.sliderViewDifficulty.GetNumber());
        data.AddData(gameloop.sliderViewFun.GetNumber());
        data.AddData(gameloop.sliderViewRealism.GetNumber());
        data.AddData(gameloop.sliderViewAiComplexity.GetNumber());
        data.AddData(gameloop.stringGuess.GetInputFieldTextIfValueChanged());
        
        gameloop.DataCollection.Add(data);

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
