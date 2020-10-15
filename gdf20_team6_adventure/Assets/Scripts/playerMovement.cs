using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {
    public float speed;
    float x, y;
    public GameObject textBox;
    Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update() {
        // get inputs
        x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        transform.Translate(x, y, 0);

        // update anim variables
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);

        if (x == 0 && y == 0) {
            anim.SetBool("isIdle", true);
        } else {
            anim.SetBool("isIdle", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ingredient") {
            // get GameObject for the ingredient collided with
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            // get ingredient ID
            int ingredient = collision.gameObject.GetComponent<ingredient>().ID;
            // add ingredient to inventory
            inventory.GetComponent<inventoryManager>().addIngredient(ingredient);
            // destroy ingredient GameObject
            Destroy(collision.gameObject);
            // display message about discovery
            textBox.GetComponent<textBoxManager>().discoverIngredient(ingredient);
        }
    }
}
