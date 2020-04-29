using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゆっくり指定したオブジェクトに追従する
// 初期状態の相対位置を保持する

public class ChaseTarget : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 10f;
    private Vector3 _relative;

    void Awake()
    {
        _relative = transform.position - _target.position;
        print(transform.position + ", " + _target.position);
        
    }

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    // Update is called once per frame
    public void UpdateMe()
    {
        // transform.position = _target.position;
        transform.position = Vector3.Lerp(transform.position, _target.position + _relative, _speed);
    }
}
