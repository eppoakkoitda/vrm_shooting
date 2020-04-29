using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スピードを指定すると時間経過で指定オブジェクトを追跡し、自壊するコンポーネント

public class MoveChase : MoveForward
{
    private Transform _target;
    private Vector3 _toTarget;
    private Quaternion _toTargetRot;
    private float _angleDiff;
    private float _deltaTime;
    public float _limitAngle = 4f;
    public string _targetName;

    void Awake() // これたしかOnEnable前
    {
        if(_targetName == null) _targetName = "DelayAim";
        _target = GameObject.Find(_targetName).transform;
    }

    public void OnEnable()
    {
        _time = 0f;
        if(transform.parent){
            transform.position = transform.parent.position;
        }
        transform.LookAt(_target.position);
        
        _deltaTime = UpdateManager.deltaTime;
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public override void UpdateMe()
    {
        _toTarget = _target.position - transform.position;
        _toTargetRot = Quaternion.LookRotation(_toTarget);
        _angleDiff = Quaternion.Angle(_toTargetRot, transform.rotation);
        
        if(_angleDiff < _limitAngle){
            transform.rotation = Quaternion.Slerp(transform.rotation, _toTargetRot, _deltaTime * 20f * 90f / _angleDiff);
        }

        transform.RotateAround(transform.forward, _roteSpeed * _deltaTime);
        transform.position += _speed * transform.forward * _deltaTime;
        
        _time += UpdateManager.deltaTime;

        if(_time > _destroyTime) gameObject.SetActive(false);
    }
}
