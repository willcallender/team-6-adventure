using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dud_potion : MonoBehaviour
{
    public void onLand() {
        print("landed");
        Destroy(gameObject);
    }
}
