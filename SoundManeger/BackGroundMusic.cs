using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip m_AudioClip;
    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlaySound(AudioClip m_AudioClip)
    {

    }
}
