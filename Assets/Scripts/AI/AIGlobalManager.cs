using System;
using UnityEngine;

namespace AI
{
    public class AIGlobalManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        
        public static AIGlobalManager instance;

        public event Action OnPlayerDeath;
        private void Awake()
        {
            if (instance != null && instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                instance = this; 
            } 
        }

        public void InvokePlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }
        public GameObject GetEnemyPrefab()
        {
            return enemyPrefab;
        }
        
    }
}
