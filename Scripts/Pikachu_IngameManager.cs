using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class Pikachu_IngameManager : MonoBehaviour
{
    public static Pikachu_IngameManager instance;
    public List<PokeImg_Controller> listPoke;
    public Algorithm map;
    public int cols, rows;
    public List<Sprite> listSpriteRandom;
    public int level;
    int coin = 0;
    private Vector2Int FirstPos = Vector2Int.zero;
    private Vector2Int SecondPos = Vector2Int.zero;
    public PokeImg_Controller[][] MatrixPoke;
    [SerializeField] GridLayoutGroup gridPoke;
    [SerializeField] Text txtTimePlay;
    [SerializeField] Text txtScore1,txtScore2;
    public ParticleSystem thunderAppear;
    // Popup lose and win
    public IPopupManager losePopup;
    public IPopupManager winPopup;
    // Panel Lose and Win
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject scoreAddPanel;
    public GameObject pokeimg;
    public Text scoreAddText;
    [SerializeField] ParticleSystem starEffect;
    public ParticleSystem rainEffect;
    public ParticleSystem pauseEffect;
    public float timePlay;
    IEnumerator timeAction, checkEndGameAction;
    public GameObject[] starsYellow;
    public GameObject[] levelPanel;
    public StateGame state;
    public  Hammer_ItemController hammerPrefabs;
    public GameObject hammerWall;
    
    public enum StateGame {
        Playing,
        Paused,
        Finish
    }
    public ShakeController shakeController;
    public ShakeController shakeBg;
    public Slider slide;

    public PokeImg_Controller currentPokeFocus = null;
    public Button nextLevel;
    

    void Awake() {
        instance = this;
        Audio_Manager.instance.PlayMusicInGame();
    }
    
    void Start() {
        level = Data_Manager.instance.levelTemp;
        txtScore1.text = Data_Manager.instance.score.totalScore.ToString();
        Init();
        if (Data_Manager.instance.listItem.Count <=0)
        {
            Item_Manager.instance.btnIce.interactable = false;
            Item_Manager.instance.btnTime.interactable = false;
            Item_Manager.instance.btnHammer.interactable = false;
        }

        if (Audio_Manager.instance.musicSource.mute == false && Audio_Manager.instance.musicSourceInGame.mute == false)
        {

            Button_Manager.instance.playMusic.SetActive(true);
        }
        else
        {
            Button_Manager.instance.stopMusic.SetActive(true);
        }

        if (Audio_Manager.instance.soundSource.mute == false)
        {

            Button_Manager.instance.playSound.SetActive(true);
        }
        else
        {
            Button_Manager.instance.stopSound.SetActive(true);
        }
        Audio_Manager.instance.musicSource.Stop();
        if(level == 28)
        {
            nextLevel.interactable = false;
        }
        
    }
    void Init() {
        StartGame();
        CheckLogicGame();
        rainEffect.Play();
        starEffect.Stop();
        pauseEffect.Stop();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        scoreAddPanel.SetActive(false);
        Button_Manager.instance.pausePanel.SetActive(false);
        //CheckEndGame();
    }
    public void CheckLogicGame()// Ham check duong di pokemon trong Map con` hay ko
    {
        Vector2Int p1 = new Vector2Int();
        Vector2Int p2 = new Vector2Int();
        int countCouple = 0;
        if(listPoke.Count > 1)
        {
            do
            {
                for (int i = 0; i < listPoke.Count; i++)
                {
                    for (int j = i + 1; j < listPoke.Count; j++)
                    {
                        p1 = listPoke[i].point.posMatrix;
                        p2 = listPoke[j].point.posMatrix;
                        int index = listPoke[i].name.LastIndexOf(",");
                        p1.x = int.Parse(listPoke[i].name.Substring(0, index));
                        p1.y = int.Parse(listPoke[i].name.Substring(index + 1));
                        p2.x = int.Parse(listPoke[j].name.Substring(0, index));
                        p2.y = int.Parse(listPoke[j].name.Substring(index + 1));
                        if (map.checkdLine(p1, p2))
                        {

                            if (listPoke.Count > 5)
                            {
                                countCouple++;
                                if (countCouple > 1)
                                {
                                    Debug.Log("Can play continute");
                                    Debug.Log("" + p1 + "-->" + p2);
                                    return;
                                }
                            }
                            else
                            {
                                Debug.Log("Can play continute");
                                Debug.Log("" + p1 + "-->" + p2);
                                return;
                            }
                        }
                    }
                }
                RandomMap();
                Debug.Log("RandomMap !!!");
            }
            while (!map.checkdLine(p1, p2));
        }
    }
    private void RandomMap()// Random ngau nhien pokemon
    {
        listSpriteRandom = new List<Sprite>();
        for (int i = 0; i < listPoke.Count; i++)
        {
            listSpriteRandom.Add(listPoke[i].pokeImg.sprite);
        }
        map.ShuffleListSprite(listSpriteRandom);
        int index = 0;
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                if (map.Matrix[i][j] > 0 && listPoke.Contains(MatrixPoke[i][j]))
                {
                    MatrixPoke[i][j].pokeImg.sprite = listSpriteRandom[index];
                    map.Matrix[i][j] = int.Parse(listSpriteRandom[index].name);
                    index++;
                }
            }
        }
    }
    public void EvtClick()// su kien click pokemon
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
       
        foreach (PokeImg_Controller _poke in listPoke) {
            if (_poke.pokeID.Equals(name)) {
                currentPokeFocus = _poke;
            }
        }
        if (FirstPos == Vector2Int.zero)
        {
            Debug.Log("vi tri dau tien dc bam: " + currentPokeFocus.point.posMatrix);
            FirstPos = currentPokeFocus.point.posMatrix;
            MatrixPoke[FirstPos.x][FirstPos.y].pokeImg.color = new Color(0, 2, 47, 100);
            Audio_Manager.instance.PlayClickButton();
        }
        else
        {
            SecondPos = currentPokeFocus.point.posMatrix;
            Audio_Manager.instance.PlayClickButton();
            Debug.Log("vi tri thu2 dc bam: " + currentPokeFocus.point.posMatrix);
            if (!map.checkTwoPoint(FirstPos, SecondPos))
            {
                Debug.Log("False !! ");
                MatrixPoke[FirstPos.x][FirstPos.y].pokeImg.color = new Color(1, 1, 1, 1);
                Audio_Manager.instance.PlayWrongButton();
            }
            else
            {

                Execute(FirstPos, SecondPos);
                Audio_Manager.instance.PlayRightButton();
                SetModeGame(level, rows, cols);
                map.UpdateValueButton(rows, cols);
                RandomClick();
                AddScore();
                CheckLogicGame();
            }
            //if (!FirstPos.Equals(SecondPos) && map.Matrix[FirstPos.x][FirstPos.y] == map.Matrix[SecondPos.x][SecondPos.y])
            //{
            //    Execute(FirstPos, SecondPos);
            //    Audio_Manager.instance.PlayRightButton();
            //    SetModeGame(level, rows, cols);
            //    map.UpdateValueButton(rows, cols);
            //    RandomClick();
            //    AddScore();
            //    CheckLogicGame();

            //}
            //else
            //{
            //    Debug.Log("False !! ");
            //    MatrixPoke[FirstPos.x][FirstPos.y].pokeImg.color = new Color(1, 1, 1, 1);
            //    Audio_Manager.instance.PlayWrongButton();

            //}
            FirstPos = Vector2Int.zero;
            SecondPos = Vector2Int.zero;
            Debug.Log("Done");
        }
    }
    public void Execute(Vector2Int v1, Vector2Int v2)// Hai pokemon mat
    {
        
        Debug.LogError("vi tri diem 1: " + MatrixPoke[v1.x][v1.y].point.posMatrix);
        Debug.LogError("vi tri diem 2: " + MatrixPoke[v2.x][v2.y].point.posMatrix);
        MatrixPoke[v1.x][v1.y].pokeBtn.interactable = false;
        MatrixPoke[v2.x][v2.y].pokeBtn.interactable = false;
        MatrixPoke[v1.x][v1.y].pokeImg.color = new Color(0, 0, 0, 0);
        MatrixPoke[v2.x][v2.y].pokeImg.color = new Color(0, 0, 0, 0);
        map.Matrix[v1.x][v1.y] = -1;
        map.Matrix[v2.x][v2.y] = -1;       
           
        Debug.Log("Delectable");
    }
    void StartGame() {
        SetUpSize(level);
        SetUpMatrix(level);
        map.CreateMatrix(cols, rows);
        map.RandomSpriteList(cols, rows, listSpriteRandom, level);
        map.AddPoke(cols, rows, listSpriteRandom);
        SetLevelUI(level); 
        gridPoke.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridPoke.constraintCount = cols;
        timePlay = (level >= 1 && level <= 4) ? 60f :
                    (level >= 5 && level <= 12) ? 100f :
                    (level >= 13 && level <= 19) ? 100f + ((level % 10) * 5) :
                    (level == 20) ? 150f :160f + ((level % 10)*5);
        slide.minValue = 0f;
        slide.maxValue = timePlay;
        state = StateGame.Playing;
        if (timeAction != null)
        {
            StopAllCoroutines();
            timeAction = null;
        }
        else
            timeAction = StartTimePlay();
        StartCoroutine(timeAction);

        if (checkEndGameAction != null)
        {
            StopAllCoroutines();
            checkEndGameAction = null;
        }
        else
            checkEndGameAction = CheckEndGame();
        StartCoroutine(checkEndGameAction);
        //StartCoroutine(pokeRotation());
    }
    void SetUpSize(int _level) //Tao size Pika theo level
    {
        if (_level >= 1 && _level <= 4)
        {
            gridPoke.cellSize = new Vector2(105 - _level * 4, 105 - _level * 4);
        }
        else if (_level >= 5 && _level <= 11)
        {
            gridPoke.cellSize = new Vector2(78, 78);
        }
        //else if (_level >= 9 && _level <= 11)
        //{
        //    gridPoke.cellSize = new Vector2(80, 80);
        //}
        else if(_level >= 13 && _level <= 16)
        {
            gridPoke.cellSize = new Vector2(77, 77);
        }
        else /*if (_level == 12 || _level >=17 && _level <= 28)*/
        {
            gridPoke.cellSize = new Vector2(67, 67);
        }
        //else
        //{
        //    gridPoke.cellSize = new Vector2(77, 77);
        //}

    }
    void SetUpMatrix(int _level ) // Tao ma tran theo Level
    {
        rows = (_level == 1 || _level == 2 || _level == 3 || _level == 4) ? 6 :
                (_level == 5) || (_level == 6) || (_level == 7) || (_level == 8)
                || (_level == 9) || (_level == 10) || (_level == 11) || (_level == 13) 
                || (_level == 14) || (_level == 15) || (_level == 16)
                || (_level == 17) || (_level == 18) || (_level == 19) || (_level == 20)
                || (_level == 21) || (_level == 22) || (_level == 23) ? 8: 9;
        cols = (_level == 1) ? 7 :
               (_level == 2) ? 8 :
               (_level == 3) ? 9 :
               (_level == 4) ? 10 :
               (_level == 5 || _level == 6 || _level == 7 || _level == 8) 
               || (_level == 13) || (_level == 14) || (_level == 15) || (_level == 16) ? 12 : 14;
                
            
    }
    void AddScore()// Cong Diem, voi moi 1 cap hinh mat di +100d
    {
       
        Data_Manager.instance.score.totalScore += 100;
        coin += 100;
        txtScore2.text = coin.ToString();
        

    }
    int CalStarNumber() // Tinh sao cua level theo Thoi gian
    {
        int _starNumber = 0;
        float _timeStar3 = 0, _timeStar2 = 0, timeOriginal = 0;
        timeOriginal = (level >= 1 && level <= 4) ? 60f :
                    (level >= 5 && level <= 12) ? 100f :
                    (level >= 13 && level <= 19) ? 100f + ((level % 10) * 5) :
                    (level == 20) ? 150f : 160f + ((level % 10) * 5);
        _timeStar3 = timeOriginal - timeOriginal * 0.4f;

        _timeStar2 = timeOriginal - timeOriginal * 0.7f;

        _starNumber = (timePlay >= _timeStar3) ? 3 :
            (timePlay >= _timeStar2 && timePlay < _timeStar3) ? 2 :
            (timePlay < _timeStar2) ? 1 : 0;
        return _starNumber;
    }
    void SetLevelUI(int _level)
    {
        for(int i=1; i<=28; i++)
        {
            if(_level >0 &&_level < 5)
            {
                if (_level == i)
                {
                    levelPanel[i - 1].SetActive(true);
                }
            }else if(_level >=5 && _level < 13)
            {
                if (_level == i)
                {
                    levelPanel[i - 5].SetActive(true);
                }
            }else if(_level >= 13 && _level < 21)
            {
                if (_level == i)
                {
                    levelPanel[i - 13].SetActive(true);
                }
            }
            else
            {
                if (_level == i)
                {
                    levelPanel[i - 21].SetActive(true);
                }
            }
            
        }
        
    }
    IEnumerator StartTimePlay() // Thoi Gian Choi
    {    
        while (true) {
            if(timePlay > 0 && state == StateGame.Playing)
            {
                timePlay -= Time.deltaTime;
                txtTimePlay.text = string.Format("{0:00}", timePlay);
                slide.value = timePlay;
                //Debug.Log("thoi gian: " + timePlay);
            }           
            yield return null;
        }
    }
    IEnumerator CheckEndGame() // Kiem Tra Thua Hoac Win Game
    {
        bool _isFinish = false;
        while (true) {
            if (timePlay <= 0 && !_isFinish)
            {
                rainEffect.Stop();
                losePanel.SetActive(true);
                losePopup.Show();            
                _isFinish = true;
                state = StateGame.Finish;
                Audio_Manager.instance.StopMusicItemIce();
                Audio_Manager.instance.StopMusicInGame();
                Audio_Manager.instance.PlayMusicLoseGame();
            }
            else if (listPoke.Count == 0 && !_isFinish) {
                if (Item_Manager.instance.timeIce > 0)
                {
                    Item_Manager.instance.icePopup.Hide2();
                    Item_Manager.instance.timeIce = 0;                   
                    state = StateGame.Finish;
                }              
                state = StateGame.Finish;
                int starNumber = CalStarNumber();
                
                StartCoroutine(EffectAddScoreTheFirst(level));
                AddScoreWinGameTheFirst(level, starNumber);               
                Level _level = new Level();
                _level.levelNumber = level;
                _level.starNumber = starNumber;                                
                StartCoroutine(CheckStarGame(starNumber));
                CheckSaveData(_level);              
                Data_Manager.instance.SaveDataPlayer();
                Audio_Manager.instance.StopMusicInGame();
                Audio_Manager.instance.PlayMusicWinGame();
                Audio_Manager.instance.StopMusicItemIce();
                _isFinish = true;
            }
            yield return null;
        }
    }
    void CheckSaveData(Level _level)//Luu thong tin Level khi choi 
    { 
        if (Data_Manager.instance.listLevel.Count > 0)
        {
            for (int i = 0; i < Data_Manager.instance.listLevel.Count; i++)
            {
                if (Data_Manager.instance.listLevel[i].levelNumber == _level.levelNumber
                    && Data_Manager.instance.listLevel[i].starNumber < _level.starNumber)
                {
                    Debug.Log("Remove! " + Data_Manager.instance.listLevel[i].levelNumber);
                    Data_Manager.instance.listLevel.RemoveAt(i);
                    Data_Manager.instance.SaveData<Level>(Data_Manager.instance.listLevel, _level, "ListLevel");
                    break;
                }
                else
                {
                    if (Data_Manager.instance.listLevel.Count == _level.levelNumber-1)
                    {
                        Debug.LogError("Level  save");
                        Data_Manager.instance.SaveData<Level>(Data_Manager.instance.listLevel, _level, "ListLevel");
                        break;
                    }                    
                }
            }
        }
        else
        {
            Debug.LogError("New List");
            Data_Manager.instance.SaveData<Level>(Data_Manager.instance.listLevel, _level, "ListLevel");
        }
        winPanel.SetActive(true);       
        winPopup.Show();
        starEffect.Play();       
        rainEffect.Stop();
    }
    IEnumerator CheckStarGame(int _starNumber)// kiem tra sao de lam hieu ung
    {
        int _indexStar = 0;
        while (_starNumber >0) {
            yield return Yielders.Get(0.2f);
            Audio_Manager.instance.StarSound();
            StarEffect(_indexStar);
            _indexStar++;
            _starNumber--;
        }
    }
    void StarEffect(int _indexStar)// Hieu ung sao 
    { 
        starsYellow[_indexStar].SetActive(true);
        starsYellow[_indexStar].transform.localScale = new Vector3(5, 5, 5);       
        LeanTween.scale(starsYellow[_indexStar], new Vector3(1, 1, 1), 0.5f);
    }
    void AddScoreWinGameTheFirst(int _level,int _startNumber)// ham luu diem lan dau
    {
        if (Data_Manager.instance.listLevel.Count > 0)
        {
            if ((_level - Data_Manager.instance.listLevel.Count ) == 1)
            {
                if(_level >= 2 && _level <= 4)
                {
                    if (_startNumber == 3)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + 3 * 100 * _level;
                    }
                    else if (_startNumber == 2)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + 2 * 100 * _level;
                    }
                    else
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + 100 * _level;
                    }
                }else if(_level >=5 && _level <= 12)
                {
                    if (_startNumber == 3)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (3 * 100 * 2 * (_level - 4));
                    }
                    else if (_startNumber == 2)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (2 * 100 * 2 * (_level - 4));
                    }
                    else
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + ( 100 * 2 * (_level - 4));
                    }
                }else if(_level >= 13 && _level <= 20)
                {
                    if (_startNumber == 3)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (3 * 100 * 3 * (_level - 12));
                    }
                    else if (_startNumber == 2)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (2 * 100 * 3 * (_level - 12));
                    }
                    else
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (100 * 3 * (_level - 12));
                    }
                }else if (_level >= 21 && _level <= 28)
                {
                    if (_startNumber == 3)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (3 * 100 * 4 * (_level - 20));
                    }
                    else if (_startNumber == 2)
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (2 * 100 * 4 * (_level - 20));
                    }
                    else
                    {
                        Data_Manager.instance.score.totalScore = Data_Manager.instance.score.totalScore + (100 * 4 * (_level - 20));
                    }
                }
                
            }
            txtScore1.text = Data_Manager.instance.score.totalScore.ToString();
            Data_Manager.instance.SaveDataPlayer();
        }
        else
        {           
            if(_level == 1)
            {
                if (_startNumber == 3)
                {
                    Data_Manager.instance.score.totalScore += 3 * 100;
                }
                else if (_startNumber == 2)
                {
                    Data_Manager.instance.score.totalScore += 2 * 100;

                }
                else
                {
                    Data_Manager.instance.score.totalScore += 100;

                }
            }
            txtScore1.text = Data_Manager.instance.score.totalScore.ToString();
            Data_Manager.instance.SaveDataPlayer();
            
        }  
    }
    int AddScoreTheFirst(int _level,int _starNumber)// ham tinh diem lan dau
    {
        int score = 0;
        if (Data_Manager.instance.listLevel.Count > 0)
        {
           
            if ((_level - Data_Manager.instance.listLevel.Count) == 1)
            {
                if (_level >= 2 && _level <= 4)
                {
                    score = (_starNumber == 3) ? 3 * 100 * level :
                         (_starNumber == 2) ? 2 * 100 * level : 100 * level;
                }else if(_level >=5 && _level <= 12)
                {
                    score = (_starNumber == 3) ? 3 * 100 * 2 * (_level -4 ) :
                         (_starNumber == 2) ? 2 * 100 * 2 * (_level - 4) : 100 * 2 * (_level - 4);
                }else if(_level >= 13 && _level <= 20)
                {
                    score = (_starNumber == 3) ? 3 * 100 * 3 * (_level - 12) :
                        (_starNumber == 2) ? 2 * 100 * 3 * (_level - 12) : 100 * 3 * (_level - 12);
                }
                else if (_level >= 21 && _level <= 28)
                {
                    score = (_starNumber == 3) ? 3 * 100 * 4 * (_level - 20) :
                        (_starNumber == 2) ? 2 * 100 * 4 * (_level - 20) : 100 * 4 * (_level - 20);
                }
            }            
        }
        else
        {
            if(_level == 1)
            {
                score = (_starNumber == 3) ? 3 * 100 :
                         (_starNumber == 2) ? 2 * 100 : 100;
            }
                     
            
        }
        return score;
    }
    IEnumerator EffectAddScoreTheFirst(int _level)// ham hieu ung luu diem lan dau
    {
        if (Data_Manager.instance.listLevel.Count > 0)
        {
            if ((_level - Data_Manager.instance.listLevel.Count) == 1)
            {
                int _starNumber = CalStarNumber();
                int scoreAdd = AddScoreTheFirst(level, _starNumber);
                scoreAddText.text = scoreAdd.ToString();
                scoreAddPanel.SetActive(true);
                scoreAddPanel.transform.localScale = new Vector3(5, 5, 5);
                //yield return Yielders.Get(0.5f);
                LeanTween.scale(scoreAddPanel, new Vector3(1, 1, 1), 0.5f);
                yield return null;

            }
        }
        else
        {
            int _starNumber = CalStarNumber();
            int scoreAdd = AddScoreTheFirst(level, _starNumber);
            scoreAddText.text = scoreAdd.ToString();
            scoreAddPanel.SetActive(true);
            scoreAddPanel.transform.localScale = new Vector3(5, 5, 5);
            //yield return Yielders.Get(0.5f);
            LeanTween.scale(scoreAddPanel, new Vector3(1, 1, 1), 0.5f);
            yield return null;

        }
        
    }
    public void Shake()// rung man hinh
    {
        shakeController.SetUpShakeLocalPoint(0.05f, 2f);/*(thgian rung càng lớn là càng giảm, độ rung)*/
        Audio_Manager.instance.PlayEarthquakeSound();
    }
    public void ShakeBg()// rung man hinh
    {
        shakeBg.SetUpShakeLocalPoint(0.05f, 0.5f);/*(thgian rung càng lớn là càng giảm, độ rung)*/
        
    }
    public void SetModeGame(int _level , int _row, int _col)
    {
        if(_level == 13 || _level == 14  )
        {
           map.MoveLeft(_row, _col);
        }
        else if ( _level == 15 || _level == 16)
        {
            map.MoveRight(_row, _col);
        }
        else if (_level == 17 || _level == 18)
        {
            map.MoveBottom(_row, _col);
        }
        else if (_level == 19 || _level == 20)
        {
            map.MoveTop(_row, _col);
        }
        else if (_level == 21 || _level == 22)
        {
            map.MoveLeft(_row, _col);
            map.MoveTop(_row, _col);
        }
        else if (_level == 23 || _level == 24)
        {
            map.MoveLeft(_row, _col);
            map.MoveBottom(_row, _col);
        }
        else if (_level == 25 || _level == 26)
        {
            map.MoveRight(_row, _col);
            map.MoveTop(_row, _col);
        }
        else if (_level == 27 || _level == 28)
        {
            map.MoveRight(_row, _col);
            map.MoveBottom(_row, _col);
        }

    }
    void RandomClick()
    {
        if(level >= 9 && level <= 28)
        {
            RandomMap();
        }
    }
    public void CreateHammer() {
        hammerWall.SetActive(true);
        state = StateGame.Paused;
        Hammer_ItemController hammer = Instantiate(hammerPrefabs,gameObject.transform.parent);
        hammer.Init();
    }

}
