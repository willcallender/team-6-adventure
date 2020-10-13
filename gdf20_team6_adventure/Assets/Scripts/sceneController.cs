using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    public void changeScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void loadingScreen(string name) {
        StartCoroutine(changeSceneAsync(name));
    }

    IEnumerator changeSceneAsync(string name) {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(name);

        while (sceneLoad.progress < 1) {
            print(sceneLoad.progress);
            yield return new WaitForEndOfFrame();
        }
    }
}
