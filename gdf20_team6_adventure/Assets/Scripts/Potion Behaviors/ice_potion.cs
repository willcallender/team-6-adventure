using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ice_potion : MonoBehaviour
{
    potionController pc;

    private void Start() {
        pc = GetComponent<potionController>();
    }

    public void onLand() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            if (Vector2.Distance(enemies[i].transform.position, transform.position) < pc.effectRange) {
                enemies[i].GetComponent<enemyManager>().ice(pc.effectDuration);
            }
        }
    }
}
