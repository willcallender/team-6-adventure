using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {
    public float speed;
    float x, y;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D> ();
    }
    
    // Update is called once per frame
    void Update() {
        // get inputs
        x = Input.GetAxisRaw("Horizontal") * speed;
        y = Input.GetAxisRaw("Vertical") * speed;

        // move player according to input
        transform.Translate(x, y, 0);
    }
}
