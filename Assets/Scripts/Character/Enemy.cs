using AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Character
{
    public class Enemy : Humanoid
    {

        private IObjectPool<Enemy> objectPool;
        private bool releasedToPool = false;
        protected override void Die()
        {
            Debug.Log(gameObject.name +" died.");

            GetComponent<EnemyAnimationManager>().SetDeathBool(true);
            
            ChangeComponentActivation(false);
            
            Invoke("ReleaseGameObject", 4.5f);
        }

        private void ReleaseGameObject()
        {
            if (releasedToPool) return;
            releasedToPool = true;
            objectPool.Release(this);
        }
        
        public void SetPool(IObjectPool<Enemy> pPool){
            objectPool = pPool;
        }

        private void OnEnable()
        {
            releasedToPool = false;
            AIGlobalManager.instance.OnPlayerDeath += Die;
            ChangeComponentActivation(true);
            GetComponent<Enemy_AI>().enabled = true;
        }

        private void OnDisable()
        {
            AIGlobalManager.instance.OnPlayerDeath -= Die;
        }

        public void ChangeComponentActivation(bool state)
        {
            Debug.Log("Changing Component Status from "+gameObject.name +" to "+state);
            GetComponent<Damageable>().enabled = state;
            GetComponent<Enemy_AI>().enabled = state;
            GetComponent<NavMeshAgent>().enabled = state;
           
        }
    }
}

