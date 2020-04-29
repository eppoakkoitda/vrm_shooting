using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SimpleFractalCollision : MonoBehaviour, IUpdatable
{
    public string _DistanceFuncObjName = "DistanceFunc";
    //public UnityEvent ON_BURIED_FRACTAL;
    private Func<Vector3, float> _fractalDistFunc;
    private float _radius;

    void Awake()
    {
        GameObject distanceFuncObj = GameObject.Find(_DistanceFuncObjName);

        if(distanceFuncObj != null){
           IDistanceFunc disfun = distanceFuncObj.GetComponent<IDistanceFunc>();
            _fractalDistFunc = disfun.DistanceFunction;
            _radius = GetComponent<SphereCollider>().radius;
        }
    }

    void OnEnable()
    {
        // if(!gameObject.activeSelf) gameObject.SetActive(true);
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    // シンプル版は一定周期で呼び出す保証はしなくてもよい(nonFixed)
    public void UpdateMe()
    {
        if(_fractalDistFunc != null){
            if (_fractalDistFunc(transform.position) < _radius) {
                //print(transform.parent.name + " -> " + transform.name);
                //ON_BURIED_FRACTAL.Invoke();
                if(gameObject.activeSelf) gameObject.SetActive(false);
            }
        }
    }
}
