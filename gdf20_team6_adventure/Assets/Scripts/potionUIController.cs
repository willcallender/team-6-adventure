using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionUIController : MonoBehaviour
{
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void draw() {
        id = "ice_potion";
        string path = "Sprites/Potions/" + id;
        print(path);
        Sprite sprite = Resources.Load<Sprite>(path);
        print(sprite);
        GetComponent<Image>().sprite = sprite;
    }
}
