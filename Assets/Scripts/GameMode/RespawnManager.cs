using System;
using Audio;
using UnityEngine;

namespace GameMode
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager instance;
        
        [SerializeField] private Transform lastSpawnpoint;

        [SerializeField] private GameObject playerPrefab;

       
        // Start is called before the first frame update
        public void Respawn(GameObject player)
        {
            player.transform.position = lastSpawnpoint.position;
            player.transform.rotation = lastSpawnpoint.rotation;
           
        }

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

        public void SetNewSpawnPoint(Transform pTransform)
        {
            lastSpawnpoint = pTransform;
        }
    }
}
