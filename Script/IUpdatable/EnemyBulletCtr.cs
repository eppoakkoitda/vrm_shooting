using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCtr : MonoBehaviour, IUpdatable
{
    [SerializeField] PoolObjectsToList _gun;
    [SerializeField] float _freq;

    private float _time = 0;

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public void UpdateMe()
    {
        if(_freq <= _time){
            Shot();
            _time = 0;
        }
        _time += UpdateManager.deltaTime;
    }

    private void Shot()
    {
        _gun.EnableFalsePoolObj();
    }

}
