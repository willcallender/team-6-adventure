using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    public float speed;
    float x, y;
    // Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        // rb = GetComponent<Rigidbody2D> ();
        // Camera cam = (Camera)FindObjectOfType(typeof(Camera));
        // float width, height;
        // height = 2 * cam.orthographicSize;
        // width = height * cam.aspect;
        // speed = height * 0.01f;
    }
    
    // Update is called once per frame
    void Update() {
        // get inputs
        x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // move player according to input
        transform.Translate(x, y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ingredient") {
            // get GameObject for the ingredient collided with
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            // get ingredient ID
            int ingredient = collision.gameObject.GetComponent<ingredientBehavior>().ingID;
            // add ingredient to inventory
            inventory.GetComponent<inventoryManager>().addIngredient(ingredient);
            // destroy ingredient GameObject
            Destroy(collision.gameObject);
        }
    }
}
