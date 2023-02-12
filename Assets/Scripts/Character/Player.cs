using AI;
using Audio;
using GameMode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class Player : Humanoid
    {
        [SerializeField] private Animator animator;
        [SerializeField] private RandomAudioListPlayer deathAudioPlayer;
        protected override void Die()
        {
            Debug.Log("Player has Died.");
            ChangeIsDead(true);
            animator.Play("Death");
            Invoke("CallForRespawn", 3);
            AIGlobalManager.instance.InvokePlayerDeath();
            VoicelineManager.instance.PlayPlayerDeathVoiceline();
            deathAudioPlayer.Play();
        }

        private void CallForRespawn()
        {
            
            RespawnManager.instance.Respawn(gameObject);
            UIManager.instance.ShowPlayerDeath(false);
            ChangeIsDead(false);
            GetComponent<Health>().PrintHealthToUI();
        }

        private void ChangeIsDead(bool state)
        {
            UIManager.instance.ShowPlayerDeath(state);
            animator.enabled = state;
            GetComponent<PlayerInput>().enabled = !state;
            GetComponent<Health>().enabled = !state;
        }
        
    }
}
