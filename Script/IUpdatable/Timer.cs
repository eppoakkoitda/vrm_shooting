// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour, IUpdatable
{
    float _count = 0;
    int _sec = 0;
    [SerializeField] TextMeshProUGUI _text;
    void OnEnable()
    {
        //_count = 0;
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    
    public void UpdateMe()
    {
        _count += UpdateManager.deltaTime;
        _sec = (int)(_count / 1f);
        _text.SetText("{0}", _sec); 
    }
}
