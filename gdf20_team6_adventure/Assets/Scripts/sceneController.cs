using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneController : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;

    public void changeScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void loadingScreen(string name) {
        StartCoroutine(changeSceneAsync(name));
    }

    IEnumerator changeSceneAsync(string name) {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(name);

        loadScreen.SetActive(true);

        while (!sceneLoad.isDone) {
            float progress = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            slider.value = progress;
            print(progress);
            yield return null;
        }
    }
}
