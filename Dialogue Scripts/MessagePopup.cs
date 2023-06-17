using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MessagePopup : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    [SerializeField] GameObject popupWindow;
    // Start is called before the first frame update
    void Start()
    {
        popupWindow.SetActive(false);

    }

    public void ShowMessage(string message)
    {
        Time.timeScale = 0;
        popupWindow.SetActive(true);
        textMeshProUGUI.text = message;

    }
 public void CloseMessage()
    {
        Time.timeScale = 1;
        Debug.Log("закрыл");
        popupWindow.SetActive(false);
    }

}
