using System;
using System.Collections;
using AI.StateMachine;
using Character;
using GameMode;
using InfimaGames.LowPolyShooterPack;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class Enemy_AI : MonoBehaviour
    {
        
        private AIState aiStateIdle;
        private AIState aiStateChase;
        private AIState aiStateAttack;

        private AIState activeAIState;
        
        
        private EnemyMovement movement;

        [SerializeField] private float attackRange=10;
        [SerializeField] private float minimalAttackRange=3;
        [SerializeField] private float recognitionRange=15;
        
        [SerializeField] private Transform IK_Target;
        [SerializeField] private Transform eye;
        
        private AIRangeDetection[] RangeTrigger;
        private EnemyAnimationManager animationManager;

        private Coroutine ikSetterRoutine;
        void Start()
        {
            movement = GetComponent<EnemyMovement>();
            animationManager = GetComponent<EnemyAnimationManager>();

            #region AIStates instanziieren
            
            aiStateIdle = new AIStateIdle();
            aiStateChase = new AIStateChase(movement, animationManager);
            aiStateAttack = new AIStateAttack(movement,animationManager,GetComponentInChildren<Weapon>());

            activeAIState = aiStateIdle;
            
            #endregion

            
                
            #region Trigger-Setzen
            //Trigger sind: Erkennung-Reichweite, Attack-Reichweite
            /*
            RangeTrigger = GetComponentsInChildren<AIRangeDetection>();
            
            RangeTrigger[0].SetColliderRadius(recognitionRange);
            RangeTrigger[1].SetColliderRadius(attackRange);

            RangeTrigger[0].TriggerEntered += SwitchToChase;
            RangeTrigger[1].TriggerEntered += SwitchToAttack;
            
            RangeTrigger[0].TriggerExited += SwitchToIdle;
            RangeTrigger[1].TriggerExited += SwitchToChase;
            */
            #endregion
            

        }

        private void FixedUpdate()
        {
           
            float distanceToPlayer = Vector3.Distance(GameManager.instance.GetPlayerPosition(), transform.position);
           
            if (distanceToPlayer <= recognitionRange)
            {
                bool canSeePlayer = false;
            
                
                if(Physics.Raycast(eye.position, GameManager.instance.GetPlayerPosition() - eye.position, out RaycastHit hit,distanceToPlayer))
                {
                    if (hit.collider.tag.Equals("Player") || distanceToPlayer >=minimalAttackRange)
                    {
                        canSeePlayer = true;
                    }
                }
                if(distanceToPlayer<=attackRange && canSeePlayer) SwitchToAttack();
                else SwitchToChase();
            }
            else SwitchToIdle();
            
            
            activeAIState.Execute();
        }

       private IEnumerator UpdateIKTarget()
        {
            IK_Target.position = GameManager.instance.GetPlayerPosition()+Vector3.up*1.5f;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(UpdateIKTarget());
        }
        private void SwitchToIdle()
        {
            movement.StopMovement();
            activeAIState =  aiStateIdle;
        }
        private void SwitchToChase()
        {
            if(ikSetterRoutine!= null) StopCoroutine(ikSetterRoutine);
            activeAIState =  aiStateChase;
        }
        private void SwitchToAttack()
        {
            if (ikSetterRoutine == null)
            {
                ikSetterRoutine = StartCoroutine(UpdateIKTarget());
            }
            movement.StopMovement();
            activeAIState = aiStateAttack;
        }
    }
}
