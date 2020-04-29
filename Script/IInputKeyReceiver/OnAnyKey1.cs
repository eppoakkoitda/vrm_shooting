using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnyKey1 : MonoBehaviour, IInputKeyReceiver
{
    public string _keyCode;
    public float _waitTime = 0f;
    public UnityEvent ON_KEY = new UnityEvent();
    private float _time = 0f;
    private System.Reflection.FieldInfo _vals;

    void OnEnable()
    {
        _time = 0f;
        InputKeySender.Add(this);
    }

    public void OnKeys(KeyEvent e)
    {
        if(_vals == null){
            _vals = e.GetType().GetField(_keyCode);
        }
    
        if((bool)_vals.GetValue(e) && UpdateManager.time - _time > _waitTime){
            _time = UpdateManager.time;
            ON_KEY.Invoke();
        }
        
    } 
}
