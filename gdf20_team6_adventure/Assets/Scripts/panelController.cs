using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setAlpha(float a) {
        GetComponent<Image>().canvasRenderer.SetAlpha(a);
    }

    public void animateAlpha(float a, float duration) {
        GetComponent<Image>().CrossFadeAlpha(a, duration, false);
    }
}
