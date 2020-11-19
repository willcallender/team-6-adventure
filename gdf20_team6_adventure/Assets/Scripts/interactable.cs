using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    playerManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>();
    }

    private void OnMouseEnter() {
        player.mouseOverInteractable = true;
    }

    private void OnMouseExit() {
        player.mouseOverInteractable = false;
    }
}
