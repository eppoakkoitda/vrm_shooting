using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// これをアタッチするとwasdで操作できる

public class CharaCtr1 : MonoBehaviour, IInputKeyReceiver
{
    [SerializeField] private float _speed = 10f;
    private float _initSpeed;
    private bool _flag = false;
    [SerializeField] Vector3 _limit;
    private Vector3 _newPosition;
    private float _deltaTime;

    void Awake()
    {
        _initSpeed = _speed;
    }

    void OnEnable()
    {
        InputKeySender.Add(this);
        _deltaTime = 1f/UpdateManager.FPS;
    }

    // Update is called once per frame
    public void OnKeys(KeyEvent e)
    {
        _flag = false;

        if(e.W){
            transform.position += _speed * Camera.main.transform.forward * _deltaTime;
            _flag = true;
        }else if(e.S){
            transform.position -= _speed * Camera.main.transform.forward * _deltaTime;
            _flag = true;
        }
        if(e.A){
            transform.position -= _speed * Camera.main.transform.right * _deltaTime;
            _flag = true;
        }else if(e.D){
            transform.position += _speed * Camera.main.transform.right * _deltaTime;
            _flag = true;
        }

        if(e.Space){
            if(!_flag) transform.position += _speed * Camera.main.transform.up * _deltaTime;
            if(_flag) _speed = _initSpeed*2f;
        }else if(e.LeftShift){
            if(!_flag) transform.position -= _speed * Camera.main.transform.up * _deltaTime;
            if(_flag) _speed = _initSpeed*0.5f;
        }else{
            if(_speed != _initSpeed) _speed = _initSpeed;
        }

        _newPosition.x = Mathf.Clamp(transform.position.x, -_limit.x, _limit.x);
        _newPosition.y = Mathf.Clamp(transform.position.y, -_limit.y, _limit.y);
        _newPosition.z = Mathf.Clamp(transform.position.z, -_limit.z, _limit.z);

        transform.position = _newPosition;
    }

    [SerializeField] private float _back = 0.5f;
    public void OnBuried(Vector3 normal){
        transform.position += _back * normal;
    }
}
