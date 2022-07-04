using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Point
{
    public Vector2Int posMatrix;
    public Vector2 position;
    public int value;

    public Point(int _value) {
        value = _value;
        posMatrix = new Vector2Int();
        position = new Vector2();
    }
}
