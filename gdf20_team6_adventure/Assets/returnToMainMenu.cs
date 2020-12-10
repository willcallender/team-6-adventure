using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToMainMenu : MonoBehaviour
{
    public void returnToMenu() {
        SceneManager.LoadScene("Main Menu");
    }
}
