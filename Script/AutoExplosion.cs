using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoExplosion : MonoBehaviour
{
    public GameObject _prefab; // エフェクト
    public float _enableDistanceFromCamera = 20f;
    [SerializeField] AudioClip _SE;
    private GameObject _pool;
    private GameObject _parent; //  メンバじゃないかも
    private PoolObjectsToList _potl;

    void Awake()
    {
        // 標準エフェクトを設定することで_prefab==nullを回避
        if(_prefab == null) _prefab = Resources.Load<GameObject>("ErbGameArt/Procedural fire/Prefabs/Explosion");

        _parent = GameObject.Find("pool" + _prefab.name);
        if(_parent == null){
            _parent = new GameObject("pool" + _prefab.name);
            _potl = _parent.AddComponent<PoolObjectsToList>();
            _potl.Initialize(_prefab, _parent.transform);
        }else{
            _potl = _parent.GetComponent<PoolObjectsToList>();
        }
        
    }

    void OnDisable()
    {
        _pool = _potl.GetPoolObj(); // EnableFalsePoolObj()では対応しきれない
        if(_pool == null) return;   
        // _pool.GetComponent<ParticleSystem>().Play();

        // print(Vector3.Distance(transform.position, Camera.main.transform.position) + " < " + _enableDistanceFromCamera);
        
        if(Vector3.Distance(transform.position, Camera.main.transform.position) < _enableDistanceFromCamera){
            _pool.transform.position = transform.position;
            _pool.SetActive(true);

            if(_SE != null){
                var audioSource = _pool.GetComponent<AudioSource>();
                if(audioSource != null){
                    audioSource.PlayOneShot(_SE);
                }else{
                    audioSource = _pool.AddComponent<AudioSource>();
                    audioSource.PlayOneShot(_SE);
                }
            }

        }    
    }

}
