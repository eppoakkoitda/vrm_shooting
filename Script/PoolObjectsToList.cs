using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectsToList : MonoBehaviour
{
    // 指定したオブジェクトをリストにプールする GetPoolObjでfalseのobj取得可能
    // オプションでプールオブジェクトを指定したオブジェクトの子における

    public GameObject _prefab;
    private List<GameObject> _poolObj = new List<GameObject>();
    [SerializeField] private Transform _parent;
    [SerializeField] private int _limit = 15;

    public void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        
    }

    public void Initialize(GameObject prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    void OnDisable()
    {
        Reset();
    }

    public GameObject GetPoolObj()
    {
        if(_poolObj.Count > 0){
            foreach(GameObject child in _poolObj){
                if(child.activeSelf == false){
                    return child;
                }
            }
        }

        if(_poolObj.Count >= _limit) return null;

        return CreatePoolObj();
    }

    public GameObject CreatePoolObj()
    {
        if(_prefab == null) return null;
        GameObject obj = Instantiate<GameObject>(_prefab, transform.position, Quaternion.identity);
        _poolObj.Add(obj);
        if(_parent != null) obj.transform.parent = _parent;
        return obj;
    }

    // 無効状態のオブジェクトを一つ有効にする
    public void EnableFalsePoolObj()
    {
        GameObject a = GetPoolObj();
        GameObject child;
        if(a == null) return;
        a.transform.position = transform.position;
        a.SetActive(true);
        for(int i = 0; i < a.transform.childCount; ++i){
            child = a.transform.GetChild(i).gameObject;
            if(child.activeSelf == false){
                child.SetActive(true);
            }
        }
    }

    public void Reset()
    {
        foreach(GameObject child in _poolObj){
            Destroy(child);
        }

        _poolObj.Clear();
    }
}
