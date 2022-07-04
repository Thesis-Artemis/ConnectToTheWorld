using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Data_Manager : MonoBehaviour
{
    public static Data_Manager instance;
    public int levelTemp;
    public Score score;
    public List<Level> listLevel;
    public List<Item> listItem;
    void Awake() {
        GetInstance();
    }
    void Start()
    {
        Init();
    }
    void Init() {
        LoadDataPlayer();
        listLevel = LoadData<Level>("ListLevel");
        listItem = LoadData<Item>("ListItem");
    }
    void GetInstance() {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadDataPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/Score"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Score", FileMode.Open);
            score = (Score)bf.Deserialize(file);
            file.Close();
            //Debug.Log("Load information player is successfull !");

        }
        else
        {
            score = new Score();
            score.totalScore =1000000;
            SaveDataPlayer();
            
        }
    }
    public void SaveDataPlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Score");
        bf.Serialize(file, score);
        file.Close();
        
    }
    public List<T> LoadData<T>(string fileName)
    {
        List<T> _list = new List<T>();
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            _list = (List<T>)bf.Deserialize(file);
            file.Close();
            Debug.Log("Load information List is successfull !");

        }
        else
        {
            _list = new List<T>();
            Debug.Log("Not find list!");
        }
        return _list;
    }
    public void SaveData<T>(List<T> _list,T name, string fileName)
    {
        _list.Add(name);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);
        bf.Serialize(file, _list);
        file.Close();
    }
}
