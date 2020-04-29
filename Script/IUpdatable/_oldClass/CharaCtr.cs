using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// これをアタッチするとwasdで操作できる

public class CharaCtr : MonoBehaviour, IUpdatable
{
    [SerializeField] private float _speed = 10f;
    private float _initSpeed;
    private bool _flag = false;
    [SerializeField] Vector3 _limit;
    [SerializeField] bool _hasRigidBody;
    private Vector3 _newPosition;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _initSpeed = _speed;
        if(_hasRigidBody) _rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    // Update is called once per frame
    public void UpdateMe()
    {
        _flag = false;

        if(Input.GetKey(KeyCode.W)){
            transform.position += _speed * Camera.main.transform.forward * Time.smoothDeltaTime;
            _flag = true;
        }else if(Input.GetKey(KeyCode.S)){
            transform.position -= _speed * Camera.main.transform.forward * Time.smoothDeltaTime;
            _flag = true;
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position -= _speed * Camera.main.transform.right * Time.smoothDeltaTime;
            _flag = true;
        }else if(Input.GetKey(KeyCode.D)){
            transform.position += _speed * Camera.main.transform.right * Time.smoothDeltaTime;
            _flag = true;
        }

        if(Input.GetKey(KeyCode.Space)){
            if(!_flag) transform.position += _speed * Camera.main.transform.up * Time.smoothDeltaTime;
            if(_flag) _speed = _initSpeed*2f;
        }else if(Input.GetKey(KeyCode.LeftShift)){
            if(!_flag) transform.position -= _speed * Camera.main.transform.up * Time.smoothDeltaTime;
            if(_flag) _speed = _initSpeed*0.5f;
        }else{
            if(_speed != _initSpeed) _speed = _initSpeed;
        }

        
        _newPosition.x = Mathf.Clamp(transform.position.x, -_limit.x, _limit.x);
        _newPosition.y = Mathf.Clamp(transform.position.y, -_limit.y, _limit.y);
        _newPosition.z = Mathf.Clamp(transform.position.z, -_limit.z, _limit.z);

        if(_hasRigidBody){
            _rigidbody.position = _newPosition;
        }else{
            transform.position = _newPosition;
        }
        
    }

    [SerializeField] private float _back = 0.5f;
    public void OnBuried(Vector3 normal){
        transform.position += _back * normal;
    }
}
