using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokeImg_Controller : MonoBehaviour
{
    public string pokeID;
    public Point point;
    public Button pokeBtn;
    public Image pokeImg;

    public void Init(Sprite _pokeSprite,string _pokeID,int _value ) {
        pokeImg.sprite = _pokeSprite;
        pokeID = _pokeID;
        GetPos(_value);
    }
    public void GetPos(int _value) {
        point = new Point(_value);
        point.position = gameObject.transform.position;
    }
    public void OnBtnClick() {
        Pikachu_IngameManager.instance.EvtClick();
    }
}
