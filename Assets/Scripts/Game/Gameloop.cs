using DAE.GameSystem.Singleton;
using DefaultNamespace.AI;
using DefaultNamespace.UI;
using Game;
using System;
using System.Collections;
using System.Collections.Generic;
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

    [HideInInspector]
    public bool wonLastCombat = false;
    
    // private int CurrentCombatencounter = 0;

    public List<List<string>> questionnaireData = new List<List<string>>();

    private void Awake()
    {
       
        enemyAiManager = new EnemyAiManager();
    }
    
    
    
    private void Start()
    {
        stateMachine.SetState(stateMachine.StartGameState);
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
    
    
  
    
}
