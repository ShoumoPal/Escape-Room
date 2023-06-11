using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum for sounds
public enum SoundTypes
{
    BackgroundMusic,
    ButtonHover,
    ButtonPress,
    JumpSound,
    LevelCompleteSound,
    HurtSound
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundsFX;
    [SerializeField]
    private AudioSource soundsBG;
    [SerializeField]
    private AudioSource soundsFootsteps;

    public Sounds[] sounds;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    //Singleton SoundManager
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySFX(SoundTypes soundType)
    {
        AudioClip clip = Array.Find(sounds, i => i.soundType == soundType).soundClip;
        float Volume = Array.Find(sounds, i => i.soundType == soundType).volume;
        soundsFX.clip = clip;
        soundsFX.volume = Volume;
        soundsFX.Play();
    }
    public void PlayBG(SoundTypes soundType)
    {
        AudioClip clip = Array.Find(sounds, i => i.soundType == soundType).soundClip;
        float Volume = Array.Find(sounds, i => i.soundType == soundType).volume;
        soundsBG.clip = clip;
        soundsBG.volume = Volume;
        if (!soundsFX.isPlaying)
        {
            soundsBG.Play();
        }
    }
    public void PlayFootsteps()
    {
        if(!soundsFootsteps.isPlaying)
        {
            soundsFootsteps.Play();
        }
    }
    public void StopFootsteps()
    {
        soundsFootsteps.Stop();
    }
}
//class for Sounds
[Serializable]
public class Sounds
{
    public SoundTypes soundType;
    public AudioClip soundClip;
    [Range(0f, 1f)]
    public float volume;
}

