using UnityEngine;
using System;

using UnityEngine.Audio;

namespace BaseFramework
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;
        public bool loop;

        [HideInInspector] public AudioSource audioSource;
    }

    public class AudioManager : MonoBehaviour
    {
		[TextArea]
		public string scriptInfo;

		[Space]
		public bool useDestroy = false;

        [Tooltip("Default Volume, Pitch & Loop Configurations from the AudioSource instead of Individual Sound Elements.")]
        public bool useDefaults = false;
        public bool playRandom = false;
        public string nameToPlay;

        [Space]
        public Sound[] sounds;

        void OnEnable()
        {
            UnityEngine.Random.InitState(DateTime.Now.Second);
            
            if (playRandom)
            {
                //Select a Random SoundIndex and Play.
                int i = UnityEngine.Random.Range(0, sounds.Length);

				scriptInfo += "Current Audio:" + sounds [i].clip.name;

				SetUpAudio (sounds [i]);
				PlayAudio(sounds[i].name);

            }

            else PlayAudio(nameToPlay);

			if (useDestroy) 
			{
				Destroy (this);
			}
        }

		void SetUpAudio(Sound s)
		{

			if (gameObject.GetComponent<AudioSource> () == null)
			{
				gameObject.AddComponent<AudioSource> ();
			}

			s.audioSource = gameObject.GetComponent<AudioSource>();

			if (!useDefaults)
			{
				s.audioSource.volume = s.volume;
				s.audioSource.pitch = s.pitch;
				s.audioSource.loop = s.loop;
			}
		}

        public void PlayAudio(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s != null)
            {
                s.audioSource.clip = s.clip;
                s.audioSource.Play();

                /*
                #if UNITY_EDITOR
                Debug.Log(scriptInfo);
                #endif
                */
            }
        }
    }
}


