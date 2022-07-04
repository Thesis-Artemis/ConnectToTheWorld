using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class Level
{
    public int levelNumber;
    public int starNumber;

    public Level() {
        levelNumber = 0;
        starNumber = 0;
    } 
    
}
