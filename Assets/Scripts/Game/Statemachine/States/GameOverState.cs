using DefaultNamespace.ExcelWriting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game.Statemachine.States
{
    public class GameOverState : MonoBehaviour, IState
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
            List<DataCollection> data = gameloop.DataCollection;
            
            List<DataCollection> printout = data.OrderBy(dc => dc.GetData().FirstOrDefault()).ToList();
            
            CSVWriter.WriteDataToCsv(printout);
            
            gameloop.GameOverScreen.SetActive(true);
        }
        public void OnExitState()
        {

        }
        public void UpdateState()
        {

        }
        public void ContinueButtonClocked()
        {
             #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }
}
