using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractMessage : MonoBehaviour
{

    [SerializeField] GameObject textFieald;
    [SerializeField] GameObject player;

    private void Start()
    {
        gameObject.SetActive(false);
        textFieald.SetActive(false );
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            gameObject.transform.position = player.transform.position + new Vector3(10, 10 ,0); 
        }
    }
    public void DissplayMessage(string message)
    {
        gameObject.SetActive(true);
        textFieald.SetActive(true);
        if (message != null)
        {
            textFieald.GetComponent<TextMeshProUGUI>().text = message;
        }
        else
        {
            textFieald.GetComponent<TextMeshProUGUI>().text = "[E]";
        }
    }
    public void UndissplayMessage()
    {
        gameObject.SetActive(false);
        textFieald.GetComponent<TextMeshProUGUI>().text = "";
        textFieald.SetActive(false);
    }
}
