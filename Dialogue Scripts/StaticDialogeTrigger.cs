using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDialogeTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    DialogueManager dialogueManager;
    private void Start()
    {
        
    }
    public void TriggerDialogue()
    {
        dialogueManager = GameObject.FindWithTag("dialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
    }
}
