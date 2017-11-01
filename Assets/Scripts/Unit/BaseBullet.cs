using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour {
    public float speed = 15f;
    public float damage = 100f;
    public float radious = 10;  //폭팔범위
    Vector2 _target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 포탄을 특정좌표로 발사한다
    /// </summary>
    /// <param name="dest">여기서 좌표는 screen좌표가 아니고 계산된 world의 좌표임</param>
    public void Fire(Vector2 dest)
    {

    }
}
