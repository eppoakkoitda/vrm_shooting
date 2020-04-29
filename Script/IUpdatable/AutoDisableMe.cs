using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ParticleSystem))]
public class AutoDisableMe : MonoBehaviour, IUpdatable
{
    private float _waitTime = 0;
    private float _time;
    
    void Start()
    {
        _waitTime = GetComponent<ParticleSystem>().main.duration;
    }
    void OnEnable()
    {
        _time = 0;
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public void UpdateMe()
    {
        _time += Time.deltaTime;
        if(_time > _waitTime){
            gameObject.SetActive(false);
        }
    }
}
