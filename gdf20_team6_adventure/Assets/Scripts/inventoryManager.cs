using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour {
    // declare array of ingredient names, amount in inventory, and
    // whether or not they've been discovered yet
    public string[] itemNames = {"Ingredient 1", "Ingredient 2"};
    public int[] inventory;
    public bool[] discovered;

    // Start is called before the first frame update
    void Start() {
        inventory = new int[itemNames.Length];
        discovered = new bool[itemNames.Length];
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    public void addIngredient(int ID) {
        if (!discovered[ID]) {
            print("New item " + itemNames[ID] + " discovered");
            discovered[ID] = true;
        }
        inventory[ID]++;
    }
}