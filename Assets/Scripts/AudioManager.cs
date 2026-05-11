using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;
    public Sound[] sounds;
    [SerializeField] AudioMixer mixer;

    public AudioSource audioSource; //reference to the audio source component on the game object

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    bool frontend = true;
    bool dummy = false;
    void Awake()
    {
        // if instance is null, store a reference to this instance
        if (instance == null)
        {
            // a reference does not exist, so store it
            instance = this;
            DontDestroyOnLoad(gameObject);              
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            Destroy(gameObject);
            return;
        }
        


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            float pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        LoadVolume();


    }

    void Update()
    {
       // SwitchMusic();
    }


    public void LoadVolume() // volume saved in volumesettings
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    /*public void SwitchMusic() // switches music between scenes lmao (im lying)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if(dummy)
            {
                PlayClip("MusicGame");
                StopClip("MenuMusic");
                dummy = false;
                frontend = true;
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if(frontend)
            {
                StopClip("MusicGame");
                PlayClip("MenuMusic");
                frontend = false;
                dummy = true;
            }
        }
    }
    */

    public void PlayClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
