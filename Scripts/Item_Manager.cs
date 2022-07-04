using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    public static Item_Manager instance;
     
    //Txt Item
    [SerializeField] Text txtQuanlityItemIce;
    [SerializeField] Text txtQuanlityItemTime;
    [SerializeField] Text txtQuanlityItemStone;
    //Ice Item
    public IPopupManager icePopup;
    public float timeIce ;
    public bool itemIce ;
    IEnumerator timeActionIce;
    //Hammer Item
    public bool itemHammer;
  
    //Button UI 
    public Button btnIce;
    public Button btnTime;
    public Button btnHammer;
    // Dem so lan su dung trong Map
    public int countIceItem = 1;
    public int countTimeItem = 2;
    public int countHammerItem = 2;

   // public int countCouple =2;
    public StateItemIce stateItemIce;
    public StateItemHammer stateItemHammer;
    public enum StateItemIce
    {
        Playing,
        Paused,
        Finish
    }

    public enum StateItemHammer
    {
        Playing,
        Paused,
        Finish
    }

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Pikachu_IngameManager.instance.listPoke.Count == 0)
        {
            Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Finish;
        }
        if (timeIce > 0 && stateItemIce == StateItemIce.Playing || stateItemHammer == StateItemHammer.Playing || Button_Manager.instance.isPause==true)
        {
            Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Paused;
        }
        LoadQuanlityItemIce();
        LoadQuanlityItemTime();
        LoadQuanlityItemHammer();
    }
    void Start()
    {
        icePopup.Hide2();
        
        if (timeActionIce != null)
        {
            StopAllCoroutines();
            timeActionIce = null;
        }
        else
            timeActionIce = CheckFrozen();
        StartCoroutine(timeActionIce);
        stateItemIce = StateItemIce.Finish;
        stateItemHammer = StateItemHammer.Finish;
        Debug.LogError("trang thai khi start: " + stateItemIce);


    }
    
    /// Su Dung Item Bang Dong Bang Time    
    public void UseIceItem()
    {
        
        itemIce = true;
        btnIce.interactable = false;
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 1 && Data_Manager.instance.listItem[i].quatityItem > 0 )
            {
                stateItemIce = StateItemIce.Playing;
                Item _item = new Item();
                _item.nameItem = "Ice Time";
                _item.itemID = 1;
                _item.priceItem = 12000;
                _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                Data_Manager.instance.listItem.RemoveAt(i);
                Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                //LoadQuanlityItemIce();
                break;
            }
        }
         StartCoroutine(CheckFrozen());
    }
    IEnumerator CheckFrozen()
    {       
        for(int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 1)
            {
                if(countIceItem > 0 && countIceItem <= 1){
                    
                    if (itemIce)
                    {
                        Audio_Manager.instance.PlayMusicItemIce();
                        Audio_Manager.instance.StopMusicInGame();
                        icePopup.Show2();
                        Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Paused;
                        while (timeIce > -1)
                         {
                            if (timeIce > 0 && stateItemIce == StateItemIce.Playing) 
                            {
                                Debug.Log(timeIce);
                                timeIce -= Time.deltaTime;
                            }
                            else if(timeIce < 0)
                            {
                                Audio_Manager.instance.StopMusicItemIce();
                                Audio_Manager.instance.PlayMusicInGame();
                                itemIce = false;
                                icePopup.Hide2();
                                stateItemIce = StateItemIce.Finish;
                                if (Pikachu_IngameManager.instance.listPoke.Count == 0)
                                {                             
                                    Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Finish;
                                }
                                else
                                {
                                    Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Playing;
                                }
                                
                                timeIce = 10;                              
                                countIceItem--;
                                if(countIceItem == 0)
                                {
                                    btnIce.interactable = false;
                                }
                                else
                                {
                                    btnIce.interactable = true;
                                }                                                                                                
                                break;
                            }
                            yield return null;
                        }
                    }
                }
                //else
                //{
                //    Debug.Log("Ban da xai het luot");
                //}
            }

        } 
    }
    public void CheckUseHammerItem()
    {
        
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 3)
            {
                if(countHammerItem > 0 && countHammerItem <= 2)
                {                   
                    if (itemHammer)
                    {
                        if(countHammerItem > 0 )
                        {
                            Pikachu_IngameManager.instance.CreateHammer();
                            countHammerItem--;
                            StartCoroutine(WaitUseHammer());                           
                            break;
                        }                        
                        
                    }
                }
            }
        }          
    }
    /// Su Dung Item Dong Ho De Cong Them Thoi Gian
    public void UseClockItem()
    {
        
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 2)
            {
                if (countTimeItem > 0 && countTimeItem <= 2)
                {
                    if (Data_Manager.instance.levelTemp >= 1 && Data_Manager.instance.levelTemp <= 4)
                    {
                        if (Pikachu_IngameManager.instance.timePlay > 0 && Pikachu_IngameManager.instance.timePlay <= 55)
                        {
                            Pikachu_IngameManager.instance.timePlay += 5;
                            Audio_Manager.instance.PlayMusicItemClock();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                            Data_Manager.instance.listItem.RemoveAt(i);
                            Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                            countTimeItem--;
                            if (countTimeItem == 0)
                            {
                                btnTime.interactable = false;
                            }
                            else
                            {
                                btnTime.interactable = true;
                            }
                            break;
                        }
                    }
                    else if (Data_Manager.instance.levelTemp >= 5 && Data_Manager.instance.levelTemp <= 12)
                    {
                        if (Pikachu_IngameManager.instance.timePlay > 0 && Pikachu_IngameManager.instance.timePlay <= 95)
                        {
                            Pikachu_IngameManager.instance.timePlay += 5;
                            Audio_Manager.instance.PlayMusicItemClock();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                            Data_Manager.instance.listItem.RemoveAt(i);
                            Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                            countTimeItem--;
                            if (countTimeItem == 0)
                            {
                                btnTime.interactable = false;
                            }
                            else
                            {
                                btnTime.interactable = true;
                            }
                            break;
                        }
                    }
                    else if (Data_Manager.instance.levelTemp >= 13 && Data_Manager.instance.levelTemp <= 19)
                    {
                        if (Pikachu_IngameManager.instance.timePlay > 0 && Pikachu_IngameManager.instance.timePlay <= (100 + ((Data_Manager.instance.levelTemp % 10) * 5)) -5)
                        {
                            Pikachu_IngameManager.instance.timePlay += 5;
                            Audio_Manager.instance.PlayMusicItemClock();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                            Data_Manager.instance.listItem.RemoveAt(i);
                            Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                            countTimeItem--;
                            if (countTimeItem == 0)
                            {
                                btnTime.interactable = false;
                            }
                            else
                            {
                                btnTime.interactable = true;
                            }
                            break;
                        }
                    }
                    else if (Data_Manager.instance.levelTemp == 20)
                    {
                        if (Pikachu_IngameManager.instance.timePlay > 0 && Pikachu_IngameManager.instance.timePlay <= 145)
                        {
                            Pikachu_IngameManager.instance.timePlay += 5;
                            Audio_Manager.instance.PlayMusicItemClock();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                            Data_Manager.instance.listItem.RemoveAt(i);
                            Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                            countTimeItem--;
                            if (countTimeItem == 0)
                            {
                                btnTime.interactable = false;
                            }
                            else
                            {
                                btnTime.interactable = true;
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (Pikachu_IngameManager.instance.timePlay > 0 && Pikachu_IngameManager.instance.timePlay <= 160 + ((Data_Manager.instance.levelTemp % 10) * 5) -5)
                        {
                            Pikachu_IngameManager.instance.timePlay += 5;
                            Audio_Manager.instance.PlayMusicItemClock();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                            Data_Manager.instance.listItem.RemoveAt(i);
                            Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                            countTimeItem--;
                            if (countTimeItem == 0)
                            {
                                btnTime.interactable = false;
                            }
                            else
                            {
                                btnTime.interactable = true;
                            }
                            break;
                        }
                    }
                    Debug.LogError("Bien dem hien tai: " + countTimeItem);

                }//else
                 //{
                 //    Debug.Log("Ban da xai het luot");
                 //}

            }
        }
        LoadQuanlityItemTime();
    }
    /// Su dung Item Ston de chon 2 hinh giong nhau
    public void UseHammerItem()
    {
        Audio_Manager.instance.PlaySoundUseItemHammer();
        itemHammer = true;
        btnHammer.interactable = false;
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 3 )
            {
                stateItemHammer = StateItemHammer.Playing;
                Item _item = new Item();
                _item.nameItem = "God Hammer";
                _item.itemID = 3;
                _item.priceItem = 9000;
                Debug.Log("so luong item 1: " + _item.quatityItem);
                _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem - 1;
                Debug.Log("so luong item 2: " + _item.quatityItem);
                Data_Manager.instance.listItem.RemoveAt(i);
                Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                //LoadQuanlityItemStone();
                break;
            }

        }
        CheckUseHammerItem();
    }
    void LoadQuanlityItemIce()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 1)
            {
                if (Data_Manager.instance.listItem[i].quatityItem == 0)
                {
                    btnIce.interactable = false;                   
                    txtQuanlityItemIce.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
                else
                {
                    txtQuanlityItemIce.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
            }     
        }
    }

    void LoadQuanlityItemTime()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 2)
            {
                if (Data_Manager.instance.listItem[i].quatityItem == 0)
                {
                    btnTime.interactable = false;
                    txtQuanlityItemTime.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
                else
                {
                    txtQuanlityItemTime.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
            }
        }
    }
    void LoadQuanlityItemHammer()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 3)
            {
                if (Data_Manager.instance.listItem[i].quatityItem == 0)
                {
                    btnHammer.interactable = false;
                    txtQuanlityItemStone.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
                else
                {
                    txtQuanlityItemStone.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
                }
            }
        }
    }
    public void Check2TheSamePoke()
    {
        Vector2Int p1 = new Vector2Int();
        Vector2Int p2 = new Vector2Int();
        
        int count = 0;
        
        //Debug.Log(Pikachu_IngameManager.instance.listPoke.Count);
            if(Pikachu_IngameManager.instance.listPoke.Count > 4)
            {
                int ranIndex = Random.Range(1, 3);
                Debug.LogError("gia tri pokeRandom: list 1 " + ranIndex);
                for (int i = 0; i < Pikachu_IngameManager.instance.listPoke.Count; i++)
                {
                    p1 = Pikachu_IngameManager.instance.listPoke[i].point.posMatrix;
                    if (ranIndex > 0)
                    {
                        if (count == 0)
                        {
                            for (int j = i + 1; j < Pikachu_IngameManager.instance.listPoke.Count; j++)
                            {
                                p2 = Pikachu_IngameManager.instance.listPoke[j].point.posMatrix;
                                int index = Pikachu_IngameManager.instance.listPoke[i].name.LastIndexOf(",");
                                p1.x = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(0, index));
                                p1.y = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(index + 1));
                                p2.x = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(0, index));
                                p2.y = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(index + 1));
                            if (Pikachu_IngameManager.instance.map.checkdLine(p1, p2))
                            {

                                Pikachu_IngameManager.instance.Execute(p1, p2);
                                Pikachu_IngameManager.instance.SetModeGame(Pikachu_IngameManager.instance.level, Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                Pikachu_IngameManager.instance.map.UpdateValueButton(Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                Pikachu_IngameManager.instance.CheckLogicGame();
                                ranIndex--;
                                break;
                            }
                        }

                        }
                        else
                        {
                            break;
                        }

                    }
                }

            }else if (Pikachu_IngameManager.instance.listPoke.Count > 2)
                {
                int ranIndex = Random.Range(1,2);
                Debug.LogError("gia tri pokeRandom: list 2 " + ranIndex);
                for (int i = 0; i < Pikachu_IngameManager.instance.listPoke.Count; i++)
                {
                    p1 = Pikachu_IngameManager.instance.listPoke[i].point.posMatrix;
                    if (ranIndex > 0)
                    {
                        if (count == 0)
                        {

                            for (int j = i + 1; j < Pikachu_IngameManager.instance.listPoke.Count; j++)
                            {
                                p2 = Pikachu_IngameManager.instance.listPoke[j].point.posMatrix;
                                int index = Pikachu_IngameManager.instance.listPoke[i].name.LastIndexOf(",");
                                p1.x = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(0, index));
                                p1.y = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(index + 1));
                                p2.x = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(0, index));
                                p2.y = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(index + 1));
                                if (Pikachu_IngameManager.instance.map.checkdLine(p1, p2))
                                {

                                    Pikachu_IngameManager.instance.Execute(p1, p2);
                                    Pikachu_IngameManager.instance.SetModeGame(Pikachu_IngameManager.instance.level, Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.map.UpdateValueButton(Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.CheckLogicGame();
                                    ranIndex--;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }else if (Pikachu_IngameManager.instance.listPoke.Count > 2)
                {
                int ranIndex = Random.Range(1,2);
                Debug.LogError("gia tri pokeRandom: list 3 " + ranIndex);
                for (int i = 0; i < Pikachu_IngameManager.instance.listPoke.Count; i++)
                {
                    p1 = Pikachu_IngameManager.instance.listPoke[i].point.posMatrix;
                    if (ranIndex > 0)
                    {
                        if (count == 0)
                        {

                            for (int j = i + 1; j < Pikachu_IngameManager.instance.listPoke.Count; j++)
                            {
                                p2 = Pikachu_IngameManager.instance.listPoke[j].point.posMatrix;
                                int index = Pikachu_IngameManager.instance.listPoke[i].name.LastIndexOf(",");
                                p1.x = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(0, index));
                                p1.y = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(index + 1));
                                p2.x = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(0, index));
                                p2.y = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(index + 1));
                                if (Pikachu_IngameManager.instance.map.checkdLine(p1, p2))
                                {

                                    Pikachu_IngameManager.instance.Execute(p1, p2);
                                    Pikachu_IngameManager.instance.SetModeGame(Pikachu_IngameManager.instance.level, Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.map.UpdateValueButton(Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.CheckLogicGame();
                                    ranIndex--;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }
            else 
            {
                int ranIndex = 1;
                Debug.LogError("gia tri pokeRandom: " + ranIndex);
                for (int i = 0; i < Pikachu_IngameManager.instance.listPoke.Count; i++)
                {
                    p1 = Pikachu_IngameManager.instance.listPoke[i].point.posMatrix;
                    if (ranIndex > 0)
                    {
                        if (count == 0)
                        {

                            for (int j = i + 1; j < Pikachu_IngameManager.instance.listPoke.Count; j++)
                            {
                                p2 = Pikachu_IngameManager.instance.listPoke[j].point.posMatrix;
                                int index = Pikachu_IngameManager.instance.listPoke[i].name.LastIndexOf(",");
                                p1.x = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(0, index));
                                p1.y = int.Parse(Pikachu_IngameManager.instance.listPoke[i].name.Substring(index + 1));
                                p2.x = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(0, index));
                                p2.y = int.Parse(Pikachu_IngameManager.instance.listPoke[j].name.Substring(index + 1));
                                if (Pikachu_IngameManager.instance.map.checkdLine(p1, p2))
                                {

                                    Pikachu_IngameManager.instance.Execute(p1, p2);
                                    Pikachu_IngameManager.instance.SetModeGame(Pikachu_IngameManager.instance.level, Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.map.UpdateValueButton(Pikachu_IngameManager.instance.rows, Pikachu_IngameManager.instance.cols);
                                    Pikachu_IngameManager.instance.CheckLogicGame();
                                    ranIndex--;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }
    }

    public IEnumerator WaitUseHammer()
    {
        yield return new WaitForSeconds(5f);
        Check2TheSamePoke();       
        itemHammer = false;
        if (countHammerItem == 0)
        {
            btnHammer.interactable = false;
        }
        else
        {
            btnHammer.interactable = true;
        }
        stateItemHammer = StateItemHammer.Finish;
        

    }
    

}

