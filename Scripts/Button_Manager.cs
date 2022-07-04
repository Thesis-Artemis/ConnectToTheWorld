using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Button_Manager : MonoBehaviour
{
    public static Button_Manager instance;
    public IPopupManager pausePopup;
    public GameObject pausePanel;

    public GameObject stopMusic;
    public GameObject playMusic;

    public GameObject stopSound;
    public GameObject playSound;
    public bool isPause;
    void Awake()
    {
        instance = this;
    }

    
    public void PauseButton()
    {
        isPause = true;
        Audio_Manager.instance.PlayButtonSound();
        Pikachu_IngameManager.instance.rainEffect.Pause();
        Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Paused;
        pausePanel.SetActive(true);
        pausePopup.Show();
        Pikachu_IngameManager.instance.winPopup.Hide2();
        Pikachu_IngameManager.instance.losePopup.Hide2();
        Pikachu_IngameManager.instance.pauseEffect.Play();
        if (Item_Manager.instance.timeIce > 0 && Item_Manager.instance.stateItemIce == Item_Manager.StateItemIce.Playing)
        {
            Item_Manager.instance.stateItemIce = Item_Manager.StateItemIce.Paused;
        }
    }
    public void ContinueButton()
    {
        isPause = false;
        Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Playing;       
        Audio_Manager.instance.PlayButtonSound();
        Debug.LogError("thoi gian thuc: " + Pikachu_IngameManager.instance.state);
        pausePopup.Hide();
        pausePanel.SetActive(false);
        Pikachu_IngameManager.instance.rainEffect.Play();       
        //StartCoroutine(Pikachu_IngameManager.instance.StartTimePlay());
        Pikachu_IngameManager.instance.pauseEffect.Stop();
        if (Item_Manager.instance.timeIce > 0 && Item_Manager.instance.stateItemIce == Item_Manager.StateItemIce.Paused)
        {
            Item_Manager.instance.stateItemIce = Item_Manager.StateItemIce.Playing;
        }
        // Item_Manager.instance.stateItem = Item_Manager.StateItem.Playing;

    }
    public void MenuButton()
    {
        Audio_Manager.instance.PlayButtonSound();
        SceneManager.LoadScene("Scene_Map");
        Time.timeScale = 1f;
        Audio_Manager.instance.musicSourceInGame.Stop();
        Audio_Manager.instance.PlayMusicMain();
        Audio_Manager.instance.StopMusicWinGame();
    }
    public void LoadSceneLevel(int _level)
    {
        Data_Manager.instance.levelTemp = _level;
        SceneManager.LoadScene("Scene_GamePlay");
        //Audio_Manager.instance.StopMusic();
        Audio_Manager.instance.PlayChooseLevelGame();

    }
    public void LoadSceneReLoad()
    {
        Audio_Manager.instance.PlayButtonSound();
        SceneManager.LoadScene("Scene_GamePlay");
        Data_Manager.instance.LoadDataPlayer();
        Audio_Manager.instance.StopMusicWinGame();

    }
    public void LoadNextLevel()
    {
        Audio_Manager.instance.PlayButtonSound();
        Data_Manager.instance.levelTemp = Data_Manager.instance.levelTemp+1;
        SceneManager.LoadScene("Scene_GamePlay");
        Audio_Manager.instance.StopMusicWinGame();
        //Time.timeScale = 1f;
    }

    public void StopMusic()
    {
        Audio_Manager.instance.PlayButtonSound();
        playMusic.SetActive(false);
        stopMusic.SetActive(true);
        Audio_Manager.instance.MuteMusic();
        Audio_Manager.instance.MuteMusicInGame();

    }

    public void PlayMusic()
    {
        Audio_Manager.instance.PlayButtonSound();
        playMusic.SetActive(true);
        stopMusic.SetActive(false);
        Audio_Manager.instance.musicSource.mute = false;
        Audio_Manager.instance.musicSourceInGame.mute = false;
    }

    public void PlaySound()
    {
        Audio_Manager.instance.PlayButtonSound();
        playSound.SetActive(true);
        stopSound.SetActive(false);
        Audio_Manager.instance.soundSource.mute = false;
    }

    public void StopSound()
    {
        Audio_Manager.instance.PlayButtonSound();
        playSound.SetActive(false);
        stopSound.SetActive(true);
        Audio_Manager.instance.MuteSound();

    }
    void OnApplicationPause()
    {
        isPause = true;
        Audio_Manager.instance.PlayButtonSound();
        Pikachu_IngameManager.instance.rainEffect.Pause();
        Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Paused;
        pausePanel.SetActive(true);
        pausePopup.Show();
        Pikachu_IngameManager.instance.winPopup.Hide2();
        Pikachu_IngameManager.instance.losePopup.Hide2();
        Pikachu_IngameManager.instance.pauseEffect.Play();
        if (Item_Manager.instance.timeIce > 0 && Item_Manager.instance.stateItemIce == Item_Manager.StateItemIce.Playing)
        {
            Item_Manager.instance.stateItemIce = Item_Manager.StateItemIce.Paused;
        }
    }

}
