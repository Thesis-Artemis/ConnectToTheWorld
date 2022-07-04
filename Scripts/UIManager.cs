using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mapSelectionPanel;    
    public GameObject[] levelSelectionPanel;
    

    [Header("Star UI")]
    public MapSelection[] mapSelections;
    public Text starText;
    public Text[] questStarText;
    public Text[] lockedStarText;
    public Text[] unlockStarText;
    public int stars ;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Init();
        
        
    }
    void Init()
    {
        UpdateStarUI();
        UpdateLockedStarUI();
        UpdateUnlockedStarUI();
    }
    void UpdateLockedStarUI()
    {
        for (int i = 0; i < mapSelections.Length; i++)
        {
            questStarText[i].text = mapSelections[i].starQuestNum.ToString();
            
        }
    }
    void UpdateUnlockedStarUI()
    {
        int _indexStart = 0,_indexFinish =0,_totalStar=0, _totalStar1 = 0, _totalStar2 = 0, _totalStar3 = 0;
        for (int i = 0; i < mapSelections.Length; i++)
        {
            unlockStarText[i].text = stars.ToString() + "/" + mapSelections[i].endLevel * 3;
            switch (i)
            {
                case (0)://Map1
                    _indexStart = 0;
                    _indexFinish = 3;
                    for (int j = _indexStart; j <= _indexFinish; j++)
                    {
                        if (j < Data_Manager.instance.listLevel.Count)
                            _totalStar += Data_Manager.instance.listLevel[j].starNumber;
                    }
                    unlockStarText[i].text = _totalStar.ToString() + "/" + (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 3;
                    break;
                case (1)://Map2
                    _indexStart = 4;
                    _indexFinish =11;
                    for (int j = _indexStart; j <= _indexFinish; j++)
                    {
                        if (j < Data_Manager.instance.listLevel.Count)
                            _totalStar1 += Data_Manager.instance.listLevel[j].starNumber;
                    }
                    unlockStarText[i].text = _totalStar1.ToString() + "/" + (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 3;
                    break;
                case (2)://Map3
                    _indexStart = 12;
                    _indexFinish = 19;
                    for (int j = _indexStart; j <= _indexFinish; j++)
                    {
                        if (j < Data_Manager.instance.listLevel.Count)
                            _totalStar2 += Data_Manager.instance.listLevel[j].starNumber;
                    }
                    unlockStarText[i].text = _totalStar2.ToString() + "/" + (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 3;
                    break;
                case (3)://Map4
                    _indexStart = 20;
                    _indexFinish = 27;
                    for (int j = _indexStart; j <= _indexFinish; j++)
                    {
                        if (j < Data_Manager.instance.listLevel.Count)
                            _totalStar3 += Data_Manager.instance.listLevel[j].starNumber;
                    }
                    unlockStarText[i].text = _totalStar3.ToString() + "/" + (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 3;
                    break;

            }
            
        }
    }
    void UpdateStarUI()
    {
        for (int i = 0; i < Data_Manager.instance.listLevel.Count; i++) {
            stars += Data_Manager.instance.listLevel[i].starNumber;
        }
        //stars = 58;
        starText.text = stars.ToString();
    }
    public void PressMapButton(int _mapIndex)
    {
        Audio_Manager.instance.PlayButtonSound();
        if (mapSelections[_mapIndex].isUnlock == true)
        {
            levelSelectionPanel[_mapIndex].gameObject.SetActive(true);
            mapSelectionPanel.gameObject.SetActive(false);
        }
        
    }
    public void BackButton()
    {
        Audio_Manager.instance.PlayButtonSound();
        mapSelectionPanel.gameObject.SetActive(true);
        for(int i=0; i < mapSelections.Length; i++)
        {
            levelSelectionPanel[i].gameObject.SetActive(false);
        }
    }

    public void CloseMapSelection()
    {
        Audio_Manager.instance.PlayOpenDoor();
        SceneManager.LoadScene("Scene_Main");
    
    }
    

    
}
