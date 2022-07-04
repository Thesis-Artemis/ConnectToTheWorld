using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public bool isUnlocked = false;
    public Image lockImage;
    public Image[] starsImage;
    public Sprite[] starSprite;
    public Button button;
    public Level level;
    
    void Start() {
        InitUI();
        UpdateLevelButton();
        UnlockedLevel();
    }
    private void UpdateLevelButton()
    {
        if (isUnlocked)//level khong bi khoa
        {
            lockImage.gameObject.SetActive(false);
            button.interactable = true;
            for (int i=0; i< starsImage.Length; i++)
            {
                starsImage[i].gameObject.SetActive(true);
                
            }
            for(int i=0; i < level.starNumber; i++)
            {
                starsImage[i].sprite = starSprite[i];
            }

        }
        else // level bi khoa
        {
            lockImage.gameObject.SetActive(true);
            button.interactable = false;
            for (int i = 0; i < starsImage.Length; i++)
            {
                starsImage[i].gameObject.SetActive(false);
            }
        }
    }
    private void UnlockedLevel()
    {
        int name = int.Parse(gameObject.name);
        if (Data_Manager.instance.listLevel.Count > 0)
        {
            foreach (Level _level in Data_Manager.instance.listLevel)
            {
                if (_level.levelNumber == name - 1)
                {
                    isUnlocked = true;
                    UpdateLevelButton();
                }
            }
        }
        else {
            Debug.LogError("Don't load data level");
        }
    }
    void InitUI() {
        foreach (Level _level in Data_Manager.instance.listLevel) {
            if (_level.levelNumber == int.Parse(gameObject.name)) {
                level = _level;
            }
        }
    }
}
