using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVFloorOne : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject dialogueManager;
    public Dialogue dialogue;
    [SerializeField] PlayerState platerState;
    bool dissapeared;
    bool canBeTriggered;
    bool popupShown;


    [SerializeField] MessagePopup messagePopup;
    // Start is called before the first frame up
    // date
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        dissapeared = false;
        canBeTriggered = true;
        popupShown = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {


        if (dissapeared && !popupShown)
        {
            popupShown = true;
            messagePopup.ShowMessage("Вам пирдётся Найти выход отсюда!");
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
