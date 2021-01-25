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
                gridPosition.Add(new Vector3(x, y, 0f));
            }
        }

    }


    void BoradSetup()
    {

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
