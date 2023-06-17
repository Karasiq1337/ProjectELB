using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show()
    {
        settings.SetActive(true);
    }
    public void Close()
    {
        Loader.LoadScene(Loader.Scene.MainMenu);

    }
}
