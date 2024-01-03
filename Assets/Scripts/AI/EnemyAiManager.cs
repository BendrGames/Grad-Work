using DefaultNamespace.AI.aiBehaviours;
using DefaultNamespace.AI.aiBehaviours.complex;
using DefaultNamespace.AI.aiBehaviours.high_medium;
using DefaultNamespace.AI.aiBehaviours.Medium;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace DefaultNamespace.AI
{
    public class EnemyAiManager
    {
        Dictionary<AItypes, AIBehaviourBase> behaviours = new();
        private List<AItypes> AllreadyTestedBehviours = new();

        private AItypes currentBehaviourtype;
        private AIBehaviourBase currentBehaviour;
        
        public static int DeepSearchIterationMultiplyer = 500;
        public static int UndeepSearchIterationMultiplyer = 5;

        public EnemyAiManager()
        {
            //Low Complexity/meme
            behaviours.Add(AItypes.AiRandomBehaviour, new AiRandomBehaviour());
            behaviours.Add(AItypes.AIStraightAhead, new AIStraightAheadBehaviour());
            behaviours.Add(AItypes.AIAlphabetical, new AIAlphabeticalBehaviour());
            
            //simple board evaluating AI 
            behaviours.Add(AItypes.AIRandomAAttackAndHealthT, new AIRandomAAttackAndHealthT());
            behaviours.Add(AItypes.AIRandomADangerEvalT, new AIRandomADangerEvalT());
            
            //simple winning designer made AI
            behaviours.Add(AItypes.AiLowestHealthTBestAttacker, new AiLowestHealthTBestAttacker());
            behaviours.Add(AItypes.AIBestHighHealthKillPotential, new AIBestHighHealthKillPotential());
            
            //Deep search
            behaviours.Add(AItypes.AIMCTSDeepWin, new AIMCTSDeepWinBehaviour());
            behaviours.Add(AItypes.AIMCTSWin, new AIMCTSWinBehaviour());
            
            //Hustling AI
            behaviours.Add(AItypes.AIMCTSSelfBalancing, new AIMCTSHustling());
        }

        public void SetRandomBehaviour()
        {
            int random = Random.Range(0, behaviours.Count);
            KeyValuePair<AItypes, AIBehaviourBase> randomentry = GetRandomEntry(behaviours);

            AllreadyTestedBehviours.Add(randomentry.Key);
            currentBehaviourtype = randomentry.Key;
            currentBehaviour = randomentry.Value;
            
            Debug.Log(currentBehaviour + " Not removing though" );
            
           
        }
        
        public void SetRandomBehaviourAndRemove()
        {
            int random = Random.Range(0, behaviours.Count);
            KeyValuePair<AItypes, AIBehaviourBase> randomentry = GetRandomEntry(behaviours);

            AllreadyTestedBehviours.Add(randomentry.Key);
            currentBehaviourtype = randomentry.Key;
            currentBehaviour = randomentry.Value;
            
            Debug.Log(currentBehaviour);
            
            behaviours.Remove(randomentry.Key);
        }

        public AIBehaviourBase SetSpecificBehaviour(AItypes type)
        {
            currentBehaviourtype = type;
            currentBehaviour = behaviours[type];
            return behaviours[type];
        }


        public bool IsTestedAllBehaviours()
        {
            if (behaviours.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetCurrentBehaviourString()
        {
            return currentBehaviour.ToString();
        }

        public AItypes GetCurrentBehaviourType()
        {
            return currentBehaviourtype;
        }

        public AIBehaviourBase GetCurrentBehaviour()
        {
            return currentBehaviour;
        }

        KeyValuePair<TKey, TValue> GetRandomEntry<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            // Convert dictionary to a list of key-value pairs
            List<KeyValuePair<TKey, TValue>> list = new(dictionary);

            // Get a random index
            int randomIndex = Random.Range(0, list.Count);

            // Return the random entry
            return list[randomIndex];
        }
        
      
    }

    public enum AItypes
    {
        AiRandomBehaviour,
        AiLowestHealthTBestAttacker,
        AIRandomAAttackAndHealthT,
        AIRandomADangerEvalT,
        AIStraightAhead,
        AIMCTSDeepWin,
        AIMCTSWin,
        AIMCTSSelfBalancing,
        AIBestHighHealthKillPotential,
        AIAlphabetical

    }
}
