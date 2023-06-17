using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorThreeMV : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;
    GameObject dialogueManager ;
    public Dialogue dialogue;
    PlayerState platerState;
    bool beenTriggererd;
    bool dissapeared;
    bool flamesAppeared;
    bool canBeTriggered;
    bool popupShown;
    List<GameObject> flames;
    [SerializeField] SceneSettings sceneSettings;
    [SerializeField] MessagePopup messagePopup;
    [SerializeField] GameObject escapeLadder;
    [SerializeField] GameObject cantMoveTrow;

    [Header("Flames")]
    [SerializeField] GameObject flame1;
    [SerializeField] GameObject flame2;
    [SerializeField] GameObject flame3;
    [SerializeField] GameObject flame4;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        dialogueManager = GameObject.FindWithTag("dialogueManager");
        platerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        animator.Play("Idle");
        boxCollider = GetComponent<BoxCollider2D>();
        dissapeared = false;
        flamesAppeared = false;
        canBeTriggered = true;
        popupShown = false;
        flames = new List<GameObject>();
        flames.Add(flame1);
        flames.Add(flame2);
        flames.Add(flame3);
        flames.Add(flame4);
    }
    private void FixedUpdate()
    {
        if (dissapeared && !flamesAppeared)
        {
            StartCoroutine(TriggerFlames());
        }
        if (flamesAppeared && !popupShown)
        {
            popupShown = true;
            messagePopup.ShowMessage("Теперь вы можете совершить рывок нажав [SPACE]!");
            //messagePopup.ShowMessage();
            //messagePopup.ShowMessage("Бегите!");
        }
        if (popupShown)
        {
            sceneSettings.canDashScene = true;
           // cantMoveTrow.SetActive(false);
            escapeLadder.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canBeTriggered)
            {
                canBeTriggered = false;
                animator.Play("Triggered");
                platerState.canMove = false;
                StartCoroutine(WaitForAnimation());
               
                platerState.canMove = true;
            }
           
           

        }
    }
    IEnumerator TriggerFlames()
    {
        flamesAppeared = true;
        foreach (GameObject flame in flames)
        {
            flame.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
        animator.Play("Stare");
        StartCoroutine(PlayDissappearAnimation());


    }
    IEnumerator PlayDissappearAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color targetColor = sprite.color;
        targetColor.a = 1;
        while (sprite.color.a >= 0)
        {
            targetColor.a -= 0.05f;
            sprite.color = targetColor;
            yield return new WaitForSeconds(0.1f);
        }
        dissapeared = true;
    }
}
