using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBoxManager : MonoBehaviour {
    // text box and text field must be manually set in inspector for every instance of this script. Thanks Unity
    public GameObject textBox;
    public Text textField;
    public inventoryManager inventory;
    public int waitSeconds;
    // Start is called before the first frame update
    void Start() {
        textBox.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryManager>();
    }

    // Update is called once per frame
    // void Update() {
    // }
    public void discoverIngredient(int ID) {
        string message = "You have discovered " + inventory.itemNames[ID];
        textBox.SetActive(true);
        textField.text = message;
        StartCoroutine(dispText());
        print(message);
    }

    IEnumerator dispText() {
        print("Entered coroutine");
        yield return new WaitForSeconds(waitSeconds);
        print("End wait");
        textBox.SetActive(false);
        print("Coroutine ended");
    }
}
