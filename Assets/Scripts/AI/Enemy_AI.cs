using Character;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class Enemy_AI : MonoBehaviour
    {
        private AIBehaviourState aiState;

        private EnemyMovement movement;
        // Start is called before the first frame update
        void Start()
        {
            aiState = AIBehaviourState.Idle;
            movement = GetComponent<EnemyMovement>();
        }

        public void SwitchState(AIBehaviourState newState)
        {
            aiState = newState;
        }
        
        // Update is called once per frame
        void Update()
        {
            movement.MoveToPosition(FindObjectOfType<CharacterPlayer>().gameObject.transform.position);
            
            if (aiState == AIBehaviourState.Idle)
            {
                
            }
            else if (aiState == AIBehaviourState.ChasePlayer)
            {
                
            }
            else if (aiState == AIBehaviourState.Attack)
            {
                
            }
            
        }
    }
}
