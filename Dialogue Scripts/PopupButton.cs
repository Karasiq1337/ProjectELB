using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    [SerializeField] GameObject popupWindow;
    // Start is called before the first frame update
public void ClosePopup()
    {
        popupWindow.SetActive(false);
    }
}
