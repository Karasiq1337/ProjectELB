using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddelAudio : MonoBehaviour
{
    [SerializeField] private bool _toddleMusic, _toddleEffects;
    // Start is called before the first frame update

    public void Toddle()
    {
        if (_toddleEffects) SoundManager.Instance.ToddleEffects();
        if(_toddleMusic) SoundManager.Instance.ToddleMusic();
    }
}
