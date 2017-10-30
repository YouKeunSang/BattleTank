using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TANK_STATE
{
    IDLE,
    RUN,
    SKILL_RUN,
    DIE,
}
/*
 * 목적: contoller의 경우 ui와 연계하여 처리하고, network를 통하여 controller를 대체하여 pvp를 구현하기 위해 분리한다.
 *       탄의 경우 스킬과 구현이 다양할수 있어 분리한다.
 *       여기서는 네트워크 또는 유저의 입력에 대해서 탱크의 이동과 포탑의 움직임을 책임진다.
 * TODO: 입력된 좌표로 이동하면서 장애물을 건너뛰는 path finding에 대한 서비스도 나중에 여기서 구현한다
 */
public class Tank : MonoBehaviour {
    public float speed = 3.0f;  //이동속도
    public float skillSpeed = 5.0f; //스킬이동속도
    public GameObject turret;   //포탑
    public BaseBullet normalBullet;
    public BaseBullet skillBullet;

    Tank enemy; //현재 지정된 적

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

}
