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
    // the current cardinal direction, 0 = up, 1 = right, 2 = down, 3 = left
    int facing;
    // the text box object to write to
    public GameObject canvas;
    public textBoxManager text;
    // the player animation component to control
    Animator anim;
    // get GameObject for the inventory
    inventoryManager inventory;
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryManager>();
        text = canvas.GetComponent<textBoxManager>();
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
                idle(facing);
            } else if (x > 0) {
                right();
            } else {
                left();
            }
        }
        // if only y changed and x is 0
        else if (py != y && px == x && x == 0) {
            if (y == 0) {
                idle(facing);
            } else if (y > 0) {
                up();
            } else {
                down();
            }
        }
        // if x changed to 0 and y not 0
        else if (x == 0 && py == y && y != 0) {
            if (y > 0) {
                up();
            } else {
                down();
            }
        }
        // if y changed to 0 and x not 0
        else if (y == 0 && px == x && x != 0) {
            if (x > 0) {
                right();
            } else if (x < 0) {
                left();
            }
        }
        // if both changed
        else if (px != x && py != y) {
            if (x == 0 && y == 0) {
                idle(facing);
            } else if (x == 0) {
                if (y > 0) {
                    up();
                } else {
                    down();
                }
            } else {
                if (x > 0) {
                    right();
                } else {
                    left();
                }
            }
        }
    }

    void idle(int d) {
        anim.enabled = false;
        Sprite idle;
        if (facing == 0) {
            idle = upSprite;
        } else if (facing == 1) {
            idle = rightSprite;
        } else if (facing == 2) {
            idle = downSprite;
        } else {
            idle = leftSprite;
        }
        GetComponent<SpriteRenderer>().sprite = idle;
    }

    void up() {
        anim.enabled = true;
        anim.Play("elf_run_up");
        facing = 0;
    }

    void right() {
        anim.enabled = true;
        anim.Play("elf_run_right");
        facing = 1;
    }

    void down() {
        anim.enabled = true;
        anim.Play("elf_run_down");
        facing = 2;
    }

    void left() {
        anim.enabled = true;
        anim.Play("elf_run_left");
        facing = 3;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ingredient") {
            // get ingredient ID
            int ingredient = collision.gameObject.GetComponent<ingredient>().ID;
            // add ingredient to inventory
            inventory.addIngredient(ingredient);
            // destroy ingredient GameObject
            Destroy(collision.gameObject);
            // display message about discovery
            text.discoverIngredient(ingredient);
        }
    }
}