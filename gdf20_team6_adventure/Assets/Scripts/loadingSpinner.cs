using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingSpinner : MonoBehaviour
{
    public GameObject spinner;

    public GameObject sceneControl;

    void Start() {
        print("start");
        StartCoroutine(sceneControl.GetComponent<sceneController>().changeSceneAsync("Level 1"));
    }

    // Update is called once per frame
    void Update()
    {
        spinner.transform.Rotate(0,0,-1);
    }
}
