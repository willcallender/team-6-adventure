using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingredient : MonoBehaviour {
    // declare int to store what type of ingredient this is
    public int ID;
    inventoryManager inventory;
    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryManager>();
        if (!inventory.ready()) {
            StartCoroutine(waitTillReady());
            return;
        }
        GetComponent<SpriteRenderer>().sprite = inventory.herbs[ID];
    }

    IEnumerator waitTillReady() {
        yield return new WaitUntil(inventory.ready);
        Start();
    }
}