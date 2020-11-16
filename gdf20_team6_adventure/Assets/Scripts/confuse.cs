using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confuse : MonoBehaviour
{
    public GameObject enemy;
    public GameObject confuseTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        enemy.GetComponent<genericEnemyAI>().confuse(confuseTarget, 3f);
    }
}
