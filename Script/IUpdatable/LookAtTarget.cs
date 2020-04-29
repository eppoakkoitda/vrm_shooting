using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform _target;

    private Vector3 _targetPos;

    public float _limitAngle = 30f;
    private Vector3 _toTarget;
    private Quaternion _toTargetRot;
    private float _angleDiff;
    private float _dir;
    private Vector3 _rotateParameter;

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    // Update is called once per frame
    public void UpdateMe()
    {
        // アップデートにしては長すぎいい

        _targetPos = _target.position;
        _dir = _targetPos.y - transform.position.y;  
        _targetPos.y = transform.position.y;
        transform.LookAt(_targetPos);

        _toTarget = _target.position - transform.position;
        _toTargetRot = Quaternion.LookRotation(_toTarget);
        _angleDiff = Quaternion.Angle(_toTargetRot, transform.rotation);
    
        if(_angleDiff < _limitAngle){
            transform.rotation = _toTargetRot;
        }else{
            _rotateParameter.x = (_dir<0) ? _limitAngle : -_limitAngle;
            transform.Rotate(_rotateParameter);

        }
    }
}
