using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 子オブジェクト達を距離関数によって移動させるコンポーネント

public class ChildsMoveByFunc : MonoBehaviour, IUpdatable
{
    // 時間を渡すと半径距離が返る関数
    // [SerializeField] private Func<float, float> _func = (float t) => { return t;};

    public AnimationCurve _animationCurve = null;
    public float _rateX = 1f;
    public float _rateY = 1f;
    //private float _curveRate = 0;
    private float _time = 0;
    private Transform[] _childTransform;

    void Awake()
    {
        _childTransform = new Transform[transform.childCount];
        GetChildTransform();
    }

    private void GetChildTransform()
    {
        for(int i = 0; i < transform.childCount; ++i){
            _childTransform[i] = transform.GetChild(i).transform;
        }
    }

    // 有効化するたびに呼ばれる
    void OnEnable()
    {
        _time = 0;

        for(int i = 0; i < transform.childCount; ++i){
            _childTransform[i].position = transform.position;
        }
        // foreach(Transform child in transform){
        //     child.transform.position = transform.position; 
        // }

        UpdateManager.AddUpdatable(this);

    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    // Update is called once per frame
    public void UpdateMe()
    {
        for(int i = 0; i < transform.childCount; ++i){
            if(_childTransform[i] == null) GetChildTransform();
            _childTransform[i].position = transform.position + _animationCurve.Evaluate(_time*_rateX)*_rateY * transform.up;
            _childTransform[i].RotateAround(transform.position, transform.forward, i * 360f / transform.childCount);
        }
        // int i = 0;
        // foreach(Transform child in transform){
        //     // child.transform.position = transform.position + _func(_time) * transform.up;
        //     child.transform.position = transform.position + _animationCurve.Evaluate(_time*_rateX)*_rateY * transform.up;
        //     child.transform.RotateAround(transform.position, transform.forward, i * 360f / transform.childCount);
        //     i++;
        // }
        _time += UpdateManager.deltaTime;
    }
}
