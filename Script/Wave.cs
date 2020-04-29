using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] Presenter UIPresenter;
    public void OnEnemyDeath()
    {
        foreach(Transform enemy in transform){
            if(enemy.gameObject.activeSelf) return;
        }
        transform.parent.GetComponent<WaveManager>().ChangeWave();
    }

    void OnEnable()
    {
        UIPresenter.SetEnemies(this);
    }

    public Transform[] GetChildTransforms()
    {
        Transform[] transforms = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; ++i){
            transforms[i] = transform.GetChild(i).transform;
        }
        return transforms;
    }
}
