using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    private Queue<string> setances;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI npcName;
    public GameObject npcImagestart;
    public DialogueBox dialogueBox;
    public float typingSpeed;
    [SerializeField] Sprite monologueImage;

    [SerializeField] AudioSource leeterSound1;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void StartMolologue(Dialogue dialogue)
    {
        dialogue.image = monologueImage;
        dialogue.name = "Это тоже";
        StartDialogue(dialogue);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        setances = new Queue<string>();
        Time.timeScale = 0;
        dialogueBox.Appear();


        npcImagestart.GetComponent<PicBox>().PicBoxtInvoke(dialogue.image);
        npcName.text = dialogue.name;
        if (setances != null)
        {
            setances.Clear();
        }
       

        foreach (string sentence in dialogue.sentances)
        {
            setances.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(setances.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = setances.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            leeterSound1.Play();
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        Time.timeScale = 1;
        Debug.Log("ENded talking");
        dialogueBox.Diaapper();
    }

}

