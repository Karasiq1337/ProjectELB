using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithObject : MonoBehaviour
{
    public bool isInrange { get; private set; }
    public KeyCode interactKey;
    public UnityEvent interactAction;
    [Header("Опционально")]
    [SerializeField] InteractMessage playerInteractMessageReference;
    [SerializeField] string itrractActionString;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isInrange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInrange = true;
            if (itrractActionString == null)
            {

                playerInteractMessageReference.DissplayMessage("[E]");

            }
            else if (itrractActionString == "")
            {
                playerInteractMessageReference.DissplayMessage("[E]");
            }
            else
            {
                playerInteractMessageReference.DissplayMessage(itrractActionString);
            }
            

            Debug.Log("in range with "+ gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("noy in range with " + collision.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInteractMessageReference.UndissplayMessage();
            isInrange = false;
        }
    }
}
