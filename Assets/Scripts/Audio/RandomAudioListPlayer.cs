using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class RandomAudioListPlayer : MonoBehaviour
    {
        public Sound[] sounds;

        public AudioMixerGroup effectMixerGroup;
        [Header("How 3D the sound should be. 0=2D, 1=3D")]
        [SerializeField] private float spatialBlend = 0;
        void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = spatialBlend;
                s.source.outputAudioMixerGroup = effectMixerGroup;
               // s.source.spread = 360;
            }
        }
    
        public void Play ()

        {
            int i= UnityEngine.Random.Range(0, sounds.Length);;
            /*
            do
            {
                 i = UnityEngine.Random.Range(0, sounds.Length);
                 
                //sounds[i].source.PlayOneShot(sounds[i].source.clip);
              //  if (!sounds[i].source.isPlaying) 
                
            } while (sounds[i].source.isPlaying);
            */
            sounds[i].source.Play();

        }
    }
}

