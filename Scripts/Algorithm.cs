using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    //public static Map instance; 
    public PokeImg_Controller pokemonPrefab;
    public PokeImg_Controller wallPrefab;
    public Sprite[] pokemonSprites1;
    public Sprite[] pokemonSprites2;
    public Sprite[] pokemonSprites3;
    public int[][] Matrix;
    public Transform container;
    public LineRenderer lineRenderer;
    public Pikachu_IngameManager pika_manager;
    public int level;
    void Awake()
    {
        //instance = this;       
        pokemonSprites1 = Resources.LoadAll<Sprite>("Images1");
        pokemonSprites2 = Resources.LoadAll<Sprite>("Images2");
        pokemonSprites3 = Resources.LoadAll<Sprite>("Images3");
        level = Data_Manager.instance.levelTemp;
    }
    public void AddPoke(int _col, int _row, List<Sprite> _listSprite)
    {
        int index = 0;
        //Tạo ma trận có row hàng
        Matrix = new int[_row][];
        //Tạo một ma trận mà mỗi phần tử là 1 hình 
        Pikachu_IngameManager.instance.MatrixPoke = new PokeImg_Controller[_row][];
        for (int i = 0; i < _row; i++)
        {
            Matrix[i] = new int[_col];
            Pikachu_IngameManager.instance.MatrixPoke[i] = new PokeImg_Controller[_col];
            for (int j = 0; j < _col; j++)
            {
                if (level == 5)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                        //i=1
                       || (i == 1 && j == 3) || (i == 1 && j == 4) || (i == 1 && j == 7)
                       || (i == 1 && j == 8)
                       //i=2
                       || (i == 2 && j == 3) || (i == 2 && j == 4) || (i == 2 && j == 7)
                       || (i == 2 && j == 8)
                       //i=3
                       || (i == 3 && j == 1) || (i == 3 && j == 2) || (i == 3 && j == 5)
                       || (i == 3 && j == 6) || (i == 3 && j == 9) || (i == 3 && j == 10)
                       //i=4
                       || (i == 4 && j == 1) || (i == 4 && j == 2) || (i == 4 && j == 5)
                       || (i == 4 && j == 6) || (i == 4 && j == 9) || (i == 4 && j == 10)
                       //i=5
                       || (i == 5 && j == 3) || (i == 5 && j == 4) || (i == 5 && j == 7)
                       || (i == 5 && j == 8)
                       //i=6
                       || (i == 6 && j == 3) || (i == 6 && j == 4) || (i == 6 && j == 7)
                       || (i == 6 && j == 8))

                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        //tao 1 doi tuong PokeImg có gameobj pokemon,vi tri va truc quay)
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;

                    }

                }
                else if (level == 6)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                        //i=1
                       || (i == 1 && j == 4) || (i == 1 && j == 5) || (i == 1 && j == 6)
                       || (i == 1 && j == 7) 
                       // i=2
                       || (i == 2 && j == 4) || (i == 2 && j == 7)
                       //i=3                                                                   
                       || (i == 3 && j == 1) || (i == 3 && j == 2) || (i == 3 && j == 3)
                       || (i == 3 && j == 8) || (i == 3 && j == 9) || (i == 3 && j == 10)
                       //i=4
                       || (i == 4 && j == 1) || (i == 4 && j == 2) || (i == 4 && j == 3)
                       || (i == 4 && j == 8) || (i == 4 && j == 9) || (i == 4 && j == 10)
                       //i=5
                       || (i == 5 && j == 4) || (i == 5 && j == 7)
                       //i=6
                       || (i == 6 && j == 4) || (i == 6 && j == 5) || (i == 6 && j == 6)
                       || (i == 6 && j == 7)) // lv6 4 hcn

                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;
                    }

                }else if (level == 7)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                    || (j == 2) || (j == 7)
                    || (i == 1 && j == 4) || (i == 1 && j == 5) || (i == 1 && j == 9)
                    || (i == 2 && j == 4) || (i == 2 && j == 5) || (i == 2 && j == 9)
                    || (i == 3 && j == 4) || (i == 3 && j == 5)
                    || (i == 4 && j == 4) || (i == 4 && j == 5)
                    || (i == 5 && j == 9)
                    || (i == 6 && j == 9))// lv7 IUH                   
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;

                    }

                } else if (level == 8)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                        || (i == 2 && j == 2) || (i == 2 && j == 3) || (i == 2 && j == 4)
                        || (i == 2 && j == 5) || (i == 2 && j == 6) || (i == 2 && j == 7)
                        || (i == 2 && j == 8) || (i == 2 && j == 9) // i=2
                                                                    //i=3
                        || (i == 3 && j == 2) || (i == 3 && j == 9)
                        //i=4
                        || (i == 4 && j == 2) || (i == 4 && j == 9)
                        //i=5
                        || (i == 5 && j == 2) || (i == 5 && j == 3) || (i == 5 && j == 4)
                        || (i == 5 && j == 5) || (i == 5 && j == 6) || (i == 5 && j == 7)
                        || (i == 5 && j == 8) || (i == 5 && j == 9))// lv8 o vuong

                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;
                    }
                }
                else if (level == 9)
                {
                     if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1 || j == 2 || j == 11
                        //i=1
                        || (i == 1 && j == 1) || (i == 1 && j == 2) || (i == 1 && j == 11)
                        || (i == 1 && j == 12)

                        //i=2
                        || (i == 2 && j == 2) || (i == 2 && j == 4) || (i == 2 && j == 5) 
                        || (i == 2 && j == 6) || (i == 2 && j == 7) || (i == 2 && j == 8)
                        || (i == 2 && j == 9) || (i == 2 && j == 11)
                        //i=3
                        || (i == 3 && j == 2) || (i == 3 && j == 11)
                        //i=4
                        || (i == 4 && j == 2) || (i == 4 && j == 11)
                        //i=5
                        || (i == 5 && j == 2) || (i == 5 && j == 4)|| (i == 5 && j == 5) 
                        || (i == 5 && j == 6) || (i == 5 && j == 7) || (i == 5 && j == 8) 
                        || (i == 5 && j == 9) || (i == 5 && j == 11)
                         //i=6
                         || (i == 6 && j == 1) || (i == 6 && j == 2) || (i == 6 && j == 11)
                         || (i == 6 && j == 12))// lv9 ma tran  hinh robot

                    {
                           Vector2 temPosition = new Vector2(i, j);
                           PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                           temp.point.posMatrix = new Vector2Int(i, j);
                           temp.transform.SetParent(container, false);
                           temp.name = "Wall" + "" + i + "," + j; ;
                           Matrix[i][j] = 0;
                           Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                     }
                     else
                     {
                           Vector2 temPosition = new Vector2(i, j);                            
                           PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                           pokeObj.transform.SetParent(container, false);
                           pokeObj.name = "" + i + "," + j;
                           pokeObj.Init(_listSprite[index], pokeObj.name, index);
                           pokeObj.point.posMatrix = new Vector2Int(i, j);
                           Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                           Matrix[i][j] = int.Parse(_listSprite[index].name);
                           Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                           index++;
                     }
                    
                }
                else if (level == 10)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1                      
                       //i=2
                       || (i == 2 && j == 2) || (i == 2 && j == 3) || (i == 2 && j == 4)
                       || (i == 2 && j == 5) || (i == 2 && j == 6) || (i == 2 && j == 7)
                       || (i == 2 && j == 8) || (i == 2 && j == 9) || (i == 2 && j == 10)
                       || (i == 2 && j == 11)
                       //i=3
                       || (i == 3 && j == 2) || (i == 3 && j == 6) || (i == 3 && j == 7)
                       || (i == 3 && j == 11)
                       //i=4
                       || (i == 4 && j == 2) || (i == 4 && j == 6) || (i == 4 && j == 7)
                       || (i == 4 && j == 11)
                       //i=5
                       || (i == 5 && j == 2) || (i == 5 && j == 3) || (i == 5 && j == 4)
                       || (i == 5 && j == 5) || (i == 5 && j == 6) || (i == 5 && j == 7)
                       || (i == 5 && j == 8) || (i == 5 && j == 9) || (i == 5 && j == 10)
                       || (i == 5 && j == 11))// lv 10  ma tran boc 2 o hcn benh trong

                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;
                    }

                }
                else if (level == 11)
                {
                        if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                       //i=2
                       || (i == 2 && j == 2)  || (i == 2 && j == 6) || (i == 2 && j == 7)                      
                       || (i == 2 && j == 11) 
                       //i=3
                       || (i == 3 && j == 2) || (i == 3 && j == 3) || (i == 3 && j == 4)
                       || (i == 3 && j == 5) || (i == 3 && j == 8) || (i == 3 && j == 9) 
                       || (i == 3 && j == 10) || (i == 3 && j == 11)
                       //i=4
                       || (i == 4 && j == 2) || (i == 4 && j == 3) || (i == 4 && j == 4)
                       || (i == 4 && j == 5) || (i == 4 && j == 8) || (i == 4 && j == 9)
                       || (i == 4 && j == 10) || (i == 4 && j == 11)
                      //i=5
                      || (i == 5 && j == 2) || (i == 5 && j == 6) || (i == 5 && j == 7)
                      || (i == 5 && j == 11) )// lv 11  ma tran boc qua cau

                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;
                    }

                }
                else if (level == 12)
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1
                   //i=2
                   || (i == 2 && j == 1) || (i == 2 && j == 2) || (i == 2 && j == 3)
                   || (i == 2 && j == 4) || (i == 2 && j == 5) || (i == 2 && j == 6)
                   || (i == 2 && j == 7) || (i == 2 && j == 8) || (i == 2 && j == 9)
                   || (i == 2 && j == 10) || (i == 2 && j == 11)
                   //i=3
                   || (i == 3 && j == 1) || (i == 3 && j == 11)
                   //i=4
                   || (i == 4 && j == 1) || (i == 4 && j == 3) || (i == 4 && j == 4)
                   || (i == 4 && j == 5) || (i == 4 && j == 6) || (i == 4 && j == 7)
                   || (i == 4 && j == 8) || (i == 4 && j == 9) || (i == 4 && j == 11)
                   //i=5
                   || (i == 5 && j == 1) || (i == 5 && j == 3) ||  (i == 5 && j == 11)
                    //i=6
                    || (i == 6 && j == 1) || (i == 6 && j == 3) || (i == 6 && j == 4)
                    || (i == 6 && j == 5) || (i == 6 && j == 6) || (i == 6 && j == 7)
                    || (i == 6 && j == 8) || (i == 6 && j == 9) || (i == 6 && j == 10)
                    || (i == 6 && j == 11)
                    //i=7
                    || (i == 7 && j == 1))// lv 11  ma tran boc qua cau
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        temp.point.posMatrix = new Vector2Int(i, j);
                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;

                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;
                    }

                }else
                {
                    if (i == 0 || i == _row - 1 || j == 0 || j == _col - 1)
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        //tạo  một biến pokeimg có chưa vị trí
                        PokeImg_Controller temp = Instantiate(wallPrefab, temPosition, Quaternion.identity);
                        //gán vị trí trong ma trận
                        temp.point.posMatrix = new Vector2Int(i, j);

                        temp.transform.SetParent(container, false);
                        temp.name = "Wall" + "" + i + "," + j; ;
                        Matrix[i][j] = 0;
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = temp;
                    }
                    else
                    {
                        Vector2 temPosition = new Vector2(i, j);
                        Debug.LogError("vi tri Position :" + temPosition);
                        PokeImg_Controller pokeObj = Instantiate(pokemonPrefab, temPosition, Quaternion.identity);                        
                        pokeObj.transform.SetParent(container, false);
                        pokeObj.name = "" + i + "," + j;
                        pokeObj.Init(_listSprite[index], pokeObj.name, index);
                        pokeObj.point.posMatrix = new Vector2Int(i, j);
                        Pikachu_IngameManager.instance.listPoke.Add(pokeObj);
                        Matrix[i][j] = int.Parse(_listSprite[index].name);
                        Pikachu_IngameManager.instance.MatrixPoke[i][j] = pokeObj;
                        index++;

                    }
                }
                

            }
        }
    }

    public void CreateMatrix(int _row, int _col)
    {
        Matrix = new int[_row][];
        for (int i = 0; i < _row; i++)
        {
            Matrix[i] = new int[_col];
        }
    }
    public void RandomSpriteList(int _col, int _row, List<Sprite> _listSpites, int _level)
        // lay danh sach hình pokemon ngau nhien
    {
        // 
        int totalSprite4 = 0;
        int totalSprite2 = 0;
        int value = 0;
        int count = 0;

        

        if ((_row - 2) * (_col - 2) > 36)
        {           
            if (_level >=5 && _level <= 12)           
            {
               if( _level == 5) // map ma tran o vuong
               {
                    totalSprite4 = 4;
                    totalSprite2 = (32 - (totalSprite4 * 4)) / 2;
               }
               else if (_level == 6) // map 4 o hcn
               {
                    totalSprite4 = 4;
                    totalSprite2 = (36 - (totalSprite4 * 4)) / 2;
               }
               else if (_level == 7) // map iuh
               {                   
                    totalSprite4 = 3;
                    totalSprite2 = (36 - (totalSprite4 * 4)) / 2;
               }
               else if(_level == 8) // map hcn bao quanh
               {
                    totalSprite4 = 3;
                    totalSprite2 = (40 - (totalSprite4 * 4)) / 2;
               }
               else if (_level == 9) // map hinh robot
               {
                    totalSprite4 = 3;
                    totalSprite2 = (44 - (totalSprite4 * 4)) / 2;
               }
               else if (_level == 10) // map hinh robot
               {
                    totalSprite4 = 2;
                    totalSprite2 = (44 - (totalSprite4 * 4)) / 2;
               }
               else if (_level == 11) // map hinh robot
               {
                    totalSprite4 = 1;
                    totalSprite2 = (48 - (totalSprite4 * 4)) / 2;
               }
               else 
               {

                    //totalSprite4 = 0;
                    totalSprite2 = 24;

                }               
            }
            else if(_level >= 13 && _level <= 20)
            {
                totalSprite4 = 20 - _level;
                totalSprite2 = ((_row - 2) * (_col - 2) - (totalSprite4 * 4)) / 2;
                
            }
            else
            {                             
                totalSprite4 = 28 - _level;
                totalSprite2 = ((_row - 2) * (_col - 2) - (totalSprite4 * 4)) / 2;
            }
        }
        else
        {
            //totalSprite4 = 5;
            totalSprite2 = (_row - 2) * (_col - 2) / 2;

        }
        int Max = totalSprite4 + totalSprite2 ;
        
        if (_level >= 1 && _level <= 12)
        {
            while (totalSprite4 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);
                if (!_listSpites.Contains(pokemonSprites1[value]))
                {
                    count = 0;
                    while (count < 4)
                    {
                        _listSpites.Add(pokemonSprites1[value]);
                        count++;
                    }
                    totalSprite4--;
                }
            }
            while (totalSprite2 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);

                if (!_listSpites.Contains(pokemonSprites1[value]))
                {
                    count = 0;
                    while (count < 2)
                    {
                        _listSpites.Add(pokemonSprites1[value]);
                        count++;
                    }
                    totalSprite2--;
                }
            }
        }else if (_level >= 13 && _level <= 20)
        {
            while (totalSprite4 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);
                if (!_listSpites.Contains(pokemonSprites2[value]))
                {
                    count = 0;
                    while (count < 4)
                    {
                        _listSpites.Add(pokemonSprites2[value]);
                        count++;
                    }
                    totalSprite4--;
                }
            }
            while (totalSprite2 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);

                if (!_listSpites.Contains(pokemonSprites2[value]))
                {
                    count = 0;
                    while (count < 2)
                    {
                        _listSpites.Add(pokemonSprites2[value]);
                        count++;
                    }
                    totalSprite2--;
                }
            }

        }
        else
        {
            while (totalSprite4 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);
                if (!   _listSpites.Contains(pokemonSprites3[value]))
                {
                    count = 0;
                    while (count < 4)
                    {
                        _listSpites.Add(pokemonSprites3[value]);
                        count++;
                    }   
                    totalSprite4--;
                }
            }
            while (totalSprite2 > 0)
            {
                value = UnityEngine.Random.Range(0, Max);

                if (!_listSpites.Contains(pokemonSprites3[value]))
                {
                    count = 0;
                    while (count < 2)
                    {
                        _listSpites.Add(pokemonSprites3[value]);
                        count++;
                    }
                    totalSprite2--;
                }

            }
        }
                
       ShuffleListSprite(_listSpites);
    }
    public void ShuffleListSprite(List<Sprite> _listsprites)// tron danh sach
    {
        Sprite temp;
        for (int i = 0; i < _listsprites.Count; i++)
        {
            temp = _listsprites[i];
            int random = UnityEngine.Random.Range(i, _listsprites.Count);
            _listsprites[i] = _listsprites[random];
            _listsprites[random] = temp;
        }
        Debug.Log("Shuffle Sprite success");
    }
    public bool checkLineX(int y1, int y2, int x)
    {
        // find point have column max and min
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min + 1; y < max; y++)
        {
            if (Matrix[x][y] > 0)
            { // if see barrier then die
                Debug.Log("dieLineX: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        // not die -> success
        return true;
    }
    public bool checkLineY(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);//3
        int max = Mathf.Max(x1, x2);//5
        for (int x = min + 1; x < max; x++)
        {
            if (Matrix[x][y] > 0)
            {
                Debug.Log("dieLineY: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        return true;
    }
    public int checkRectX(Vector2Int p1, Vector2Int p2)
    {
        Debug.Log("Check Rect X");
        // find point have y min and max
        Vector2Int pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        for (int y = pMinY.y; y <= pMaxY.y; y++)
        {
            // check three line
            if (y > pMinY.y && Matrix[pMinY.x][y] > 0)
            {
                return -1;
            }
            if ((Matrix[pMaxY.x][y] == 0 || y == pMaxY.y)  && checkLineY(pMinY.x, pMaxY.x, y)
                && checkLineX(y, pMaxY.y, pMaxY.x))
            {
                Debug.Log("Rect x");
                Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                        + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                        + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                // if three line is true return column y
                return y;
            }
        }
        // have a line in three line not true then return -1
        return -1;
    }
    public int checkRectX2(Vector2Int p1, Vector2Int p2, int type)
    {
        Debug.Log("Check Rect X2");
        Vector2Int pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        int y = pMaxY.y + type;
        if (pMinY.y == pMaxY.y)
        {
            while (Matrix[pMinY.x][y] == 0
                   && Matrix[pMaxY.x][y] == 0)
            {
                if (checkLineY(pMinY.x, pMaxY.x, y))
                {
                    Debug.Log("TH X " + type);
                    Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                            + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                            + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                    return y;
                }
                y += type;
            }
        }
        return -1;
    }
    public int checkRectY(Vector2Int p1, Vector2Int p2)
    {
        Debug.Log("Check Rect Y");
        // find point have y min
        Vector2Int pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        

        // find line and y begin
        for (int x = pMinX.x; x <= pMaxX.x; x++)
        {
            Debug.Log("x"+x);
            Debug.Log("pmaxX.x"+pMaxX.x);
            if (x > pMinX.x && Matrix[x][pMinX.y] > 0)
            {
                return -1;
            }         
            if ((Matrix[x][pMaxX.y] == 0 || x == pMaxX.x)
                    && checkLineX(pMinX.y, pMaxX.y, x)
                    && checkLineY(x, pMaxX.x, pMaxX.y))
            {
                Debug.Log("Rect y");
                Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> (" + x
                        + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                        + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                return x;
            }
        }
        return -1;
    }

    public int checkRectY2(Vector2Int p1, Vector2Int p2, int type)
    {
        Debug.Log("Check Rect Y2");
        Vector2Int pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        int x = pMaxX.x + type;
        if (pMinX.x == pMaxX.x)
        {
            while (Matrix[x][pMinX.y] == 0
                   && Matrix[x][pMaxX.y] == 0)
            {
                if (checkLineX(pMinX.y, pMaxX.y, x))
                {
                    Debug.Log("TH Y " + type);
                    Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> ("
                            + x + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                            + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                    return x;
                }
                x += type;
            }
        }
        return -1;
    }
    public int checkMoreLineX(Vector2Int p1, Vector2Int p2, int type)
    {
        Debug.Log("Check More Line X");
        // find point have y min
        Vector2Int pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        // find line and y begin
        int y = pMaxY.y + type;
        int row = pMinY.x;
        int colFinish = pMaxY.y;
        if (type == -1)
        {
            colFinish = pMinY.y;
            y = pMinY.y + type;
            row = pMaxY.x;
        }
        // check more
        if ((Matrix[row][colFinish] == 0 || pMinY.y == pMaxY.y)
                && checkLineX(pMinY.y, pMaxY.y, row))
        {
            while (Matrix[pMinY.x][y] == 0
                    && Matrix[pMaxY.x][y] == 0)
            {
                if (checkLineY(pMinY.x, pMaxY.x, y))
                {
                    Debug.Log("TH X " + type);
                    Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                            + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                            + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                    //if (checkLineX(pMinY.y, pMaxY.y, y))
                    //{
                    //    return y;
                    //}
                    return y;
                }
                //if (y == 0 || y == Pikachu_IngameManager.instance.cols)
                //    return -1;
                y += type;
            }
        }
        return -1;
    }
    public int checkMoreLineY(Vector2Int p1, Vector2Int p2, int type)
    {
        Debug.Log("Check More Line Y");
        Vector2Int pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        int x = pMaxX.x + type;
        int col = pMinX.y;
        int rowFinish = pMaxX.x;
        if (type == -1)
        {
            rowFinish = pMinX.x;
            x = pMinX.x + type;
            col = pMaxX.y;
        }
        if ((Matrix[rowFinish][col] == 0 || pMinX.x == pMaxX.x)
                && checkLineY(pMinX.x, pMaxX.x, col))
        {
            while (Matrix[x][pMinX.y] == 0 && Matrix[x][pMaxX.y] == 0)
            {                  
                if (checkLineX(pMinX.y, pMaxX.y, x))
                {
                    Debug.Log("TH Y " + type);
                    Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> ("
                            + x + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                            + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                    //if (checkLineY(pMinX.x, pMaxX.x, x))
                    //{
                    //    return x;
                    //}
                    return x;

                }
                //if (x == 0 || x == Pikachu_IngameManager.instance.rows)
                //    return -1;
                x += type;
            }
        }
        return -1;
    }
    public bool checkdLine(Vector2Int p1, Vector2Int p2)
    {
        if (!p1.Equals(p2) && Matrix[p1.x][p1.y] == Matrix[p2.x][p2.y])
        {
            // check line with x
            if (p1.x == p2.x)
            {
                if (checkLineX(p1.y, p2.y, p1.x))
                    return true;
            }
            // check line with y
            if (p1.y == p2.y)
            {
                if (checkLineY(p1.x, p2.x, p1.y))
                    return true;
            }
            int t = -1; // t is column find
            // check in rectangle with x
            if ((t = checkRectX(p1, p2)) != -1)
                return true;
            //check rectangle with x right
            if ((t = checkRectX2(p1, p2, 1)) != -1)
                return true;
            //check rectangle with x left
            if ((t = checkRectX2(p1, p2, -1)) != -1)
                return true;
            // check in rectangle with y
            if ((t = checkRectY(p1, p2)) != -1)
                return true;
            // check rectangle with y down
            if ((t = checkRectY2(p1, p2, 1)) != -1)
                return true;
            // check rectangle with y up
            if ((t = checkRectY2(p1, p2, -1)) != -1)
                return true;
            // check more right
            if ((t = checkMoreLineX(p1, p2, 1)) != -1)
                return true;
            // check more left
            if ((t = checkMoreLineX(p1, p2, -1)) != -1)
                return true;
            // check more down
            if ((t = checkMoreLineY(p1, p2, 1)) != -1)
                return true;
            // check more up
            if ((t = checkMoreLineY(p1, p2, -1)) != -1)
                return true;

        }
        return false;
    }
    public void ConnectTowPoint(Vector2Int p1, Vector2Int p2)
    {
        lineRenderer = new LineRenderer();
        lineRenderer = Pikachu_IngameManager.instance.MatrixPoke[p1.x][p1.y].GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, Pikachu_IngameManager.instance.MatrixPoke[p1.x][p1.y].transform.position);
        lineRenderer.SetPosition(1, Pikachu_IngameManager.instance.MatrixPoke[p2.x][p2.y].transform.position);

    }
    public bool checkTwoPoint(Vector2Int p1, Vector2Int p2)
    {

        if (!p1.Equals(p2) && Matrix[p1.x][p1.y] == Matrix[p2.x][p2.y])
        {

            //check line with x
            if (p1.x == p2.x)
            {
                if (checkLineX(p1.y, p2.y, p1.x))
                {

                    ConnectTowPoint(p1, p2);
                    StartCoroutine(ClearLine(p1));
                    Debug.Log("Connecting two point");
                    return true;
                }
            }
            // check line with y
            if (p1.y == p2.y)
            {
                if (checkLineY(p1.x, p2.x, p1.y))
                {
                    ConnectTowPoint(p1, p2);
                    StartCoroutine(ClearLine(p1));
                    ClearLine(p1);
                    return true;
                }
            }
            int t = -1; // t is column find
            // check in rectangle with x
            if ((t = checkRectX(p1, p2)) != -1)
            {
                ConnectTowPoint(p1, new Vector2Int(p1.x, t));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(p1.x, t), new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(new Vector2Int(p1.x, t)));
                // ClearLine(lineRenderer, new Vector2Int(p1.x, t));
                return true;
            }

            //check rectangle with x right
            if ((t = checkRectX2(p1, p2,1)) != -1)
            {
                ConnectTowPoint(p1, new Vector2Int(p1.x, t));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(p1.x, t), new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(new Vector2Int(p1.x, t)));
                // ClearLine(lineRenderer, new Vector2Int(p1.x, t));
                return true;
            }

            //check rectangle with x left
            if ((t = checkRectX2(p1, p2, -1)) != -1)
            {
                ConnectTowPoint(p1, new Vector2Int(p1.x, t));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(p1.x, t), new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(new Vector2Int(p1.x, t)));
                // ClearLine(lineRenderer, new Vector2Int(p1.x, t));
                return true;
            }
            // check in rectangle with y
            if ((t = checkRectY(p1, p2)) != -1)
            {
               
                ConnectTowPoint(p1, new Vector2Int(t, p1.y));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(t, p1.y), new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(new Vector2Int(t, p1.y)));
                return true;
            }
           
            // check rectangle with y up
            if ((t = checkRectY2(p1, p2, -1)) != -1)
            {

                ConnectTowPoint(p1, new Vector2Int(t, p1.y));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(t, p1.y), new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(new Vector2Int(t, p1.y)));
                return true;
            }

            // check rectangle with y down
            if ((t = checkRectY2(p1, p2, 1)) != -1)
            {

                ConnectTowPoint(p1, new Vector2Int(t, p1.y));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(t, p1.y), new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(new Vector2Int(t, p1.y)));
                return true;
            }

            // check more right
            if ((t = checkMoreLineX(p1, p2, 1)) != -1)
            {
                Debug.Log("check ben phai");
                ConnectTowPoint(p1, new Vector2Int(p1.x, t));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(p1.x, t), new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(new Vector2Int(p1.x, t)));
                ClearLine(new Vector2Int(p1.x, t));
                return true;
            }
            // check more left
            if ((t = checkMoreLineX(p1, p2, -1)) != -1)
            {
                Debug.Log("check ben trai");
                ConnectTowPoint(p1, new Vector2Int(p1.x, t));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(p1.x, t), new Vector2Int(p2.x, t));
                StartCoroutine(ClearLine(new Vector2Int(p1.x, t)));
                return true;
            }
            // check more up
            if ((t = checkMoreLineY(p1, p2, -1)) != -1)
            {
                ConnectTowPoint(p1, new Vector2Int(t, p1.y));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(t, p1.y), new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(new Vector2Int(t, p1.y)));
                return true;
            }
            // check more down
            if ((t = checkMoreLineY(p1, p2, 1)) != -1)
            {
                ConnectTowPoint(p1, new Vector2Int(t, p1.y));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector2Int(t, p1.y), new Vector2Int(t, p2.y));
                StartCoroutine(ClearLine(new Vector2Int(t, p1.y)));
                return true;
            }
            
        }
        //Debug.Log("Gia tri cua icon:" + Matrix[0][0] + Matrix[0][1] + "Hinh:"+Matrix[p1.x][p1.y]+ Matrix[p2.x][p2.y]);
        return false;
    }
    public IEnumerator ClearLine(Vector2Int p)
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer = Pikachu_IngameManager.instance.MatrixPoke[p.x][p.y].GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        Debug.Log("Clear Line");
    }
    public void MoveRight(int _row, int _col)
    {        
        for (int i = 1; i < _row - 1; i++)
        {
            for (int j = _col - 2; j > 1; j--)
            {
                if (Matrix[i][j] < 0 && j > 1)
                {
                    if (Matrix[i][j - 1] < 0)
                    {
                        if (Matrix[i][j - 2] > 0 && j - 2 > 0)
                        {
                            SwapPosY(i, j - 1, -1);
                            
                            SwapPosY(i, j, -1);
                            
                        }
                    }
                    else if (Matrix[i][j - 1] > 0)
                    {
                        SwapPosY(i, j, -1);
                        
                    }
                }
            }
        }
    }

    public void MoveLeft(int _row, int _col)
    {
        for (int i = 1; i < _row - 1; i++)
        {
            for (int j = 1; j < _col - 1; j++)
            {
                if (Matrix[i][j] < 0 && j < _col - 1)
                {
                    if (Matrix[i][j + 1] < 0)
                    {
                        if (Matrix[i][j + 2] > 0 && j + 2 < _col - 1)
                        {
                            SwapPosY(i, j + 1, 1);
                            SwapPosY(i, j, 1);
                        }
                    }
                    else if (Matrix[i][j + 1] > 0)
                    {
                        SwapPosY(i, j, 1);
                    }
                }
            }
        }
    }
    public void MoveBottom(int _row, int _col)
    {
        for (int i = _row -2; i > 1; i--)
        {
            for (int j = 1; j < _col - 1; j++)
            {
                if (Matrix[i][j] < 0 && i > 1)
                {
                    if (Matrix[i - 1][j] < 0)
                    {
                        if (Matrix[i - 2][j] > 0 && i - 2 > 0)
                        {
                            SwapPosX(i - 1, j, -1);
                            SwapPosX(i, j, -1);
                        }
                    }
                    else if (Matrix[i - 1][j] > 0)
                    {
                        SwapPosX(i, j, -1);
                    }
                }
            }
        }
    }
    public void MoveTop(int _row, int _col)
    {
        for (int i = 1; i < _row -1; i++)
        {
            for (int j = 1; j < _col - 1; j++)
            {
                if (Matrix[i][j] < 0 && i < _row - 1)
                {
                    if (Matrix[i + 1][j] < 0)
                    {
                        if (Matrix[i + 2][j] > 0 && i + 2 < _row -1)
                        {
                            SwapPosX(i + 1, j, 1);
                            SwapPosX(i, j, 1);
                        }
                    }
                    else if (Matrix[i + 1][j] > 0)
                    {
                        SwapPosX(i, j, 1);
                    }
                }
            }
        }
    }
  
    public void UpdateValueButton(int _row, int _col)
    {
        for (int i = 1; i < _row - 1; i++)
        {
            for (int j = 1; j < _col - 1; j++)
            {
                if (Matrix[i][j] < 0)
                {
                    Debug.LogError("gia tri luc dau tai" + Pikachu_IngameManager.instance.MatrixPoke[i][j] + "="+ Matrix[i][j]);
                    Matrix[i][j] = 0;
                    Debug.LogError("gia tri luc sau tai" + Pikachu_IngameManager.instance.MatrixPoke[i][j] + "=" + Matrix[i][j]);
                    Pikachu_IngameManager.instance.listPoke.Remove(Pikachu_IngameManager.instance.MatrixPoke[i][j]);
                    Debug.Log("Update value success !");
                }
            }
        }
    }

    public void SwapPosY(int i, int j, int type)
    {
        Matrix[i][j] = Matrix[i][j + type];
        Matrix[i][j + type] = -1;

        //pokeObj.point.posMatrix = new Vector2Int(i, j);
        Sprite spriteReplace = Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.sprite;
        
        Vector2 posMatrixReplace = Pikachu_IngameManager.instance.MatrixPoke[i][j].point.position;
        Pikachu_IngameManager.instance.MatrixPoke[i][j].point.position = Pikachu_IngameManager.instance.MatrixPoke[i][j + type].point.position;
        Pikachu_IngameManager.instance.MatrixPoke[i][j + type].point.position = posMatrixReplace;


        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.sprite = Pikachu_IngameManager.instance.MatrixPoke[i][j + type].pokeImg.sprite;
        Pikachu_IngameManager.instance.MatrixPoke[i][j + type].pokeImg.sprite = spriteReplace;

        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeBtn.interactable = true;
        Pikachu_IngameManager.instance.MatrixPoke[i][j + type].pokeBtn.interactable = false;

        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.color = new Color(1, 1, 1, 1);
        Pikachu_IngameManager.instance.MatrixPoke[i][j + type].pokeImg.color = new Color(0, 0, 0, 0);
    }

    
    public void SwapPosX(int i, int j, int type)
    {
        Matrix[i][j] = Matrix[i + type][j];
        Matrix[i + type][j] = -1;

        Vector2 posMatrixReplace = Pikachu_IngameManager.instance.MatrixPoke[i][j].point.position;
        Sprite spriteReplace = Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.sprite;

        Pikachu_IngameManager.instance.MatrixPoke[i][j].point.position = Pikachu_IngameManager.instance.MatrixPoke[i + type][j].point.position;
        Pikachu_IngameManager.instance.MatrixPoke[i + type][j].point.position = posMatrixReplace;


        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.sprite = Pikachu_IngameManager.instance.MatrixPoke[i + type][j].pokeImg.sprite;
        Pikachu_IngameManager.instance.MatrixPoke[i + type][j].pokeImg.sprite = spriteReplace;

        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeBtn.interactable = true;
        Pikachu_IngameManager.instance.MatrixPoke[i + type][j].pokeBtn.interactable = false;

        Pikachu_IngameManager.instance.MatrixPoke[i][j].pokeImg.color = new Color(1, 1, 1, 1);
        Pikachu_IngameManager.instance.MatrixPoke[i + type][j].pokeImg.color = new Color(0, 0, 0, 0);
    }
}
