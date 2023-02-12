using GameMode;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent agent; 
    
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPosition(Vector3 position)
        {
            agent.destination = position;
        }

        public void StopMovement()
        {
            agent.destination = transform.position;
        }

        public void RotateTo(Vector3 pTargetPosition)
        {
            Vector3 temp = pTargetPosition - new Vector3(0, -45, 0);
        
            // agent.transform.LookAt(new Vector3(0,temp.y,0));
            agent.transform.LookAt(GameManager.instance.GetPlayerTransform());
            //agent.transform.rotation.SetLookRotation(Vector3.RotateTowards(agent.transform.rotation.eulerAngles, pTargetPosition, 0, 1));
        }
    }
}
