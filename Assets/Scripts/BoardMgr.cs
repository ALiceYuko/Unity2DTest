using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class BoardMgr : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minNum;
        public int maxNum;

        public Count(int min, int max)
        {
            minNum = min;
            maxNum = max;
        }
    }

    public int cols = 8;
    public int rows = 8;
    public Count innerWallCount = new Count(4, 8);
    public Count foodCount = new Count(3, 8);
    public GameObject exitObj;
    public GameObject[] floorObjs;
    public GameObject[] foodObjs;
    public GameObject[] innerWallObjs;
    public GameObject[] enemyObjs;
    public GameObject[] outWallObjs;

    private Transform boardHolder;

    private List<Vector3> gridPosition = new List<Vector3>();

    void InitObjList()
    {
        gridPosition.Clear();

        //除去外墙其他元素的位置存储
        for(int x = 1; x < cols - 1; x++)
        {
            for(int y = 1; y < rows - 1; y++)
            {
                gridPosition.Add(new Vector3(x, y, 0));
            }
        }

    }


    void BoardSetup()
    {
        boardHolder = new GameObject("board").transform;

        //地板和外墙
        for(int x = -1; x < cols + 1; x++)
        {
            for(int y = -1; y < rows + 1; y++)
            {
                //普通选个随机地板图样引用
                GameObject boardPrefab = floorObjs[Random.Range(0, floorObjs.Length)];
                if (x == -1 || x == cols || y == -1 || y == rows)
                {
                    boardPrefab = outWallObjs[Random.Range(0, outWallObjs.Length)];
                }

                //创建实际对象，并且Trans设置父亲，为了跟着board整体动
                GameObject insToAdd = Instantiate(boardPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                insToAdd.transform.SetParent(boardHolder);

            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector3 retPos = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex); //防止重复
        return retPos;
    }

    void LayoutObjsRandom(GameObject [] srcObjs, int min, int max)
    {
        if(srcObjs.Length == 0)
        {
            return;
        }

        int needCount = Random.Range(min, max+1);
        for(int i = 0; i < needCount; i++)
        {
            Vector3 pos = RandomPosition();
            GameObject randomPrefab = srcObjs[Random.Range(0, srcObjs.Length)];
            GameObject insToAdd = Instantiate(randomPrefab, pos, Quaternion.identity) as GameObject;
        }
    }

    public void SetupScene(int level)
    {
        InitObjList();
        BoardSetup();
        LayoutObjsRandom(innerWallObjs, innerWallCount.minNum, innerWallCount.maxNum);
        LayoutObjsRandom(foodObjs, foodCount.minNum, foodCount.maxNum);

        //根据等级调整敌人数目
        //int enemyCount = (int)Math.Log(level, 2);
        //LayoutObjsRandom(enemyObjs, enemyCount, enemyCount);
        Instantiate(exitObj, new Vector3(cols - 1, rows - 1), Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
