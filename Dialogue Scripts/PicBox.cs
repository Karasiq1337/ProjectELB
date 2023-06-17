using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicBox : MonoBehaviour
{
    [SerializeField] Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }
    public void PicBoxtInvoke(Sprite sprite)
    {
        image.enabled = true;
        image.sprite = sprite;
    }
}
