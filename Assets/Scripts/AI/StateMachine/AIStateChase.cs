
using System;
using GameMode;
using UnityEngine;

namespace AI.StateMachine
{
    public class AIStateChase : AIState
    {
        private readonly EnemyMovement movement;

        public AIStateChase(EnemyMovement pMovement, EnemyAnimationManager pAnimationManager)
        {
            movement = pMovement;
            animationManager = pAnimationManager;
        }
        public override void Execute()
        {
            movement.MoveToPosition(GameManager.instance.GetPlayerPosition());
            animationManager.SetCrouchingBool(false);
            
        }
    }
}
