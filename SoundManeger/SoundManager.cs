using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectSourse;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        _effectSourse.PlayOneShot(clip);
    }
     public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ToddleEffects()
    {
        _effectSourse.mute = !_effectSourse.mute;
    }
    public void ToddleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
