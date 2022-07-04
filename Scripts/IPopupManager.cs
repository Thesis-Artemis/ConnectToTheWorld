using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class IPopupManager :MonoBehaviour
{
    [SerializeField]CanvasGroup canvasGroup;
    [SerializeField]Transform mainContainer;
    public GameObject menuPanel;

    //[Button]void ShowLosePopup()
    //{
    //    Show();
    //}
    //[Button]void ShowHidePopup()
    //{
    //    Hide();
    //}   
    void Awake() {
        Init();
    }
    public void Init() {
        canvasGroup.alpha = 0;
    }
    public void SetStar(int _startNumber) { 
        
    }
    public void Show() {
        menuPanel.SetActive(true);
        menuPanel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        LeanTween.scale(menuPanel.gameObject, Vector3.one, 0.2f);

        mainContainer.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        LeanTween.scale(mainContainer.gameObject, Vector3.one, 0.2f);
        LeanTween.alphaCanvas(canvasGroup,1,0.2f);
        
    }
    public void Hide() {        
        LeanTween.scale(menuPanel.gameObject, Vector3.zero, 0.2f);      
        LeanTween.scale(mainContainer.gameObject, Vector3.zero, 0.2f);
        LeanTween.alphaCanvas(canvasGroup, 0, 0.2f);
        StartCoroutine(WaitHide());
        
    }

    public void Show2()
    {
        mainContainer.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        LeanTween.scale(mainContainer.gameObject, Vector3.one, 0.3f);
        LeanTween.alphaCanvas(canvasGroup, 1, 0.2f);

    }
    public void Hide2()
    {
        LeanTween.scale(mainContainer.gameObject, Vector3.zero, 0.3f);
        LeanTween.alphaCanvas(canvasGroup, 0, 0.2f);
        //StartCoroutine(WaitHide());

    }

    public IEnumerator WaitHide()
    {
        yield return new WaitForSeconds(0.3f);
        menuPanel.SetActive(false);
    }

   
}
