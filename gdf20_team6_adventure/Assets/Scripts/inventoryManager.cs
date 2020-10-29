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
    public int numItems;
    public int numHerbs;
    public Sprite[] herbs;

    // Start is called before the first frame update
    void Start() {
        itemNames = (textFile.text.Split('\n'));
        numItems = itemNames.Length;
        inventory = new int[numItems];
        discovered = new bool[numItems];
        herbs = new Sprite[numHerbs];
        for (int i = 0; i < numHerbs; i++) {
            string path = "Sprites/Herbs/" + i.ToString();
            herbs[i] = Resources.Load<Sprite>(path);
        }
    }

    public void addItem(int ID) {
        if (!discovered[ID]) {
            print("New item " + itemNames[ID] + " discovered");
            discovered[ID] = true;
        }
        inventory[ID]++;
    }
}