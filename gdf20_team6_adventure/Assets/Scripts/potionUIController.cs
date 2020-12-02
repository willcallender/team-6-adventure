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
        Sprite sprite = Resources.Load<Sprite>("Sprites/Potions/" + id);
        GetComponent<Image>().sprite = sprite;
    }
}
