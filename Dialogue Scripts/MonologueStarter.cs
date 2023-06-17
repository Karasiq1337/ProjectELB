using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueStarter : MonoBehaviour
{
    [SerializeField] DialogueManager manager;
    // Start is called before the first frame update
    void Start()
    {
        new WaitForFixedUpdate();
        Debug.Log(manager.name+ " найден");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMonologue(Dialogue dialogue)
    {
        if (manager != null)
        {
            manager.StartMolologue(dialogue);
        }
    }
}
