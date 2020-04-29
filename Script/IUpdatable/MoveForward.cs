using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スピードを指定すると時間経過で前進し、自壊するコンポーネント

public class MoveForward : MonoBehaviour, IUpdatable
{
    public float _speed = 10f;
    public float _roteSpeed = 10f;
    public float _destroyTime = 5f;
    protected float _time = 0f;

    void OnEnable()
    {
        _time = 0f;
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    virtual public void UpdateMe()
    {
        transform.position += _speed * transform.forward * Time.deltaTime;
        transform.Rotate(transform.forward, _roteSpeed * Time.deltaTime);
        _time += Time.deltaTime;
        if(_time > _destroyTime) gameObject.SetActive(false);
    }
}
