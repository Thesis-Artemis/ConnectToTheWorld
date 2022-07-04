using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Manager : MonoBehaviour
{
    public static Main_Manager instance;
    //Main
    public IPopupManager shopPopup;
    public IPopupManager gamePopup;
    public IPopupManager professorPopup;
    public IPopupManager sayPopup;
    public IPopupManager wordSayPopup1, wordSayPopup2, wordSayPopup3, wordSayPopup4, wordSayPopup5;
    public IPopupManager intructuionPopup;
   // public GameObject say1, say2, say3, say4, say5;
    public GameObject shopAndGamePanel;
    public GameObject headerShop;
    public GameObject headerButtonPanel;
    public GameObject itemShopPanel;
    public GameObject coinPanel;
    public GameObject backgourndIntructionPanel;
    public GameObject intructuionPanel;
    //public GameObject closeButton;

    //Shop
    public GameObject shopPanel;
    [SerializeField] IPopupManager headerShopPopup;
    [SerializeField] IPopupManager itemPopup;

    void Awake()
    {
        instance = this;
     
    }
    void Start()
    {
        headerShop.SetActive(true);
        headerShopPopup.Show2();
        shopAndGamePanel.SetActive(true);
        headerButtonPanel.SetActive(true);
        if(Audio_Manager.instance.musicSource.mute == false && Audio_Manager.instance.musicSourceInGame.mute == false)
        {

            Button_Manager.instance.playMusic.SetActive(true);
        }
        else
        {
            Button_Manager.instance.stopMusic.SetActive(true);
        }

        if (Audio_Manager.instance.soundSource.mute == false )
        {

            Button_Manager.instance.playSound.SetActive(true);
        }
        else
        {
            Button_Manager.instance.stopSound.SetActive(true);
        }
        //
        coinPanel.SetActive(true);
        shopPopup.Show2();
        gamePopup.Show2();
        //closeButton.SetActive(false);
        professorPopup.Hide2();
        sayPopup.Hide2();
        wordSayPopup1.Hide2();
        wordSayPopup2.Hide2();
        wordSayPopup3.Hide2();
        wordSayPopup4.Hide2();
        wordSayPopup5.Hide2();
        StartCoroutine(InitData());
    }
    IEnumerator InitData()
    {
        yield return new WaitUntil(()=> Data_Manager.instance.score != null);
        Shop_Manager.instance.txtScore.text = Data_Manager.instance.score.totalScore.ToString();
        Shop_Manager.instance.LoadQuanlityItemIce();
        Shop_Manager.instance.LoadQuanlityItemTime();
        Shop_Manager.instance.LoadQuanlityItemHummer();
    }
    public void LoadSceneSelection()
    {
        Audio_Manager.instance.PlayOpenDoor();
        
        SceneManager.LoadScene("Scene_Map");
       ;

    }
    public void LoadSceneAdventure()
    {
        Audio_Manager.instance.PlayOpenDoor();

        SceneManager.LoadScene("Level1_adventure");
        ;

    }
    public void LoadShop()
    {
        Audio_Manager.instance.PlayOpenDoor();
        
        shopPopup.Hide2();
        gamePopup.Hide2();
        shopAndGamePanel.SetActive(false);
        shopPanel.SetActive(true);
        headerShopPopup.Show2();
        headerButtonPanel.SetActive(false);
        itemShopPanel.SetActive(true);
        itemPopup.Show2();
        //closeButton.SetActive(true);
        professorPopup.Show2();
        sayPopup.Show2();
        wordSayPopup1.Show2();
        

    }
    public void CloseShop()
    {
        Audio_Manager.instance.PlayOpenDoor();
        SceneManager.LoadScene("Scene_Main");
        
    }
    public void Instruction()
    {
        Audio_Manager.instance.PlayButtonSound();
        backgourndIntructionPanel.SetActive(true);
        intructuionPanel.SetActive(true);
        intructuionPopup.Show2();
    }

    public void CloseIntruction()
    {
        Audio_Manager.instance.PlayButtonSound();
        backgourndIntructionPanel.SetActive(false);
        intructuionPopup.Hide2();
        StartCoroutine(WaitClose());           
    }
    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(0.2f);
        intructuionPanel.SetActive(false);
    }

    
}
