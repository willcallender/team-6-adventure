using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {
    // speed multiplier
    public float speed;
    // the x and y component of user input
    float x, y;
    // the x and y component of translation
    float tx, ty;
    // the values of x and y from the previous frame
    float px, py = 0;
    // the text box object to write to
    public GameObject textBox;
    // the player animation component to control
    Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update() {
        // get inputs
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        // this needs to happen because the delta time may change with each frame, making the animation conditions unhelpful
        tx = x * speed * Time.deltaTime;
        ty = y * speed * Time.deltaTime;

        transform.Translate(tx, ty, 0);

        updateAnim();
        px = x;
        py = y;
    }

    void updateAnim() { 
        // only update if other axis not already doing something (if running up, don't change to sideways on press left and hold up)
        // if only x changed and y is 0
        if (px != x && py == y && y == 0) {
            if (x == 0) {
                anim.Play("idle");
            } else if (x > 0) {
                anim.Play("elf_run_right");
            } else {
                anim.Play("elf_run_left");
            }
        }
        // if only y changed and x is 0
        else if (py != y && px == x && x == 0) {
            if (y == 0) {
                anim.Play("idle");
            } else if (y > 0) {
                anim.Play("elf_run_up");
            } else {
                anim.Play("elf_run_down");
            }
        }
        // if x changed to 0 and y not 0
        else if (x == 0 && py == y && y != 0) {
            if (y > 0) {
                anim.Play("elf_run_up");
            } else {
                anim.Play("elf_run_down");
            }
        }
        // if y changed to 0 and x not 0
        else if (y == 0 && px == x && x != 0) {
            if (x > 0) {
                anim.Play("elf_run_right");
            } else if (x < 0) {
                anim.Play("elf_run_left");
            }
        }
        // if both changed
        else if (px != x && py != y) {
            if (x == 0 && y == 0) {
                anim.Play("idle");
            } else if (x == 0) {
                if (y > 0) {
                    anim.Play("elf_run_up");
                } else {
                    anim.Play("elf_run_down");
                }
            } else {
                if (x > 0) {
                    anim.Play("elf_run_right");
                } else {
                    anim.Play("elf_run_left");
                }
            }
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