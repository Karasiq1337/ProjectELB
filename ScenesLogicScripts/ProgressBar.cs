using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        image = transform.GetComponent<Image>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Loader.GetLoadingProgerss();
    }
}
