using DAE.GameSystem.Singleton;
using DefaultNamespace.AI;
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
    public bool wonLastCombat = false;
    
    // private int CurrentCombatencounter = 0;

    // public List<List<string>> questionnaireData = new List<List<string>>();
    public List<DataCollection> DataCollection = new List<DataCollection>();
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
        List<DataCollection> data = new List<DataCollection>();

        for (int i = 0; i < 10; i++)
        {
            DataCollection dc = new DataCollection();
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
    
    
}
