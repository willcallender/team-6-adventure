using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour {
    // declare array of ingredient names, amount in inventory, and
    // whether or not they've been discovered yet
    public TextAsset textFile;
    public string[] itemNames;
    public int[] inventory;
    public bool[] discovered;

    // Start is called before the first frame update
    void Start() {
        itemNames = (textFile.text.Split('\n'));
        inventory = new int[itemNames.Length];
        discovered = new bool[itemNames.Length];
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    public void addItem(int ID) {
        if (!discovered[ID]) {
            print("New item " + itemNames[ID] + " discovered");
            discovered[ID] = true;
        }
        inventory[ID]++;
    }
}