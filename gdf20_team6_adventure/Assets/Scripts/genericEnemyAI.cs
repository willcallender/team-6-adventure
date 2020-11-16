using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class genericEnemyAI : MonoBehaviour {
    GameObject player;
    GameObject target;
    bool confused = false;
    // Start is called before the first frame update
    NavMeshAgent agent;
    Coroutine navCoroutine, countdownCoroutine;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        navCoroutine = StartCoroutine(navTargetRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator confuseCountdown(float timer) {
        yield return new WaitForSeconds(timer);
        StopCoroutine(navCoroutine);
        confused = false;
        target = player;
        navCoroutine = StartCoroutine(navTargetRoutine());
    }

    // called by other scripts to confuse the AI and target it to a non player target
    public void confuse(GameObject newTarget, float confuseForSeconds) {
        target =  newTarget;
        confused = true;
        StopCoroutine(navCoroutine);

        // Little does Unity know, I'm gonna play the ultimate prank on the compiler. The classic completely ignoring exceptions trick🎃
        try {
            StopCoroutine(countdownCoroutine);
        }
        catch {}

        countdownCoroutine = StartCoroutine(confuseCountdown(confuseForSeconds));
        navCoroutine = StartCoroutine(navTargetRoutine());
    }

    IEnumerator navTargetRoutine() {
        while(true) {
            agent.SetDestination(target.transform.position);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
