using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public List<Transform> enemySpawnpoints;

        [SerializeField] private int difficultyLevel = 1;
        [SerializeField] private int maxNumberEnemies = 7;

        private bool attackActive = false;
        private int numActiveEnemies = 0;
        
        private void FixedUpdate()
        {
            if (!attackActive || numActiveEnemies>=maxNumberEnemies) return;
            
            if (Random.Range(0, 15000) <= difficultyLevel)
            {
               /* GameObject temp= Instantiate(enemyPrefab, enemySpawnpoints[Random.Range(0, enemySpawnpoints.Count)].position,
                    enemySpawnpoints[0].rotation);
*/
               Enemy temp = EnemyObjectPoolManager.instance.pool.Get();
               
               temp.GetComponent<NavMeshAgent>().Warp(enemySpawnpoints[Random.Range(0, enemySpawnpoints.Count)].position);
               temp.GetComponent<Health>().OnHealthDepleted += LowerEnemyCount;
               numActiveEnemies++;
            }

        }

        private void LowerEnemyCount()
        {
            numActiveEnemies--;
        }
        public void StartWave()
        {
            attackActive = true;
        }

        public void StopWave()
        {
            attackActive = false;
        }
    }
}
