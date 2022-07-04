using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;
    //Source
    public AudioSource musicSource;
    public AudioSource musicSourceInGame;
    public AudioSource soundSource;
    public AudioSource gameSourcePlayOnce;
    //Sound and Music
    public AudioClip[] inGameMusic;
    public AudioClip gameMusic;
    public AudioClip gameMusicIntro;
    public AudioClip pressButton;
    public AudioClip wrongSound;
    public AudioClip rightSound;
    public AudioClip payMoney;
    public AudioClip chooseLevelGame;
    public AudioClip winGame;
    public AudioClip ItemIceMusic;
    public AudioClip ItemClockMusic;
    public AudioClip EarthquakeSound;
    public AudioClip buttonSound, soundUseItemHammer;
    public AudioClip openDoorSound;
    public AudioClip starSound;
    public AudioClip loseGame;
    public AudioClip mm;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayMusicInTroGame();
    }

       
    public void PlayMusicInGame()//Bat Nhac trong luc choi game
    {
        if(musicSource && inGameMusic != null && inGameMusic.Length > 0)
        {
            int randomIndex = Random.Range(0, inGameMusic.Length);
            if(inGameMusic[randomIndex])
            {
                musicSourceInGame.clip = inGameMusic[randomIndex];
                musicSourceInGame.Play();
            }
        }
    }

    public void StopMusicInGame()//Dung Nhac trong Game
    {
        if (musicSource && inGameMusic != null && inGameMusic.Length > 0)
        {
            int randomIndex = Random.Range(0, inGameMusic.Length);
            if (inGameMusic[randomIndex])
            {
                musicSourceInGame.clip = inGameMusic[randomIndex];
                musicSourceInGame.Stop();
            }
        }
    }

    public void PlayMusicMain()// Bat Nhac sau khi an TaptoPlay
    {
        if(musicSource && gameMusic){
            musicSource.clip = gameMusic;   
            musicSource.Play();
        }
    }
    public void PlayMusicInTroGame()// Bat Nhac Intro vao game
    {
        if (musicSource && gameMusicIntro)
        {
            musicSource.clip = gameMusicIntro;
            musicSource.Play();
        }
    }

    public void StopMusicInTroGame()// Dung nhac Intro vao game
    {
        if (musicSource && gameMusicIntro)
        {
            musicSource.clip = gameMusicIntro;
            musicSource.Stop();
        }
    }

    public void PlayMusicWinGame()// Bat nhac win Game
    {
        if (gameSourcePlayOnce && winGame)
        {
            gameSourcePlayOnce.clip = winGame;
            gameSourcePlayOnce.Play();
        }
    }

    public void StopMusicWinGame()// Dung nhac win Game
    {
        if (gameSourcePlayOnce && winGame)
        {
            gameSourcePlayOnce.clip = winGame;
            gameSourcePlayOnce.Stop();
        }
    }

    public void PlayMusicLoseGame()// Bat nhac win Game
    {
        PlaySound(loseGame);
    }

   
    public void PlayMusicItemIce()// Bat nhac khi su dung Item Ice
    {
        if (musicSource && ItemIceMusic)
        {
            musicSource.clip = ItemIceMusic;
            musicSource.Play();
        }
    }
    public void StopMusicItemIce()// Dung nhac khi Item Ice het thoi gian su dung
    {
        if (musicSource && ItemIceMusic)
        {
            musicSource.clip = ItemIceMusic;
            musicSource.Stop();
        }
    }
    
    public void PlayClickButton()// Am thanh khi Bam vao 1 con pokemon
    {
        PlaySound(pressButton);
    }

    public void PlayRightButton()// Am thanh khi chon 2 cap pokemon giong nhau
    {
        PlaySound(rightSound);
    }

    public void PlayWrongButton()// Am thanh khi chon 2 cap pokemon khong giong nhau
    {
        PlaySound(wrongSound);
    }

    public void PlayPayMoney()// Am thanh khi mua item
    {
        PlaySound(payMoney);
    }

    public void PlayChooseLevelGame()// Am Thanh khi chon cac Level vao game
    {
        PlaySound(chooseLevelGame);
    }

    public void PlayOpenDoor()// Am Thanh khi mo cua vao shop hoac game
    {
        PlaySound(openDoorSound);
    }

    public void StarSound()// Am Thanh an sao
    {
        PlaySound(starSound);
    }
    public void PlaySound(AudioClip sound)// Ham` bat Am thanh
    {
        if(soundSource && sound)
        {
            soundSource.PlayOneShot(sound);
        }
    }
    
    public void PlayEarthquakeSound()// Am thanh dong dat khi Rung man hinh
    {
        PlaySound(EarthquakeSound);

    }

    public void PlayButtonSound()// Am thanh khi nhan vao bat khi button nao
    {
        PlaySound(buttonSound);

    }

    public void PlaySoundUseItemHammer()// Am thanh khi An vao su dung Item bua
    {
        PlaySound(soundUseItemHammer);

    }
    public void PlayMusicItemClock()// Nhac sau khi an Item Ice
    {
        PlaySound(ItemClockMusic);
    }
    public void MuteMusic()// Mute Nhac Main
    {
        if (musicSource)
        {
            musicSource.mute = true;
        }
    }

    public void MuteMusicInGame()// Mute Nhac Ingame
    {
        if (musicSourceInGame)
        {
            musicSourceInGame.mute = true;
        }
    }

    public void MuteSound()    //Mute Am thanh
    {
        if (soundSource)
        {
            soundSource.mute = true;
        }
    }

}
