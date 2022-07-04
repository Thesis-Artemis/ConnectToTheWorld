using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad_Manager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public static SceneLoad_Manager instance;
    void Awake()
    {
        instance = this;

    }
    void Start()
    {
        StartCoroutine(EffectTapToPlayText());
    }
    public void LoadTapToPlay()
    {
        SceneManager.LoadScene("Scene_Main");
        Audio_Manager.instance.PlayButtonSound();
        Audio_Manager.instance.StopMusicInTroGame();
        Audio_Manager.instance.PlayMusicMain();

    }

    IEnumerator EffectTapToPlayText()
    {
        while (true)
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f);
            LeanTween.alphaCanvas(canvasGroup, 1, 0.5f);
            yield return Yielders.Get(0.5f);
            LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
            LeanTween.alphaCanvas(canvasGroup, 0, 0.5f);
            yield return Yielders.Get(0.5f);
            yield return null;
        }
    }
}
