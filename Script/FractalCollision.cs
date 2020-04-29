using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class FractalCollision : MonoBehaviour
{
    public GameObject _DistanceFuncObj;

    [System.Serializable]
    public class CustomUnityEvent : UnityEvent<Vector3>{};
    public CustomUnityEvent ON_BURIED_FRACTAL;
    private Func<Vector3, float> _fractalDistFunc;
    private float _radius = 1f;

    void Awake()
    {
        if(ON_BURIED_FRACTAL == null) ON_BURIED_FRACTAL = new CustomUnityEvent();
        IDistanceFunc disfun = _DistanceFuncObj.GetComponent<IDistanceFunc>();
        _fractalDistFunc = disfun.DistanceFunction;
        _radius = GetComponent<SphereCollider>().radius;
    }

    void FixedUpdate()
    {
        // print(_fractalDistFunc(transform.position));
        if (_fractalDistFunc(transform.position) < _radius) {
            ON_BURIED_FRACTAL.Invoke(CalcNormal(transform.position));
        }
    }

    public Vector3 CalcNormal(Vector3 pos)
    {
        var d = 0.01f;
        return new Vector3(
            _fractalDistFunc(pos + new Vector3( d, 0f, 0f)) - _fractalDistFunc(pos + new Vector3(-d, 0f, 0f)),
            _fractalDistFunc(pos + new Vector3(0f,  d, 0f)) - _fractalDistFunc(pos + new Vector3(0f, -d, 0f)),
            _fractalDistFunc(pos + new Vector3(0f, 0f,  d)) - _fractalDistFunc(pos + new Vector3(0f, 0f, -d))).normalized;
    }
}
