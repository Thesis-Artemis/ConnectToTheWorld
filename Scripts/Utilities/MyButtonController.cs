using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButtonController : MonoBehaviour {
    [SerializeField] CanvasGroup myCanvasGroup;
    [SerializeField] Button myButton;
    [SerializeField] Image imgShadow;

    public void SetInteractable(bool _flag){
        if(_flag){
            myCanvasGroup.alpha = 1f;
            myCanvasGroup.blocksRaycasts = true;
            imgShadow.gameObject.SetActive(false);
        }else{
            myCanvasGroup.alpha = 0.5f;
            myCanvasGroup.blocksRaycasts = false;
            imgShadow.gameObject.SetActive(true);
        }
    }
}
