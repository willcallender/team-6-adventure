using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftingManager : MonoBehaviour
{
    // internal manager for what ingredients have been added to the cauldron so far
    List<int> cauldron = new List<int>();    // herb image prefab
    public GameObject herbImagePrefab;
    // number of herb assets
    public int numHerbs;
    // list of all herb assets
    Sprite[] herbs;
    inventoryManager inventory;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryManager>();
        numHerbs = inventory.numHerbs;
        herbs = new Sprite[numHerbs];
        for (int i = 0; i < numHerbs; i++) {
            string path = "Sprites/Herbs/" + i.ToString();
            herbs[i] = Resources.Load<Sprite>(path);
        }
    }
}
