using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionController : MonoBehaviour
{
    public GameObject particleSystemPrefab;
    public string potionName;
    // Start is called before the first frame update
    void Start()
    {
        if (!(potionName == null)) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Potions/" + potionName);
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void targetPos(Vector2 pos) {
        StartCoroutine(moveToPosition(gameObject.transform.position, pos, .5f));
    }

    IEnumerator moveToPosition(Vector2 startPos, Vector2 targetPos, float time) {
        float startTime = Time.time;
        float endTime = startTime + time;
        Vector2 dist = targetPos - startPos;
        while (Time.time <= endTime) {
            float timePassed = Time.time - startTime;
            float pctPassed = timePassed / time;
            Vector2 arc = new Vector2(0, 4 * pctPassed - 4 * Mathf.Pow(pctPassed, 2));
            gameObject.transform.position = startPos + dist * pctPassed + arc;
            yield return new WaitForEndOfFrame();
        }
        GameObject ps = Instantiate(particleSystemPrefab);
        ps.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
