using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Effect_Controller : MonoBehaviour
{
    public static Effect_Controller instance;
    public float timeScale1;
    public float timeScale0;
    IEnumerator timeAction;
    public CanvasGroup canvasGroup;
    

    void Awake()
    {
        instance = this;
    }
    void Reset()
    {
        StopCoroutine(EffectButton());
        StartCoroutine(EffectButton());
    }
    void Start()
    {
        if (timeAction != null)
        {
            StopAllCoroutines();
            timeAction = null;
        }
        else
            timeAction = EffectButton();
        StartCoroutine(timeAction);
        
    }
    public IEnumerator EffectButton()
    {
        while (true)
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), timeScale1);
            //LeanTween.alphaCanvas(canvasGroup, 1, timeScale1);
            yield return Yielders.Get(timeScale1);
            LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), timeScale0);
            //LeanTween.alphaCanvas(canvasGroup, 0, timeScale0);
            yield return Yielders.Get(timeScale0);
            yield return null;
        }
    }
}
