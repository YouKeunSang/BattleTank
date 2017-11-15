using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour {
    public float speed = 15f;
    public float damage = 100f;
    public float radious = 10;  //폭팔범위
    public GameObject explosion;
    Vector3 _dir;
    float _timeToTarget = 0;
    bool _isMove = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_isMove)
        {
            _timeToTarget -= Time.deltaTime;
            gameObject.transform.position += _dir * speed * Time.deltaTime;
            if(0>=_timeToTarget)
            {
                _isMove = false;
                Expire();
            }
        }
		
	}
    /// <summary>
    /// 포탄을 특정좌표로 발사한다
    /// </summary>
    /// <param name="dest">여기서 좌표는 screen좌표가 아니고 계산된 world의 좌표임</param>
    public void Fire(Vector3 dest)
    {
        dest.y = gameObject.transform.position.y;
        _dir = dest - gameObject.transform.position;
        _timeToTarget = _dir.magnitude / speed;
        _dir.Normalize();
        _isMove = true;
    }
    /// <summary>
    /// 지정된 위치에 도달한 경우
    /// </summary>
    void Expire()
    {
        if(null != explosion)
        {
            GameObject _obj = Instantiate(explosion) as GameObject;
            _obj.transform.position = transform.position;
            _obj.transform.localScale = new Vector3(radious,radious,radious);
            ParticleSystem[] _ps = _obj.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in _ps)
            {
                p.Play();
                
            }
        }
        Destroy(gameObject);

    }
}
