using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public Sound[] shotSounds;
        public Sound[] ExplosionSounds;
        
        private Sound[] liste;

        public AudioMixerGroup effectMixerGroup;

        public static AudioManager instance;
        
        // Start is called before the first frame update
        void Awake()
        {
            if (instance != null && instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                instance = this; 
            } 
            
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.outputAudioMixerGroup = effectMixerGroup;
            }
            foreach (Sound s in shotSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.outputAudioMixerGroup = effectMixerGroup;
            }
        
            foreach (Sound s in ExplosionSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.outputAudioMixerGroup = effectMixerGroup;
            }
        }
    
        public void Play (string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        } 

        public void PlayRandomSoundFromList(string listenName)
        {
            if (listenName.Equals("shotSounds")) liste = shotSounds;
            else if (listenName.Equals("ExplosionSounds")) liste = ExplosionSounds;
            else return;
        
            PlaySoundFromList(liste);
        }

        public void PlayRifleShotSound()
        {
            liste = shotSounds;
            PlaySoundFromList(liste);
        }

        private void PlaySoundFromList(Sound[] pListe)
        {
            int i = UnityEngine.Random.Range(0, pListe.Length);
            //pListe[i].source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
            pListe[i].source.Play();
        }
    }
}

