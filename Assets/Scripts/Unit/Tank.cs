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
    public int HP = 300;
    public float speed = 3.0f;  //이동속도
    public float skillSpeed = 5.0f; //스킬이동속도
    public GameObject turret;   //포탑
    public BaseBullet normalBullet;
    public BaseBullet skillBullet;

    Tank enemy; //현재 지정된 적
    //이동에 관련된 변수들
    Vector3 _dir;   //지정되 좌표로의 방향
    float _timeToDest = 0f; //좌표까지 가는데 걸리는 시간
    bool _isMove = false;
    float _movingSpeed = 0;

	// Use this for initialization
	void Start () {
		if(tag.Equals("Player"))
        {
            BattleMgr.instance.SetPlayer(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(_isMove)
        {
            gameObject.transform.position += _dir * Time.deltaTime * _movingSpeed;
            _timeToDest -= Time.deltaTime;
            if(0 >= _timeToDest)
            {
                _isMove = false;
            }
        }
	}

    public void SetColor(Color color)
    {

    }
    public void Attack(Vector2 dest,bool isSkill)
    {

    }
    /// <summary>
    /// 화면에서 지정된 좌료로 이동한다.
    /// 여기서는 그 방향으로 벡터 방향과 거리,시간이 계산된다
    /// </summary>
    /// <param name="dest">화면상 좌표, 실제 거리로 변경하여 이동한다.</param>
    /// <param name="isSkill"></param>
    public void Move(Vector2 dest,bool isSkill)
    {
        Camera _c = BattleMgr.instance.GetCamera();
        Vector3 _dest = _c.ScreenToWorldPoint(dest);
        _dest.y = 0;

        _dir = _dest - gameObject.transform.position;
        if(!isSkill)
        {
            _timeToDest = _dir.magnitude / speed;
            _movingSpeed = speed;
        }
        else
        {
            _timeToDest = _dir.magnitude / skillSpeed;
            _movingSpeed = skillSpeed;
        }
        _dir.Normalize();
        //몸체가 회전할 각도를 계산한다.
        gameObject.transform.eulerAngles = new Vector3(0, GetAngle(_dir), 0);
        _isMove = true;
    }

    public static float GetAngle(Vector3 direction)
    {
        //1. 각도계산
        float _deg = Vector3.Angle(Vector3.forward, direction);
        _deg = direction.x < 0 ? 360 - _deg : _deg;
        return _deg;
    }
}
