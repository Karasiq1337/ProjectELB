using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    [TextArea()]
    public string[] sentances;
    public string name;
    public Sprite image;

}
