using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTwoSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoaadFloarThree()
    {
        Loader.LoadScene(Loader.Scene.FloorThree);
    }
}
