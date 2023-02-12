using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace GameMode
{
    public class CaptureZone : MonoBehaviour
    {
        [SerializeField] private float captureTime;
        [SerializeField] private GameObject zoneCapturedEffectCone = null;
        [SerializeField] private GameObject zoneCapturedEffectBottom = null;
        [SerializeField] private Transform respawnPoint;
        
        [Header("Audio")]
        [SerializeField] private AudioSource zoneCaptured;
        [SerializeField] private AudioSource zoneCapturing;
        [SerializeField] private AudioSource zoneCapturingIntervention;

        public UnityEvent OnCaptureStart;
        public UnityEvent OnCaptureFinish;
        
        private bool aiAttackStarted = false;
        private bool playerInsideZone = false;
        private bool enemyInsideZone = false;
        private float currentCaptureProgression = 0;

        private Material zoneCaptureEffectGround;
        
        private void Start()
        {
            GameManager.instance.AddZone(this);
            zoneCaptureEffectGround = new Material(zoneCapturedEffectBottom.GetComponent<MeshRenderer>().material);
            zoneCapturedEffectBottom.GetComponent<MeshRenderer>().material = zoneCaptureEffectGround;
        }

    
        // Update is called once per frame
        void Update()
        {
            if (playerInsideZone && !enemyInsideZone)
            {
                if (!aiAttackStarted)
                {
                    aiAttackStarted = true;
                    OnCaptureStart?.Invoke();

                    
                    //Action Level 0-3 aus Eroberungszeit ausrechnen
                    bool temp = captureTime >= 20;
                    int actionLevel = Convert.ToInt32(temp)+1;
                    
                    MusicManager.instance.ChangeActionLevel(actionLevel);
                }
                
                
                currentCaptureProgression += Time.deltaTime;
                
                if(zoneCapturingIntervention.isPlaying)zoneCapturingIntervention.Stop();
                if(!zoneCapturing.isPlaying) zoneCapturing.Play();
                
                UIManager.instance.UpdateZoneCaptureProgression(currentCaptureProgression);
                zoneCaptureEffectGround.SetFloat("_Change_Slider",currentCaptureProgression/captureTime);
                
                if(currentCaptureProgression>=captureTime) ZoneCaptured();
            }
            else if (playerInsideZone && enemyInsideZone)
            {
                UIManager.instance.ZoneCaptureHolded();
                if(!zoneCapturingIntervention.isPlaying)zoneCapturingIntervention.Play();
                if(zoneCapturing.isPlaying) zoneCapturing.Stop();
            }
        
        }

        private void OnTriggerEnter(Collider collider)
        {
      
            if (collider.gameObject.tag.Equals("Player"))
            {
                playerInsideZone = true;
                UIManager.instance.ShowZoneCapture(captureTime);
            }
        }

        private void OnTriggerStay(Collider other)
        {
        
       
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemyInsideZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                playerInsideZone = false;
                UIManager.instance.UnShowZoneCapture();
                if(zoneCapturing.isPlaying) zoneCapturing.Stop();
            }
        
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemyInsideZone = false;
            }
        }

        private void ZoneCaptured()
        {
            Debug.Log("A zone was captured.");
            //zoneCapturedEffectCone.SetActive(true);
            MusicManager.instance.ChangeActionLevel(0);
            zoneCapturing.Stop();
            zoneCaptured.Play();
            
            RespawnManager.instance.SetNewSpawnPoint(respawnPoint);
            GameManager.instance.RemoveZone(this);
            UIManager.instance.ZoneCaptureSucces();
            
            OnCaptureFinish?.Invoke();
            
            this.enabled = false;
        }
    }
}
