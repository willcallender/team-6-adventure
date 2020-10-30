using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPSLate : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(delay());
    }

    IEnumerator delay() {
        yield return new WaitForSeconds(5);
        ps.Play();
    }
}
