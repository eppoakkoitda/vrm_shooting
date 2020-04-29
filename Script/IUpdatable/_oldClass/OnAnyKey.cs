using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnyKey : MonoBehaviour, IUpdatable
{
    public KeyCode _keyCode;
    public float _waitTime = 0f;
    public UnityEvent ON_KEY = new UnityEvent();
    public UnityEvent ON_KEY_DOWN = new UnityEvent();
    private float _time = 0f;

    void OnEnable()
    {
        _time = 0f;
        UpdateManager.AddUpdatable(this);
    }

    public void UpdateMe()
    {
        if(Input.GetKey(_keyCode) && _time > _waitTime){
            _time = 0f;
            ON_KEY.Invoke();
        }

        if(Input.GetKeyDown(_keyCode)){
            ON_KEY_DOWN.Invoke();
        }
        _time += Time.deltaTime;
    } 
}
