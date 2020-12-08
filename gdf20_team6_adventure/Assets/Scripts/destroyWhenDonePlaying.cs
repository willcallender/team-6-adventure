using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenDonePlaying : MonoBehaviour
{
    AudioSource audioSourceComponent;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = GetComponent<AudioSource>();
        StartCoroutine(waitForAudioFinish());
    }

    IEnumerator waitForAudioFinish() {
        yield return new WaitUntil(isDone);
        Destroy(gameObject);
    }

    bool isDone() {
        return !audioSourceComponent.isPlaying;
    }
}
