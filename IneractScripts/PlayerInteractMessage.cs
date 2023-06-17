using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractMessage : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private bool isShown;
    [SerializeField] InteractMessage interactMessage;
    Vector2 movementInput;
    [SerializeField] float raycasrDistance;
    [SerializeField] int layerMask;
    Collider2D raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        isShown = false;

    }
    private void FixedUpdate()
    {
        gameObject.transform.position = GameObject.FindWithTag("Player").transform.position;
        raycastHit = Physics2D.OverlapCircle(gameObject.transform.position, raycasrDistance);
        if(raycastHit != null)
        {
            Debug.Log(raycastHit.gameObject.name);
            if (raycastHit.gameObject.tag == "Interactable")
            {
                interactMessage.DissplayMessage(raycastHit.GetComponent<InteactMessageString>().message);
            }
            gameObject.transform.position = GameObject.FindWithTag("Player").transform.position;
        }
        else
        {
            interactMessage.UndissplayMessage();
        }
         
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Interactable" && !isShown)
        {
            isShown = true;
            
            interactMessage.DissplayMessage(collision.gameObject.GetComponent<InteactMessageString>().message);
        }
        isShown = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("גûשוכ");
        interactMessage.UndissplayMessage();
        isShown = false;
    }
}
