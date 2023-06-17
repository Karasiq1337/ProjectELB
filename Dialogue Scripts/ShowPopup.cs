using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    [SerializeField] string popupMessage;
    [SerializeField] MessagePopup messagePopup;
    private void Start()
    {
        messagePopup = GameObject.FindWithTag("messagePopup").GetComponent<MessagePopup>();
    }
    public void TriggerPopUp()
    {
        messagePopup.ShowMessage(popupMessage);
    }
}
