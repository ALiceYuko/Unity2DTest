using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameMgr = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameMgr.g_GameMgr ==  null)
        {
            Instantiate(gameMgr);
        }
    }
}
