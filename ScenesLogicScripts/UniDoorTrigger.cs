using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniDoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartFloorOne()
    {
        Loader.LoadScene(Loader.Scene.FloorOne);
    }
}
