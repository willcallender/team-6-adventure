using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_potion : MonoBehaviour
{
    potionController pc;

    private void Start() {
        pc = GetComponent<potionController>();
    }

    public void onLand() {
        ParticleSystem.MainModule settings = pc.ps.GetComponent<ParticleSystem>().main;
        settings.startColor = new Color(255, 75, 0);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            if (Vector2.Distance(enemies[i].transform.position, transform.position) < pc.effectRange) {
                enemies[i].GetComponent<enemyManager>().damage(20);
            }
        }
        Destroy(gameObject);
    }
}
