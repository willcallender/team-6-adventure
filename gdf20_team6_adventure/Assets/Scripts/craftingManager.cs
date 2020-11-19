using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftingManager : MonoBehaviour
{
    // prefab to contain the icons
    public GameObject herbImagePrefab;
    // number of herb assets
    public int numHerbs;
    // list of all herb assets
    Sprite[] herbs;
    // reference to inventory manager
    inventoryManager inventory;
    // an offset determining how far the player has scrolled in inventory
    public int invSlotOffset;
    // reference to the inventory slot elements in the UI
    public GameObject[] invSlots = new GameObject[4];
    // reference to the images inside the slots, may be null at any time
    GameObject[] invSlotImgs = new GameObject[4];
    // if the start function has finished, used to prevent the onEnable from doing anything the first time before resources have been loaded
    bool ready = false;
    // reference to the brew slot elements
    public GameObject[] brewSlots = new GameObject[4];
    // reference to images inside brew slots, may be null at any time
    GameObject[] brewSlotImgs = new GameObject[4];
    // how far the player has scrolled in brew
    public int brewSlotOffset;
    // number of herbs in the recipe
    int numInBrew = 0;
    // the current recipe
    List<int> recipe = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        // get inventory manager reference
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryManager>();
        if (!inventory.ready()) {
            StartCoroutine(waitTillReady());
            return;
        }
        numHerbs = inventory.numDiscovered;
        // get array of herb sprites from inventory manager
        herbs = inventory.herbs;
        // set ready flag to true
        ready = true;
        gameObject.SetActive(false);
    }

    IEnumerator waitTillReady() {
        yield return new WaitUntil(inventory.ready);
        Start();
    }

    private void OnEnable() {
        // only draw after start function has fully executed once
        if (ready) {
            draw();
        }
        numInBrew = recipe.Count;
    }

    private void OnDisable() {
        // clear slots on disable 
        erase();
    }

    void draw() {
        // inventory slots
        numHerbs = inventory.numDiscovered;
        if (numHerbs == 0) {
            return;
        }
        for (int i = 0; i < min(numHerbs, 4); i++) {
            // instantiate prefab image with slot as parent
            invSlotImgs[i] = Instantiate(herbImagePrefab, invSlots[i].transform);
            // next two lines get the item id
            int id = i + invSlotOffset;
            id = getID(id);
            // assigns sprite to correct herb sprite based on item id
            invSlotImgs[i].GetComponent<Image>().sprite = herbs[id];
        }

        // brew slots
        for (int i = 0; i < min(numInBrew, 4); i++) { // loop through only as many slots as are necessary
            // get the index in the recipe that should be displayed in slot i
            int pos = i + brewSlotOffset;
            // instantiate a prefab with brew slot as parent
            brewSlotImgs[i] = Instantiate(herbImagePrefab, brewSlots[i].transform);
            // set the image to the correct herb sprite based on item id
            brewSlotImgs[i].GetComponent<Image>().sprite = herbs[recipe[pos]];
        }
    }

    // gets the minimum between a and b, if equal return a
    int min(int a, int b) {
        if (a-b > 0) {
            return b;
        } else {
            return a;
        }
    }

    // get the id, properly placed between 0 and numHerbs
    int getID(int id) {
        // ensure id is positive
        while (id < 0) {
            id += numHerbs;
        }
        // ensure id is less than numHerbs
        if (id > numHerbs) {
            id = id % numHerbs;
        }
        id = inventory.discoveredHerbs[id];
        // return the calculated item id
        return id;
    }

    int getID() {
        return getID(invSlotOffset);
    }

    // responsble for erasing all on screen icons
    void erase() {
        // inventory slots
        for (int i = 0; i < 4; i++) {
            Destroy(invSlotImgs[i]);
        }

        // brew slots, looping only as many times as needed
        for (int i = 0; i < min(numInBrew, 4); i++) {
            Destroy(brewSlotImgs[i]);
        }
    }

    void redraw() {
        erase();
        draw();
    }

    // increment/decrement invSlotOffset according to direction flag and redraw
    public void invScroll(bool up) {
        if (numHerbs < 5) {
            return;
        }
        if (up) {
            invSlotOffset--;
        } else {
            invSlotOffset++;
        }
        redraw();
    }

    // add herb to recipe
    public void addHerb(int slotID) {
        // calculate correct item id
        int id = getID() + slotID;
        // add id to recipe list
        recipe.Add(id);
        numInBrew++;
        brewScroll(true);
    }

    // remove from slot
    public void removeHerb(int slotID) {
        // do nothing if slot is empty
        if (numInBrew - 1 < slotID) {
            return;
        }
        // erase first for safety reasons
        erase();
        // find the position in the recipe
        int pos = slotID + brewSlotOffset;
        // remove the herb at that position
        recipe.RemoveAt(pos);
        // reset numInBrew to length of recipe
        numInBrew = recipe.Count;
        // these while statements are an inefficient (lazy) way to ensure the brewSlotOffset is within the correct range
        while (brewSlotOffset + 4 > numInBrew) {
            brewSlotOffset--;
        }
        while (brewSlotOffset < 0) {
            brewSlotOffset++;
        }

        redraw();
    }

    // increment/decrement brewSlotOffset as long as that wouldn't take the user past the bounds
    public void brewScroll(bool right) {
        if (right) {
            if (brewSlotOffset + 5 <= numInBrew) {
                brewSlotOffset++;
            }
        } else {
            if (brewSlotOffset - 1 >= 0) {
                brewSlotOffset--;
            }
        }
        redraw();
    }
}
