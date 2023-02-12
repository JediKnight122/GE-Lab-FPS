using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GameMode
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager instance;

        private void Awake()
        {
            if (instance != null && instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                instance = this; 
            } 
        }
        
        private Transform playerTransform;

        private List<CaptureZone> uncapturedZones = new List<CaptureZone>();

        
        #region Zone-Management

        public void AddZone(CaptureZone newZone)
        {
            uncapturedZones.Add(newZone);
        }
        public void RemoveZone(CaptureZone oldZone)
        {
            uncapturedZones.Remove(oldZone);
            if (uncapturedZones.Count == 0)
            {
                CompleteLevel();
            }
            playerTransform.GetComponentInChildren<Health>().ResetHealth(true);
        }
    

        #endregion
    
    
    
        

        private void CompleteLevel()
        {
            Debug.Log("Level cleared. All zones have been Captured.");
            GetComponentInChildren<RandomAudioListPlayer>().Play();
            UIManager.instance.ShowMissionComplete();
            Invoke("LoadMainMenu", 6);

        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }
        private void Start()
        {
            playerTransform = FindObjectOfType<PlayerInput>().gameObject.transform;
        }

        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }

        public Transform GetPlayerTransform()
        {
            return playerTransform;
        }
    }
}
