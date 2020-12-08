using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] stuff = GameObject.FindGameObjectsWithTag("Enemy");
        print(stuff.Length);
        if (stuff.Length == 0) {
            SceneManager.LoadScene("Level 2");
        }
    }
}
