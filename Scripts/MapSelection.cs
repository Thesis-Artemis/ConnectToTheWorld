using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public bool isUnlock = false;
    public GameObject lockGo;
    public GameObject unlockGo;
    public int indexMap;// lv trong map
    public int starQuestNum;// So sao yeu cau
    public int startLevel;
    public int endLevel;
   
    //private UIManager uiManager;
    private void Update()
    {
        UpdateMapStautus();
        UnlockMap();
    }
    
    void UpdateMapStautus()
    {
        if (isUnlock)//Co the choi map nay
        {
            unlockGo.gameObject.SetActive(true);
            lockGo.gameObject.SetActive(false);
        }
        else//map nay bi khoa, yeu cau phai hoan thanh quest
        {
            unlockGo.gameObject.SetActive(false);
            lockGo.gameObject.SetActive(true);
        }
    }

    void UnlockMap()
    {
        //Debug.Log(uiManager.stars);
        if(UIManager.instance.stars >= starQuestNum)
        {
            isUnlock = true;
        }
        else
        {
            isUnlock = false; 
        }
    }

}
