using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneStarMonologue : MonoBehaviour
{
    DialogueManager manager;
    [SerializeField] Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("dialogueManager").GetComponent<DialogueManager>();
        if(manager != null)
        {
            Debug.Log(manager.name + " найден");
            manager.StartMolologue(dialogue);
        }

      
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
