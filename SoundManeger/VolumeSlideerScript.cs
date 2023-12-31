using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlideerScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
