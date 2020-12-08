using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionParticleSystemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator deathTimer() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
