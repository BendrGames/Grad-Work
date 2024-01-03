using DAE.GameSystem.Singleton;
using DefaultNamespace.AI;
using DefaultNamespace.AI.Algorythms.MCTS;
using DefaultNamespace.AI.Algorythms.MCTStry3;
using DefaultNamespace.ExcelWriting;
using DefaultNamespace.UI;
using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Gameloop : SingletonMonoBehaviour<Gameloop>
{
    [Header("Timer")]
    public TurnBasedTimer turnBasedTimer;
    
    public SimpleStateMachine stateMachine;
    public Board Board;
    public EnemyAiManager enemyAiManager;
    public DragAndDropManager dragAndDropManager;
    public LineRenderer LR;
    
    [Header("QuestionScreen")]
    public GameObject QuestionScreen;
    
    public SliderView sliderViewDifficulty;
    public SliderView sliderViewFun;
    public SliderView sliderViewRealism;
    public SliderView sliderViewAiComplexity;
    public TextBoxView stringGuess;
    public Button DoneButton;
    
    [Header("QuestionScreen")]
    public GameObject GameOverScreen;
    public GameObject StartScreen;
    
    [Header("enemyAIButton")]
    public GameObject enemyAIButton;

    [Header("enemyAIButton")]
    public Popup popup;
    
    [Header("UiSequences")]
    public TutorialSequence StartGameSequence;

    [HideInInspector]
    public GameOutCome gameOutcome = GameOutCome.Draw;
    
    
    public bool IsFirstCombat = true;

    
    public MCTS MCTSRunner; 
    
    // private int CurrentCombatencounter = 0;

    // public List<List<string>> questionnaireData = new List<List<string>>();
    public List<DataCollection> DataCollection = new();
    private void Awake()
    {
       
        enemyAiManager = new EnemyAiManager();
    }


    private void Start()
    {
        stateMachine.SetState(stateMachine.StartGameState);

        
        
        // TestWriteCSV();
    }
    
    public void EnableDragAndDrop()
    {
        dragAndDropManager.enabled = true;
    }
    public void DisableDragAndDrop()
    {
        dragAndDropManager.enabled = false;
    }
    
    public void DoneButtonClicked()
    {
        stateMachine.ContinueButtonClicked();
    }
    
    private void TestWriteCSV()
    {
        List<DataCollection> data = new();

        for (int i = 0; i < 10; i++)
        {
            DataCollection dc = new();
            dc.AddData("AI" + i);
            dc.AddData("1");
            dc.AddData("2");
            dc.AddData("3");
            dc.AddData(i.ToString());
            
            data.Add(dc);
        }
        
        List<DataCollection> printout = data.OrderBy(dc => dc.GetData().FirstOrDefault()).ToList();
        
        CSVWriter.WriteDataToCsv(printout);
    }
    
    public void QuitButtonClicked()
    {
             #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
