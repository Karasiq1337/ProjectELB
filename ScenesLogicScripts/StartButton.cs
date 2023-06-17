using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void LoadOnButtonClick()
    {
        Loader.LoadScene(Loader.Scene.SquereBefore);
    }
}
