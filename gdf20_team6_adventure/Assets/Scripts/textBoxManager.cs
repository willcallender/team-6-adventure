using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBoxManager : MonoBehaviour {
    // text box and text field must be manually set in inspector for every instance of this script. Thanks Unity
    public GameObject textBoxPrefab;
    public GameObject canvas;
    public inventoryManager inventory;
    public int waitSeconds;
    public int pendingMessages;
    public int msgOffset;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    // void Update() {
    // }
    public void discoverIngredient(int ID) {
        string message = "You have discovered " + inventory.itemNames[ID];
        StartCoroutine(dispText(message));
        print(message);
    }

    IEnumerator dispText(string message) {
        print("Entered coroutine");
        GameObject textDisp = Instantiate(textBoxPrefab, canvas.transform);
        textDisp.transform.Translate(0, -msgOffset * pendingMessages, 0);
        textDisp.GetComponent<Text>().text = message;
        pendingMessages++;
        yield return new WaitForSeconds(waitSeconds);
        Destroy(textDisp);
        pendingMessages--;
        print("End wait");
        print("Coroutine ended");
    }
}
