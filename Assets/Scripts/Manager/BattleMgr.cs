using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : MonoBehaviour{
    #region 싱글톤
    private static BattleMgr _instance;
    public static BattleMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (BattleMgr)FindObjectOfType(typeof(BattleMgr));
                if (null == _instance)
                {
                    GameObject _obj = new GameObject("_BattleMgr");
                    _instance = _obj.AddComponent<BattleMgr>();
                }
                DontDestroyOnLoad(_instance);

                //화면 꺼짐방지
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }
            return _instance;
        }
    }
    #endregion
    Camera _mainCam = null;
    Tank _player = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Camera GetCamera()
    {
        if(null == _mainCam)
        {
            _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        return _mainCam;
    }
    public void SetPlayer(Tank player)
    {
        _player = player;
    }
    public Tank GetPlayer()
    {
        if(null == _player)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Tank>();
        }
        return _player;
    }
}
