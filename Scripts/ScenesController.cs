using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene("Scene_Main");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1_adventure");
    }
}
