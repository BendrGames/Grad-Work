using DefaultNamespace.AI.aiBehaviours;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace.AI
{
    public class EnemyAiManager
    {
        Dictionary<AItypes, AIBehaviourBase> behaviours = new Dictionary<AItypes, AIBehaviourBase>();
        private List<AItypes> AllreadyTestedBehviours = new List<AItypes>();
        
        private AItypes currentBehaviourtype;
        private AIBehaviourBase currentBehaviour;
      
        public EnemyAiManager()
        {
            behaviours.Add(AItypes.behaviour1, new AiRandomBehaviour());
            behaviours.Add(AItypes.behaviour2, new AiRandomBehaviour());
            behaviours.Add(AItypes.behaviour3, new AiRandomBehaviour());
            behaviours.Add(AItypes.behaviour4, new AiRandomBehaviour());
            behaviours.Add(AItypes.behaviour5, new AiRandomBehaviour());
        }
        
        public AIBehaviourBase GetRandomBehaviour()
        {
            int random = Random.Range(0, behaviours.Count);
            KeyValuePair<AItypes, AIBehaviourBase> randomentry = GetRandomEntry(behaviours);
            
            
            
            AllreadyTestedBehviours.Add(randomentry.Key);
            currentBehaviourtype = randomentry.Key;
            currentBehaviour = randomentry.Value;
            return randomentry.Value;
        }

        public bool IsTestedAllBehaviours()
        {
            return behaviours.Count == AllreadyTestedBehviours.Count;

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
            List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>(dictionary);

            // Get a random index
            int randomIndex = Random.Range(0, list.Count);

            // Return the random entry
            return list[randomIndex];
        }
    }
    
    public enum AItypes
    {
        behaviour1,
        behaviour2,
        behaviour3,
        behaviour4,
        behaviour5,
    }
}
