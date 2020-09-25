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
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(x * speed, y * speed, 0f);
    }
}
