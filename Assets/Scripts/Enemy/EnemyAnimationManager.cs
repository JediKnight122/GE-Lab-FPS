using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;


    public class EnemyAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent aiAgent;
        void Start()
        {
        
        }
        // Update is called once per frame
        void Update()
        {
            Vector3 currentVelocity = aiAgent.velocity;
            currentVelocity= math.abs(currentVelocity);

            animator.SetFloat("Velocity Y",currentVelocity.x);
            animator.SetFloat("Velocity X",currentVelocity.y);
        }

        public void SetShotFiredTrigger()
        {
            Debug.Log("Shot Trigger set.");
            animator.SetTrigger("ShotFired");
        }

        public void SetHitTrigger()
        {
            Debug.Log("Hit Trigger set.");
            animator.SetTrigger("Hit");
        }

        public void SetDeathBool(bool state)
        {
            animator.SetBool("Dead", state);
        }

        public void SetCrouchingBool(bool state)
        {
            animator.SetBool("Crouching", state);
        }
    }