using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public class Score
{
    public long totalScore;
  
    public Score()
    {
        totalScore = 0;  
    }
}
