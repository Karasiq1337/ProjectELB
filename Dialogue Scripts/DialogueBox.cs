using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void Appear()
    {
        gameObject.SetActive(true);

    }
    public void Diaapper()
    {
        gameObject.SetActive(false);
    }

}
