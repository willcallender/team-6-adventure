using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionSelector : MonoBehaviour
{
    public List<string> knownPotions;
    int potionId;
    public string selectedPotion;
    GameObject potionImage;
    public GameObject potionImagePrefab;
    // Start is called before the first frame update
    void Start()
    {
        potionImage = Instantiate(potionImagePrefab, gameObject.transform);
        selectedPotion = "dud_potion";
        potionImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Potions/" + selectedPotion);
    }

    // Update is called once per frame
    void Update()
    {
        int scroll = (int)Input.mouseScrollDelta.y;
        if (scroll != 0f && knownPotions.Count > 1) {
            potionId += scroll;
            if (potionId >= knownPotions.Count) {
                potionId = potionId % knownPotions.Count;
            }
            while (potionId < 0) {
                potionId += knownPotions.Count;
            }
            Destroy(potionImage);
            potionImage = Instantiate(potionImagePrefab, gameObject.transform);
            selectedPotion = knownPotions[potionId];
            potionImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Potions/" + selectedPotion);
        }
    }


}
