using DefaultNamespace.AI.aiBehaviours;
using DefaultNamespace.AI.aiBehaviours.complex;
using DefaultNamespace.AI.aiBehaviours.high_medium;
using DefaultNamespace.AI.aiBehaviours.Medium;
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
            behaviours.Add(AItypes.AiRandomBehaviour, new AiRandomBehaviour());
            behaviours.Add(AItypes.AiRandomALowestHealthT, new AiRandomALowestHealthT());
            behaviours.Add(AItypes.AIRandomAAttackAndHealthT, new AIRandomAAttackAndHealthT());
            behaviours.Add(AItypes.AIRandomADangerEvalT, new AIRandomADangerEvalT());
            behaviours.Add(AItypes.AIMiniMaxBehaviour, new AiMiniMaxBehaviour());
            behaviours.Add(AItypes.AIStraightAheadBehaviour, new AIStraightAheadBehaviour());
            behaviours.Add(AItypes.AIMCTSBehaviour, new AIMCTSBehaviour());
            
        }
        
        public AIBehaviourBase SetRandomBehaviour()
        {
            int random = Random.Range(0, behaviours.Count);
            KeyValuePair<AItypes, AIBehaviourBase> randomentry = GetRandomEntry(behaviours);

            AllreadyTestedBehviours.Add(randomentry.Key);
            currentBehaviourtype = randomentry.Key;
            currentBehaviour = randomentry.Value;
            return randomentry.Value;
        }
        
        public AIBehaviourBase SetSpecificBehaviour(AItypes type)
        {
            currentBehaviourtype = type;
            currentBehaviour = behaviours[type];
            return behaviours[type];
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
        AiRandomBehaviour,
        AiRandomALowestHealthT,
        AIRandomAAttackAndHealthT,
        AIRandomADangerEvalT,
        AIMiniMaxBehaviour,
        AIStraightAheadBehaviour,
        AIMCTSBehaviour,
    }
}
