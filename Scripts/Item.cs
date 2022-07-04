using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]

public class Item 
{
    public string nameItem;
    public int itemID;
    public int quatityItem;
    public long priceItem;

    public Item()
    {
        nameItem = "";
        itemID = 0;
        quatityItem = 0;
        priceItem = 0;
    }
}
