using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_Manager : MonoBehaviour
{
    public static Shop_Manager instance;

    [System.Serializable]
    class ShopItem
    {
        public Sprite image;
        public int price;
        //public bool value;
        
    }

    [SerializeField] Transform shopScrollView;

    [SerializeField] List<ShopItem> listItem;
    GameObject iTems;
    GameObject g;
    Button buyBtn;
    EventTrigger seeIntructionBtn;
   
    public Text txtScore;
    [SerializeField] Text txtQuanlityItemIce;
    [SerializeField] Text txtQuanlityItemTime;
    [SerializeField] Text txtQuanlityItemHammer;
    //Huong dan item
    public Image intrucTion1;
    
    //
    public bool buttonHeld1;
    int temp;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (buttonHeld1 == true)
        {
            intrucTion1.gameObject.SetActive(true);
        }

        if(buttonHeld1 == false)
        {
            intrucTion1.gameObject.SetActive(false);
        }
        
    }
        void Start()
        {
            iTems = shopScrollView.GetChild(0).gameObject;
            for(int i=0; i < listItem.Count; i++)
            {
                g = Instantiate(iTems, shopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = listItem[i].image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = listItem[i].price.ToString();
                buyBtn = g.transform.GetChild(1).GetComponent<Button>();
                buyBtn.AddListener(i, ClickButtonBuy);
                 
            }
            Destroy(iTems);       
    }
    void ClickButtonBuy(int _itemIndex)
    {
        
        if(_itemIndex == 0)
        {
            CheckBuyItem(_itemIndex);
            
        }
        else if(_itemIndex == 1)
        {
            CheckBuyItem(_itemIndex);
            
        }
        else
        {
            CheckBuyItem(_itemIndex);           
        }
    }
    void CheckBuyItem(int _product)
    {
            if (Data_Manager.instance.listItem.Count > 0)
            {
                for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
                {
                    //Item Ice
                    if (_product == 0)
                    {
                        if (Data_Manager.instance.score.totalScore >= 12000)
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Show2();
                            Main_Manager.instance.wordSayPopup3.Hide2();
                            Main_Manager.instance.wordSayPopup4.Hide2();
                            Main_Manager.instance.wordSayPopup5.Hide2();
                            Audio_Manager.instance.PlayPayMoney();
                            Item _item = new Item();
                            _item.nameItem = "Ice Time";
                            _item.itemID = 1;
                            _item.priceItem = 12000;
                            
                            if (Data_Manager.instance.listItem[i].itemID == _item.itemID)
                            {
                                Data_Manager.instance.score.totalScore -= _item.priceItem;
                                _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem + 1;
                                Data_Manager.instance.listItem.RemoveAt(i);
                                Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                break;
                            }
                            else
                            {
                                if (Data_Manager.instance.listItem.Count == _item.itemID
                                || Data_Manager.instance.listItem.Count == _item.itemID + 1
                                && Data_Manager.instance.listItem[0].itemID == 2 && Data_Manager.instance.listItem[1].itemID == 3
                                || Data_Manager.instance.listItem.Count == _item.itemID + 1
                                && Data_Manager.instance.listItem[0].itemID == 3 && Data_Manager.instance.listItem[1].itemID == 2)
                                {
                                    Data_Manager.instance.score.totalScore -= _item.priceItem;
                                    _item.quatityItem = 1;
                                    Debug.LogError("Item  save");
                                    Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                    break;
                                }
                            }
                        }
                        else 
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Hide2();
                            Main_Manager.instance.wordSayPopup3.Hide2();
                            Main_Manager.instance.wordSayPopup4.Hide2();
                            Main_Manager.instance.wordSayPopup5.Show2();
                        }

                    }
                    // Item Time
                    else if (_product == 1)
                    {
                        if (Data_Manager.instance.score.totalScore >= 8000)
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Hide2();
                            Main_Manager.instance.wordSayPopup3.Show2();
                            Main_Manager.instance.wordSayPopup4.Hide2();
                            Main_Manager.instance.wordSayPopup5.Hide2();
                            Audio_Manager.instance.PlayPayMoney();
                            Item _item = new Item();
                            _item.nameItem = "Time Clock";
                            _item.itemID = 2;
                            _item.priceItem = 8000;
                            
                            if (Data_Manager.instance.listItem[i].itemID == _item.itemID)
                            {
                                Data_Manager.instance.score.totalScore -= _item.priceItem;
                                _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem + 1;
                                Data_Manager.instance.listItem.RemoveAt(i);
                                Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                break;
                            }
                            else
                            {
                                if (Data_Manager.instance.listItem.Count == _item.itemID - 1
                                   || Data_Manager.instance.listItem.Count == _item.itemID
                                   && Data_Manager.instance.listItem[0].itemID == 1 && Data_Manager.instance.listItem[1].itemID == 3
                                   || Data_Manager.instance.listItem.Count == _item.itemID
                                   && Data_Manager.instance.listItem[0].itemID == 3 && Data_Manager.instance.listItem[1].itemID == 1)
                                {
                                    Data_Manager.instance.score.totalScore -= _item.priceItem;
                                    _item.quatityItem = 1;
                                    Debug.LogError("Item  save");
                                    Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Hide2();
                            Main_Manager.instance.wordSayPopup3.Hide2();
                            Main_Manager.instance.wordSayPopup4.Hide2();
                            Main_Manager.instance.wordSayPopup5.Show2();
                        }
                    }
                    //Item Hammer
                    else
                    {
                        if (Data_Manager.instance.score.totalScore >= 9000)
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Hide2();
                            Main_Manager.instance.wordSayPopup3.Hide2();
                            Main_Manager.instance.wordSayPopup4.Show2();
                            Main_Manager.instance.wordSayPopup5.Hide2();
                            Audio_Manager.instance.PlayPayMoney();
                            Item _item = new Item();
                            _item.nameItem = "God Hammer";
                            _item.itemID = 3;
                            _item.priceItem = 9000;
                           
                            if (Data_Manager.instance.listItem[i].itemID == _item.itemID)
                            {
                                Data_Manager.instance.score.totalScore -= _item.priceItem;
                                _item.quatityItem = Data_Manager.instance.listItem[i].quatityItem + 1;
                                Data_Manager.instance.listItem.RemoveAt(i);
                                Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                break;
                            }
                            else
                            {
                                if (Data_Manager.instance.listItem.Count == _item.itemID - 2
                                    || Data_Manager.instance.listItem.Count == _item.itemID - 1
                                    && Data_Manager.instance.listItem[0].itemID == 1 && Data_Manager.instance.listItem[1].itemID == 2
                                    || Data_Manager.instance.listItem.Count == _item.itemID - 1
                                    && Data_Manager.instance.listItem[0].itemID == 2 && Data_Manager.instance.listItem[1].itemID == 1)
                                {
                                    Data_Manager.instance.score.totalScore -= _item.priceItem;
                                    _item.quatityItem = 1;
                                    Debug.LogError("Item  save");
                                    Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Main_Manager.instance.wordSayPopup1.Hide2();
                            Main_Manager.instance.wordSayPopup2.Hide2();
                            Main_Manager.instance.wordSayPopup3.Hide2();
                            Main_Manager.instance.wordSayPopup4.Hide2();
                            Main_Manager.instance.wordSayPopup5.Show2();
                        }
                    }
                }               
            }
            else
            {
                //Item Ice
                if (_product == 0)
                {
                    if (Data_Manager.instance.score.totalScore >= 12000)
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Show2();
                        Main_Manager.instance.wordSayPopup3.Hide2();
                        Main_Manager.instance.wordSayPopup4.Hide2();
                        Main_Manager.instance.wordSayPopup5.Hide2();
                        Audio_Manager.instance.PlayPayMoney();
                        Item _item = new Item();
                        _item.nameItem = "Ice Time";
                        _item.itemID = 1;
                        _item.priceItem = 12000;
                        _item.quatityItem = 1;
                        Data_Manager.instance.score.totalScore -= _item.priceItem;
                        Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                    }
                    else
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Hide2();
                        Main_Manager.instance.wordSayPopup3.Hide2();
                        Main_Manager.instance.wordSayPopup4.Hide2();
                        Main_Manager.instance.wordSayPopup5.Show2();
                    }
                }
                    
                // Item Time
                else if (_product == 1)
                {
                    if (Data_Manager.instance.score.totalScore >= 8000)
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Hide2();
                        Main_Manager.instance.wordSayPopup3.Show2();
                        Main_Manager.instance.wordSayPopup4.Hide2();
                        Main_Manager.instance.wordSayPopup5.Hide2();
                        Audio_Manager.instance.PlayPayMoney();
                        Item _item = new Item();
                        _item.nameItem = "Time Clock";
                        _item.itemID = 2;
                        _item.priceItem = 8000;
                        _item.quatityItem = 1;
                        Data_Manager.instance.score.totalScore -= _item.priceItem;
                        Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                    }
                    else
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Hide2();
                        Main_Manager.instance.wordSayPopup3.Hide2();
                        Main_Manager.instance.wordSayPopup4.Hide2();
                        Main_Manager.instance.wordSayPopup5.Show2();
                    }

                }
                //Item Hammer
                else
                {
                    if (Data_Manager.instance.score.totalScore >= 9000)
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Hide2();
                        Main_Manager.instance.wordSayPopup3.Hide2();
                        Main_Manager.instance.wordSayPopup4.Show2();
                        Main_Manager.instance.wordSayPopup5.Hide2();
                        Audio_Manager.instance.PlayPayMoney();
                        Item _item = new Item();
                        _item.nameItem = "God Hammer";
                        _item.itemID = 3;
                        _item.priceItem = 9000;
                        _item.quatityItem = 1;
                        Data_Manager.instance.score.totalScore -= _item.priceItem;
                        Data_Manager.instance.SaveData<Item>(Data_Manager.instance.listItem, _item, "ListItem");
                    }
                    else
                    {
                        Main_Manager.instance.wordSayPopup1.Hide2();
                        Main_Manager.instance.wordSayPopup2.Hide2();
                        Main_Manager.instance.wordSayPopup3.Hide2();
                        Main_Manager.instance.wordSayPopup4.Hide2();
                        Main_Manager.instance.wordSayPopup5.Show2();
                    }

                }
            }
        txtScore.text = Data_Manager.instance.score.totalScore.ToString();
        Data_Manager.instance.SaveDataPlayer();
        LoadQuanlityItemIce();
        LoadQuanlityItemTime();
        LoadQuanlityItemHummer();
    }
    public void LoadQuanlityItemIce()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 1)
            {
                txtQuanlityItemIce.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
            }
        }
    }
    
    public void LoadQuanlityItemTime()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 2)
            {
                txtQuanlityItemTime.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
            }
        }
    }
    public void LoadQuanlityItemHummer()
    {
        for (int i = 0; i < Data_Manager.instance.listItem.Count; i++)
        {
            if (Data_Manager.instance.listItem[i].itemID == 3)
            {
                txtQuanlityItemHammer.text = Data_Manager.instance.listItem[i].quatityItem.ToString();
            }
        }
    }
    public void IntructionItem1()
    {       
        buttonHeld1 = true;        
    }
    
    public void ReleaseButton()
    {
        buttonHeld1 = false;
    }
    


}
