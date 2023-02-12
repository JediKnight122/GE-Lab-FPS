using System;
using GameMode;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;

namespace AI.StateMachine
{
    public class AIStateAttack : AIState
    {
        private readonly Weapon weapon;
        private bool crouching=false;
        
        public AIStateAttack(EnemyMovement pMovement, EnemyAnimationManager pAnimationManager,Weapon pWeapon)
        {
            movement = pMovement;
            animationManager = pAnimationManager;
            weapon = pWeapon;
            crouching = Convert.ToBoolean(Random.Range(0, 2));
        }
         
        public override void Execute()
        {
            
            // movement.MoveToPosition(GameManager.instance.GetPlayerPosition());
            movement.RotateTo(GameManager.instance.GetPlayerPosition());
            
            if (Random.Range(0, 1000) > 25) return;
            weapon.Fire(GameManager.instance.GetPlayerPosition());
            animationManager.SetShotFiredTrigger();
            animationManager.SetCrouchingBool(crouching);
        }
    }
}
