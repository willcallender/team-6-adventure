using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarManager : MonoBehaviour
{
    Slider slider;
    public float hideAfterFullTime;
    Coroutine hideTimerCoroutine;
    CanvasRenderer[] canvasRenderer;

    private void OnEnable() {
        slider = GetComponent<Slider>();
        canvasRenderer = GetComponentsInChildren<CanvasRenderer>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaxHealth(float hp) {
        slider.maxValue = hp;
    }

    public void setHealth(float hp) {
        slider.value = hp;
        try {
            StopCoroutine(hideTimerCoroutine);
        } catch {}
        setAlpha(1);
        if (hp == slider.maxValue) {
            hideTimerCoroutine = StartCoroutine(hideTimer());
        }
    }

    void setAlpha(float a) {
        
        for (int i = 0; i < canvasRenderer.Length; i++) {
            canvasRenderer[i].SetAlpha(a);
        }
    }

    IEnumerator hideTimer() {
        yield return new WaitForSeconds(hideAfterFullTime);
        float a = 1;
        while (a > 0) {
            a -= Time.deltaTime;
            if (a < 0) {
                setAlpha(0);
            } else {
                setAlpha(a);
            }
            yield return new WaitForEndOfFrame();
        }
        
    }
}
