using DefaultNamespace.AI;
using DefaultNamespace.ExcelWriting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        DataCollection data = new();
        // List <string> data = new List<string>();
        data.AddData(currentAI);

        data.AddData(gameloop.gameOutcome.ToString());
        Debug.Log(gameloop.gameOutcome.ToString());
        data.AddData(gameloop.sliderViewDifficulty.GetNumber());
        data.AddData(gameloop.sliderViewFun.GetNumber());
        data.AddData(gameloop.sliderViewRealism.GetNumber());
        data.AddData(gameloop.sliderViewAiComplexity.GetNumber());
        string inputFieldTextIfValueChanged = gameloop.stringGuess.GetInputFieldTextIfValueChanged();
        inputFieldTextIfValueChanged =  ReplaceCommas(inputFieldTextIfValueChanged, '_');
        data.AddData(inputFieldTextIfValueChanged);

        string totalTime = gameloop.turnBasedTimer.GetTotalTime().ToString();
        data.AddData(totalTime);
        string enemyTime = gameloop.turnBasedTimer.GetEnemyAnalyzingTime().ToString();
        data.AddData(enemyTime);
        string playerTime = gameloop.turnBasedTimer.GetPlayerThinkTime().ToString();
        data.AddData(playerTime);

        gameloop.DataCollection.Add(data);
        // gameloop.DataCollection.Insert(0, data);

        List<DataCollection> dataToPrint = gameloop.DataCollection;
        // List<DataCollection> printout = dataToPrint.OrderBy(dc => dc.GetData().FirstOrDefault()).ToList();

        CSVWriter.WriteDataToCsv(dataToPrint);

        if (gameloop.enemyAiManager.IsTestedAllBehaviours())
        {
            stateMachine.SetState(stateMachine.GameOverState);
        }
        else
        {
            stateMachine.SetState(stateMachine.GameGenerationState);
        }
    }
    
    public static string ReplaceCommas(string input, char replacementChar)
    {
        // Check if the input string contains commas
        if (input.Contains(","))
        {
            // Replace commas with the specified character
            input = input.Replace(",", replacementChar.ToString());
        }

        return input;
    }
}
