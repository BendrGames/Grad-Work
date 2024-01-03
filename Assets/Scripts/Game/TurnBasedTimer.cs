using UnityEngine;
namespace Game
{
    public class TurnBasedTimer : MonoBehaviour
    {
        private float totalTime;
        private float playerThinkTime;
        private float enemyAnalyzingTime;

        private bool isTotalTimerRunning;
        private bool isPlayerThinkTimerRunning;
        private bool isEnemyAnalyzingTimerRunning;

        private float totalTimerStartTime;
        private float playerThinkTimerStartTime;
        private float enemyAnalyzingTimerStartTime;

        void Start()
        {
            // Optionally, you can start the timers here if needed.
        }

        void Update()
        {
            if (isTotalTimerRunning)
            {
                totalTime += Time.deltaTime;
            }

            if (isPlayerThinkTimerRunning)
            {
                playerThinkTime += Time.deltaTime;
            }

            if (isEnemyAnalyzingTimerRunning)
            {
                enemyAnalyzingTime += Time.deltaTime;
            }
        }

        public void StartTotalTimer()
        {
            if (!isTotalTimerRunning)
            {
                // totalTimerStartTime = Time.time;
                isTotalTimerRunning = true;
            }
        }

        public void StartPlayerThinkTimer()
        {
            if (!isPlayerThinkTimerRunning)
            {
                // playerThinkTimerStartTime = Time.time;
                isPlayerThinkTimerRunning = true;
            }
        }

        public void StartEnemyAnalyzingTimer()
        {
            if (!isEnemyAnalyzingTimerRunning)
            {
                // enemyAnalyzingTimerStartTime = Time.time;
                isEnemyAnalyzingTimerRunning = true;
            }
        }

        public void SwitchPlayerTimer()
        {
            if (isPlayerThinkTimerRunning)
            {
                PausePlayerThinkTimer();
                StartEnemyAnalyzingTimer();
            }
            else if (isEnemyAnalyzingTimerRunning)
            {
                PauseEnemyAnalyzingTimer();
                StartPlayerThinkTimer();
            }
        }

        private void PausePlayerThinkTimer()
        {
            if (isPlayerThinkTimerRunning)
            {
                // playerThinkTime += Time.time - playerThinkTimerStartTime;
                isPlayerThinkTimerRunning = false;
            }
        }

        private void PauseEnemyAnalyzingTimer()
        {
            if (isEnemyAnalyzingTimerRunning)
            {
                // enemyAnalyzingTime += Time.time - enemyAnalyzingTimerStartTime;
                isEnemyAnalyzingTimerRunning = false;
            }
        }

        public void StopAllTimers()
        {
            isTotalTimerRunning = false;
            isPlayerThinkTimerRunning = false;
            isEnemyAnalyzingTimerRunning = false;
        }

        public float GetTotalTime()
        {
            return totalTime;
        }

        public float GetPlayerThinkTime()
        {
            return playerThinkTime;
        }

        public float GetEnemyAnalyzingTime()
        {
            return enemyAnalyzingTime;
        }

        public void ResetTimers()
        {
            totalTime = 0f;
            playerThinkTime = 0f;
            enemyAnalyzingTime = 0f;
            isTotalTimerRunning = false;
            isPlayerThinkTimerRunning = false;
            isEnemyAnalyzingTimerRunning = false;
        }
    }
}
