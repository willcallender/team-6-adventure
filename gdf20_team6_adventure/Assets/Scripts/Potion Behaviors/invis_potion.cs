using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invis_potion : MonoBehaviour
{
    potionController pc;
    playerManager playerManagerScript;
    GameObject player;

    private void Start() {
        pc = GetComponent<potionController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerManagerScript = player.GetComponent<playerManager>();
        playerManagerScript.invis(pc.effectDuration);
        GameObject psObject = Instantiate(GetComponent<potionController>().particleSystemPrefab);
        psObject.transform.position = transform.position;
        ParticleSystem.MainModule settings = psObject.GetComponent<ParticleSystem>().main;
        settings.startColor = new Color(0, 100, 153);
        Destroy(gameObject);
    }

    // this code shouldn't ever run
    public void onLand() {
        Destroy(gameObject);
    }
}
