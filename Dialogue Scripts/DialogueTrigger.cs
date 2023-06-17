using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    [SerializeField] DialogueManager dialogueManager;
    private void Start()
    {
    }
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
