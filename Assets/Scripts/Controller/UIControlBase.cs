using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * 터치에 대한 처리를 한다
 * 여기서는 간단 터치,스킬터치에 대한 판단만 공통으로 하고 움직임인지 공격인지는 이를 상속받은 곳에서 처리한다.
 * 스킬이 되는 경우
 * 1. 200ms안에 2번의 더블터치가 발생한다.
 * 2. force touch가 발생한다.
 * 3. force touch를 애뮬레이트하여 길게 누르고 있으면 인식
 */
public class UIControlBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float doubleClickThreshold = 0.2f;   //200ms안에 더블클릭을 인식한다.
    bool _isProcess = false;
    int _touchID = -1;
    int _cntDown = 0;   //터치가 내려간 카운트
    int _cntUP = 0;     //터치가 올라간 카운트
    float _elaspedTime = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_isProcess)
        {
            _elaspedTime += Time.deltaTime;
            if(-1 < _touchID && Input.touchPressureSupported)
            {
                //3d touch로 세게 눌렀을 경우의 처리
                Touch _t = GetTouchByID(_touchID);
                if (2.0f < _t.pressure)
                {
                    SetClickState(false);
                    SkillClick(_t.position.x,_t.position.y);
                }
            }
        }
		
        //더블클릭을 인식하는 시간이 지난경우
        if(doubleClickThreshold < _elaspedTime)
        {
            _isProcess = false;
            Vector2 _position;

            if(100 == _touchID)
            {
                _position = Input.mousePosition;
            }
            else
            {
                _position = GetTouchByID(_touchID).position;
            }

            if(0 == _cntUP)
            {
                //지정시간동안 누르고 있는 경우
                SkillClick(_position.x,_position.y);
            }
            else
            {
                //일반 공격의 경우
                NormalClick(_position.x, _position.y);
            }
            SetClickState(false);
        }
	}
    /// <summary>
    /// 터치를 시작하고 끝날때 참고해야된 변수들이 많아 여기서 일괄적으로 초기화 한다.
    /// </summary>
    /// <param name="isStart">true = 터치가 시작되었다,false = 터치의 판단이 끝났다</param>
    void SetClickState(bool isStart)
    {
        if(isStart)
        {
            _cntDown = 0;
            _cntUP = 0;
            _elaspedTime = 0;
            _isProcess = true;
        }
        else
        {
            _isProcess = false;
            _elaspedTime = 0;
            _touchID = -2;
            _cntDown = 0;
            _cntUP = 0;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_isProcess)
        {
            SetClickState(true);
            _touchID = eventData.pointerId==-1 ? 100 : eventData.pointerId;
        }
        else
        {
            //한번 눌리고 다시 일정시간안데 다시 눌렸다면 더블클릭임을 확신할수 있음으로 스킬이다.
            SetClickState(false);
            SkillClick(eventData.position.x, eventData.position.y);
        }
        _cntDown++;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _cntUP++;
    }
    public static Touch GetTouchByID(int id)
    {
        for(int i=0;i<Input.touchCount;i++)
        {
            Touch _t = Input.GetTouch(i);
            if(_t.fingerId == id)
            {
                return _t;
            }
        }
        return Input.GetTouch(0);
    }
#region 외부와의 인터페이스
    public virtual void NormalClick(float x,float y)
    {
        //Debug.Log("NormalClick " + x + "," + y);
    }
    public virtual void SkillClick(float x, float y)
    {
        //Debug.Log("SkillClick " + x + "," + y);
    }
#endregion
}
