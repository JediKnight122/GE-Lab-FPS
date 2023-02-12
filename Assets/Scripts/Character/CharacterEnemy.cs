using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Character
{
    public class CharacterEnemy : CharacterBehaviour
    {
        
        [Tooltip("Enemy Version of the normal Camera")]
        [SerializeField]
        private Transform enemyViewpoint;
        
        public override Transform GetCameraWorld()
        {
            return transform;
        }

        public override InventoryBehaviour GetInventory()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsCrosshairVisible()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsRunning()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsCrouching()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsAiming()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsCursorLocked()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsTutorialTextVisible()
        {
            throw new System.NotImplementedException();
        }

        public override Vector2 GetInputMovement()
        {
            throw new System.NotImplementedException();
        }

        public override Vector2 GetInputLook()
        {
            throw new System.NotImplementedException();
        }
        public override void FillAmmunition(int amount)
        {
            throw new System.NotImplementedException();
        }

        public override void SetActiveMagazine(int active)
        {
            throw new System.NotImplementedException();
        }

        public override void AnimationEndedReload()
        {
            throw new System.NotImplementedException();
        }

        public override void AnimationEndedInspect()
        {
            throw new System.NotImplementedException();
        }

        public override void AnimationEndedHolster()
        {
            throw new System.NotImplementedException();
        }
    }
}
